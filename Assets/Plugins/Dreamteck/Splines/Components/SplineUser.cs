using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Dreamteck.Splines {
    [ExecuteInEditMode]
    public class SplineUser : MonoBehaviour {
        public enum UpdateMethod { Update, FixedUpdate, LateUpdate }
        [HideInInspector]
        public UpdateMethod updateMethod = UpdateMethod.Update;
       
        public SplineComputer spline
        {
            get {
                return _spline;
            }
            set
            {
                if (value != _spline)
                {
                    if (_spline != null)
                    {
                        _spline.Unsubscribe(this);
                    }
                    _spline = value;
                    if (_spline != null)
                    {
                        _spline.Subscribe(this);
                        Rebuild();
                    }
                    OnSplineChanged();
                }
            }
        }

       
        public double clipFrom
        {
            get
            {
                return sampleCollection.clipFrom;
            }
            set
            {
                if (value != sampleCollection.clipFrom)
                {
                    animClipFrom = (float)sampleCollection.clipFrom;
                    sampleCollection.clipFrom = DMath.Clamp01(value);
                    if (sampleCollection.clipFrom > sampleCollection.clipTo)
                    {
                        if (!_spline.isClosed) sampleCollection.clipTo = sampleCollection.clipFrom;
                    }
                    getSamples = true;
                    Rebuild();
                }
            }
        }

        public double clipTo
        {
            get
            {
                return sampleCollection.clipTo;
            }
            set
            {

                if (value != sampleCollection.clipTo)
                {
                    animClipTo = (float)sampleCollection.clipTo;
                    sampleCollection.clipTo = DMath.Clamp01(value);
                    if (sampleCollection.clipTo < sampleCollection.clipFrom)
                    {
                        if (!_spline.isClosed) sampleCollection.clipFrom = sampleCollection.clipTo;
                    }
                    getSamples = true;
                    Rebuild();
                }
            }
        }

        public bool autoUpdate
        {
            get
            {
                return _autoUpdate;
            }
            set
            {
                if (value != _autoUpdate)
                {
                    _autoUpdate = value;
                    if (value) Rebuild();
                }
            }
        }

        public bool loopSamples
        {
            get
            {
                return sampleCollection.loopSamples;
            }
            set
            {
                if (value != sampleCollection.loopSamples)
                {
                    sampleCollection.loopSamples = value;
                    if(!sampleCollection.loopSamples && sampleCollection.clipTo < sampleCollection.clipFrom)
                    {
                        double temp = sampleCollection.clipTo;
                        sampleCollection.clipTo = sampleCollection.clipFrom;
                        sampleCollection.clipFrom = temp;
                    }
                    Rebuild();
                }
            }
        }

        //The percent of the spline that we're traversing
        public double span
        {
            get
            {
                if (samplesAreLooped) return (1.0 - sampleCollection.clipFrom) + sampleCollection.clipTo; 
                return sampleCollection.clipTo - sampleCollection.clipFrom;
            }
        }

        public bool samplesAreLooped
        {
            get
            {
                return sampleCollection.samplesAreLooped;
            }
        }

        public RotationModifier rotationModifier
        {
            get
            {
                return _rotationModifier;
            }
        }

        public OffsetModifier offsetModifier
        {
            get
            {
                return _offsetModifier;
            }
        }

        public ColorModifier colorModifier
        {
            get
            {
                return _colorModifier;
            }
        }

        public SizeModifier sizeModifier
        {
            get
            {
                return _sizeModifier;
            }
        }

        //Serialized values
        [SerializeField]
        [HideInInspector]
        [FormerlySerializedAs("_computer")]
        private SplineComputer _spline;
        [SerializeField]
        [HideInInspector]
        private bool _autoUpdate = true;
        [SerializeField]
        [HideInInspector]
        protected RotationModifier _rotationModifier = new RotationModifier();
        [SerializeField]
        [HideInInspector]
        protected OffsetModifier _offsetModifier = new OffsetModifier();
        [SerializeField]
        [HideInInspector]
        protected ColorModifier _colorModifier = new ColorModifier();
        [SerializeField]
        [HideInInspector]
        protected SizeModifier _sizeModifier = new SizeModifier();

        [SerializeField]
        [HideInInspector]
        private SampleCollection sampleCollection = new SampleCollection();

        [SerializeField]
        [HideInInspector]
        private SplineSample clipFromSample = new SplineSample(), clipToSample = new SplineSample();

        //float values used for making animations
        [SerializeField]
        [HideInInspector]
        private float animClipFrom = 0f;
        [SerializeField]
        [HideInInspector]
        private float animClipTo = 1f;
        private bool rebuild = false, getSamples = false, postBuild = false;
        protected Transform trs = null;
        public int sampleCount
        {
            get { return _sampleCount; }
        }
        [SerializeField]
        [HideInInspector]
        private int _sampleCount = 0, startSampleIndex = 0;
        /// <summary>
        /// Use this to work with the Evaluate and Project methods
        /// </summary>
        protected SplineSample evalResult = new SplineSample();

        //Threading values
        [HideInInspector]
        public volatile bool multithreaded = false;
        [HideInInspector]
        public bool buildOnAwake = false;
        [HideInInspector]
        public bool buildOnEnable = false;


#if UNITY_EDITOR
        /// <summary>
        /// Used by the custom editor. DO NO CALL THIS METHOD IN YOUR RUNTIME CODE
        /// </summary>
        public virtual void EditorAwake()
        {
            Awake();
            RebuildImmediate();
            GetSamples();
        }
#endif

        protected virtual void Awake() {
            trs = transform;
            if (spline == null) spline = GetComponent<SplineComputer>();
            else if (!spline.IsSubscribed(this)) spline.Subscribe(this);
            if (buildOnAwake) RebuildImmediate();
        }

        protected virtual void Reset()
        {
#if UNITY_EDITOR
            EditorAwake();
#endif
        }


        protected virtual void OnEnable()
        {
            if (spline != null) spline.Subscribe(this);
            if (buildOnEnable) RebuildImmediate();
        }

        protected virtual void OnDisable()
        {
            if (spline != null) spline.Unsubscribe(this);
        }

        protected virtual void OnDestroy()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying && spline != null) spline.Unsubscribe(this); //Unsubscribe if DestroyImmediate is called
#endif
        }

        protected virtual void OnDidApplyAnimationProperties()
        {
            bool clip = false;
            if (sampleCollection.clipFrom != animClipFrom || sampleCollection.clipTo != animClipTo) clip = true;
            sampleCollection.clipFrom = animClipFrom;
            sampleCollection.clipTo = animClipTo;
            Rebuild();
            if (clip) GetSamples();
        }

        /// <summary>
        /// Gets the sample at the given index without modifications
        /// </summary>
        /// <param name="index">Sample index</param>
        /// <returns></returns>
        public SplineSample GetSampleRaw(int index)
        {
            if (index >= _sampleCount) index = _sampleCount - 1;
            if (sampleCollection.samplesAreLooped)
            {
                int start, end;
                double lerp;
                sampleCollection.GetSamplingValues(clipFrom, out start, out lerp);
                sampleCollection.GetSamplingValues(clipTo, out end, out lerp);
                if (index == 0) return clipFromSample;
                int endSample = end;
                if (lerp > 0.0) endSample++;
                if (index == _sampleCount - 1) return clipToSample;
                int loopedIndex = start + index;
                if (loopedIndex >= sampleCollection.Count) loopedIndex -= sampleCollection.Count;
                return sampleCollection.samples[loopedIndex];
            }



            if (index == 0) return clipFromSample;
            if (index == _sampleCount - 1) return clipToSample;
            return sampleCollection.samples[startSampleIndex + index];
        }


        /// <summary>
        /// Returns the sample at the given index with modifiers applied
        /// </summary>
        /// <param name="index">Sample index</param>
        /// <param name="target">Sample to write to</param>
        public void GetSample(int index, SplineSample target)
        {
            ModifySample(GetSampleRaw(index), target);
        }


        /// <summary>
        /// Rebuild the SplineUser. This will cause Build and Build_MT to be called.
        /// </summary>
        /// <param name="sampleComputer">Should the SplineUser sample the SplineComputer</param>
        public virtual void Rebuild()
        {
#if UNITY_EDITOR
            if (trs == null) trs = transform;
            //If it's the editor and it's not playing, then rebuild immediate
            if (Application.isPlaying)
            {
                if (!autoUpdate) return;
                rebuild = getSamples = true;
            } else RebuildImmediate();
#else
             if (!autoUpdate) return;
             rebuild = getSamples = true;
#endif
        }

        /// <summary>
        /// Rebuild the SplineUser immediate. This method will call sample samples and call Build as soon as it's called even if the component is disabled.
        /// </summary>
        /// <param name="sampleComputer">Should the SplineUser sample the SplineComputer</param>
        public virtual void RebuildImmediate()
        {
#if UNITY_EDITOR
            if (trs == null) trs = transform;
#if !UNITY_2018_3_OR_NEWER
            if (PrefabUtility.GetPrefabType(gameObject) == PrefabType.Prefab) return;
#endif
#endif
            try
            {
                GetSamples();
                Build();
                PostBuild();
            } catch (System.Exception ex)
            {
                Debug.Log(ex.Message);
            }
            rebuild = false;
            getSamples = false;
        }

        private void Update()
        {
            if (updateMethod == UpdateMethod.Update)
            {
                Run();
                RunUpdate();
                LateRun();
            }
        }

        private void LateUpdate()
        {   
            if (updateMethod == UpdateMethod.LateUpdate)
            {
                Run();
                RunUpdate();
                LateRun();
            }
#if UNITY_EDITOR
            if(!Application.isPlaying && updateMethod == UpdateMethod.FixedUpdate)
            {
                Run();
                RunUpdate();
                LateRun();
            }
#endif
        }

        private void FixedUpdate()
        {
            if (updateMethod == UpdateMethod.FixedUpdate)
            {
                Run();
                RunUpdate();
                LateRun();
            } 
        }

        //Update logic for handling threads and rebuilding
        private void RunUpdate()
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return;
#endif
            //Handle rebuilding
            if (rebuild)
            {
                if (multithreaded)
                {
                    if (getSamples) SplineThreading.Run(ResampleAndBuildThreaded);
                    else SplineThreading.Run(BuildThreaded);
                }
                else
                {
                    if (getSamples || spline.sampleMode == SplineComputer.SampleMode.Optimized) GetSamples();
                    Build();
                    postBuild = true;
                }
                rebuild = false;
            }
            if (postBuild)
            {
                PostBuild();
                postBuild = false;
            }
        }

        void BuildThreaded()
        {
            Build();
            postBuild = true;
        }

        void ResampleAndBuildThreaded()
        {
            GetSamples();
            Build();
            postBuild = true;
        }

        /// Code to run every Update/FixedUpdate/LateUpdate before any building has taken place
        protected virtual void Run()
        {

        }

        /// Code to run every Update/FixedUpdate/LateUpdate after any rabuilding has taken place
        protected virtual void LateRun()
        {

        }

        //Used for calculations. Called on the main or the worker thread.
        protected virtual void Build()
        {
        }

        //Called on the Main thread only - used for applying the results from Build
        protected virtual void PostBuild()
        {
        }

        protected virtual void OnSplineChanged()
        {

        }

        /// <summary>
        /// Applies the SplineUser modifiers to the provided sample
        /// </summary>
        /// <param name="source">Original sample</param>
        /// <param name="destination">Destination sample</param>
        public void ModifySample(SplineSample source, SplineSample destination)
        {
            destination.CopyFrom(source);
            ModifySample(destination);
        }

        /// <summary>
        /// Applies the SplineUser modifiers to the provided sample
        /// </summary>
        /// <param name="sample"></param>
        public void ModifySample(SplineSample sample)
        {
            offsetModifier.Apply(sample);
            _rotationModifier.Apply(sample);
            _colorModifier.Apply(sample);
            _sizeModifier.Apply(sample);
        }

        /// <summary>
        /// Sets the clip range of the SplineUser. Same as setting clipFrom and clipTo
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void SetClipRange(double from, double to)
        {
            if (!_spline.isClosed && to < from) to = from;
            sampleCollection.clipFrom = DMath.Clamp01(from);
            sampleCollection.clipTo = DMath.Clamp01(to);
            GetSamples();
            Rebuild();
        }

        /// <summary>
        /// Gets the clipped samples defined by clipFrom and clipTo
        /// </summary>
        private void GetSamples()
        {
            if (spline == null) return;
            getSamples = false;
            spline.GetSamples(sampleCollection);
            sampleCollection.Evaluate(0.0, clipFromSample);
            sampleCollection.Evaluate(1.0, clipToSample);
            int start, end;
            _sampleCount = sampleCollection.GetClippedSampleCount(out start, out end);
            double lerp;
            sampleCollection.GetSamplingValues(sampleCollection.clipFrom, out startSampleIndex, out lerp);
        }

        /// <summary>
        /// Takes a regular 0-1 percent mapped to the start and end of the spline and maps it to the clipFrom and clipTo valies. Useful for working with clipped samples
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public double ClipPercent(double percent)
        {
            ClipPercent(ref percent);
            return percent;
        }

        /// <summary>
        /// Takes a regular 0-1 percent mapped to the start and end of the spline and maps it to the clipFrom and clipTo valies. Useful for working with clipped samples
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public void ClipPercent(ref double percent)
        {
            sampleCollection.ClipPercent(ref percent);
        }

        public double UnclipPercent(double percent)
        {
            UnclipPercent(ref percent);
            return percent;
        }

        //Converts a clipped [0-1] percent into a spline percent
        public void UnclipPercent(ref double percent)
        {
            sampleCollection.UnclipPercent(ref percent);
        }

        private int GetSampleIndex(double percent)
        {
            return DMath.FloorInt(percent * (sampleCollection.Count - 1));
        }

        public Vector3 EvaluatePosition(double percent)
        {
            return sampleCollection.EvaluatePosition(percent);
        }

        public void Evaluate(double percent, SplineSample result)
        {
            sampleCollection.Evaluate(percent, result);
            result.percent = DMath.Clamp01(percent);
        }

        public SplineSample Evaluate(double percent)
        {
            SplineSample result = new SplineSample();
            Evaluate(percent, result);
            result.percent = DMath.Clamp01(percent);
            return result;
        }

        public void Evaluate(ref SplineSample[] results, double from = 0.0, double to = 1.0)
        {
            sampleCollection.Evaluate(ref results, from, to);
            for (int i = 0; i < results.Length; i++) ClipPercent(ref results[i].percent);
        }

        public void EvaluatePositions(ref Vector3[] positions, double from = 0.0, double to = 1.0)
        {
            sampleCollection.EvaluatePositions(ref positions, from, to);
        }

        public double Travel(double start, float distance, Spline.Direction direction, out float moved)
        {
            moved = 0f;
            if (direction == Spline.Direction.Forward && start >= 1.0) return 1.0;
            else if (direction == Spline.Direction.Backward && start <= 0.0) return 0.0;
            if (distance == 0f) return DMath.Clamp01(start);
            double result = sampleCollection.Travel(start, distance, direction, out moved);
            return ClipPercent(result);
        }

        public double Travel(double start, float distance, Spline.Direction direction = Spline.Direction.Forward)
        {
            float moved;
            return Travel(start, distance, direction, out moved);
        }

        public virtual void Project(Vector3 position, SplineSample result, double from = 0.0, double to = 1.0)
        {
            if (_spline == null) return;
            sampleCollection.Project(position, _spline.pointCount, result, from, to);
            ClipPercent(ref result.percent);
        }

        public float CalculateLength(double from = 0.0, double to = 1.0)
        {
            return sampleCollection.CalculateLength(from, to);
        }
    }
}
