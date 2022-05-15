namespace Dreamteck.Splines.Editor
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEditor;

    [CustomEditor(typeof(SplineUser), true)]
    [CanEditMultipleObjects]
    public class SplineUserEditor : Editor
    {
        protected bool showClip = true;
        protected bool showAveraging = true;
        protected bool showUpdateMethod = true;
        protected bool showMultithreading = true;
        private bool settingsFoldout = false;
        protected RotationModifierEditor rotationModifierEditor;
        protected OffsetModifierEditor offsetModifierEditor;
        protected ColorModifierEditor colorModifierEditor;
        protected SizeModifierEditor sizeModifierEditor;
        protected SplineUser[] users = new SplineUser[0];
        protected SerializedObject serializedUsers;
        SerializedProperty multithreaded, updateMethod, buildOnAwake, buildOnEnable, autoUpdate, loopSamples, clipFrom, clipTo;
        protected GUIStyle foldoutHeaderStyle;

        bool doRebuild = false;
        protected SerializedProperty spline;

        public int editIndex
        {
            get { return _editIndex; }
            set
            {
                if(value == 0)
                {
                    Debug.LogError("Cannot set edit index to 0. 0 is reserved.");
                    return;
                }
                if (value < -1) value = -1;
                _editIndex = value;
            }
        }
        private int _editIndex = -1; //0 is reserved for editing clip values

        protected GUIContent editButtonContent = new GUIContent("Edit", "Enable edit mode in scene view");

        protected virtual void HeaderGUI()
        {
            SplineUser user = (SplineUser)target;

            Undo.RecordObject(user, "Inspector Change");
            SplineComputer lastSpline = (SplineComputer)spline.objectReferenceValue;
            EditorGUILayout.PropertyField(spline);
            SplineComputer newSpline = (SplineComputer)spline.objectReferenceValue;
            if (lastSpline != (SplineComputer)spline.objectReferenceValue)
            {
                for (int i = 0; i < users.Length; i++)
                {
                    if (lastSpline != null) lastSpline.Unsubscribe(users[i]);
                    if (newSpline != null) newSpline.Subscribe(users[i]);
                }
                user.Rebuild();
            }


            if (user.spline == null) EditorGUILayout.HelpBox("No SplineComputer is referenced. Link a SplineComputer to make this SplineUser work.", MessageType.Error);

            settingsFoldout = EditorGUILayout.Foldout(settingsFoldout, "User Configuration", foldoutHeaderStyle);
            if (settingsFoldout)
            {
                EditorGUI.indentLevel++;
                if (showClip) InspectorClipEdit();
                if (showUpdateMethod) EditorGUILayout.PropertyField(updateMethod);
                EditorGUILayout.PropertyField(autoUpdate, new GUIContent("Auto Rebuild"));
                if (showMultithreading) EditorGUILayout.PropertyField(multithreaded);
                EditorGUILayout.PropertyField(buildOnAwake);
                EditorGUILayout.PropertyField(buildOnEnable);
                EditorGUI.indentLevel--;
            }
        }

        private void InspectorClipEdit()
        {
            bool isClosed = true;
            bool loopSamples = true;
            for (int i = 0; i < users.Length; i++)
            {
                if (users[i].spline == null) isClosed = false;
                else if (!users[i].spline.isClosed) isClosed = false;
                else if (!users[i].loopSamples) loopSamples = false;
            }

            float clipFrom = 0f, clipTo = 1f;
            clipFrom = this.clipFrom.floatValue;
            clipTo = this.clipTo.floatValue;
            EditorGUI.BeginChangeCheck();

            if (isClosed && loopSamples)
            {
                EditorGUILayout.BeginHorizontal();
                if (EditButton(_editIndex == 0))
                {
                    if (_editIndex == 0) _editIndex = -1;
                    else _editIndex = 0;
                }
                EditorGUILayout.BeginVertical();
                clipFrom = EditorGUILayout.Slider("Clip From", clipFrom, 0f, 1f);
                clipTo = EditorGUILayout.Slider("Clip To", clipTo, 0f, 1f);
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
            else
            {

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.BeginHorizontal();
                if (EditButton(_editIndex == 0))
                {
                    if (_editIndex == 0) _editIndex = -1;
                    else _editIndex = 0;
                }
                EditorGUIUtility.labelWidth = 80f;
                EditorGUILayout.MinMaxSlider(new GUIContent("Clip Range:"), ref clipFrom, ref clipTo, 0f, 1f);
                EditorGUIUtility.labelWidth = 0f;
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal(GUILayout.MaxWidth(30));
                clipFrom = EditorGUILayout.FloatField(clipFrom);
                clipTo = EditorGUILayout.FloatField(clipTo);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndHorizontal();
            }
            if (EditorGUI.EndChangeCheck())
            {
                this.clipFrom.floatValue = clipFrom;
                this.clipTo.floatValue = clipTo;
            }
            SplineComputerEditor.hold = _editIndex >= 0;

            if (isClosed) EditorGUILayout.PropertyField(this.loopSamples, new GUIContent("Loop Samples"));
            if (!this.loopSamples.boolValue || !isClosed)
            {
                if (this.clipFrom.floatValue > this.clipTo.floatValue)
                {
                    float temp = this.clipTo.floatValue;
                    this.clipTo.floatValue = this.clipFrom.floatValue;
                    this.clipFrom.floatValue = temp;
                }
            }
        }

        protected virtual void BodyGUI()
        {
            EditorGUILayout.Space();
        }

        protected virtual void FooterGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Sample Modifiers", EditorStyles.boldLabel);
            if (users.Length == 1)
            {
                if (offsetModifierEditor != null) offsetModifierEditor.DrawInspector();
                if (rotationModifierEditor != null) rotationModifierEditor.DrawInspector();
                if (colorModifierEditor != null) colorModifierEditor.DrawInspector();
                if (sizeModifierEditor != null) sizeModifierEditor.DrawInspector();
            }
            else EditorGUILayout.LabelField("Modifiers not available when multiple Spline Users are selected.", EditorStyles.centeredGreyMiniLabel);
            
            EditorGUILayout.Space();
        }

        protected virtual void OnSceneGUI()
        {
            if (doRebuild) DoRebuild();
            SplineUser user = (SplineUser)target;
            if (user == null) return;
            if (user.spline != null)
            {
                SplineComputer rootComputer = user.GetComponent<SplineComputer>();
                List<SplineComputer> allComputers = user.spline.GetConnectedComputers();
                for (int i = 0; i < allComputers.Count; i++)
                {
                    if (allComputers[i] == rootComputer && _editIndex == -1) continue;
                    if (allComputers[i].alwaysDraw) continue;
                    DSSplineDrawer.DrawSplineComputer(allComputers[i], 0.0, 1.0, 0.4f);
                }
                DSSplineDrawer.DrawSplineComputer(user.spline);
            }
            if (_editIndex == 0) SceneClipEdit();
            if (offsetModifierEditor != null) offsetModifierEditor.DrawScene();
            if (rotationModifierEditor != null)  rotationModifierEditor.DrawScene();
            if (colorModifierEditor != null) colorModifierEditor.DrawScene();
            if (sizeModifierEditor != null) sizeModifierEditor.DrawScene();
        }

        void SceneClipEdit()
        {
            if (users.Length > 1) return;
            SplineUser user = (SplineUser)target;
            if (user.spline == null) return;
            Color col = user.spline.editorPathColor;
            Undo.RecordObject(user, "Edit Clip Range");
            double val = user.clipFrom;
            SplineEditorHandles.Slider(user.spline, ref val, col, "Clip From", SplineEditorHandles.SplineSliderGizmo.ForwardTriangle);
            if (val != user.clipFrom) user.clipFrom = val;
            val = user.clipTo;
            SplineEditorHandles.Slider(user.spline, ref val, col, "Clip To", SplineEditorHandles.SplineSliderGizmo.BackwardTriangle);
            if (val != user.clipTo) user.clipTo = val;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (doRebuild) DoRebuild();
            serializedUsers = new SerializedObject(users);
            updateMethod = serializedUsers.FindProperty("updateMethod");
            buildOnAwake = serializedUsers.FindProperty("buildOnAwake");
            buildOnEnable = serializedUsers.FindProperty("buildOnEnable");
            multithreaded = serializedUsers.FindProperty("multithreaded");
            autoUpdate = serializedUsers.FindProperty("_autoUpdate");
            SerializedProperty sampleCollection = serializedUsers.FindProperty("sampleCollection");
            loopSamples = sampleCollection.FindPropertyRelative("loopSamples");
            clipFrom = sampleCollection.FindPropertyRelative("clipFrom");
            clipTo = sampleCollection.FindPropertyRelative("clipTo"); ;
            spline = serializedUsers.FindProperty("_spline");

            EditorGUI.BeginChangeCheck();
            HeaderGUI();
            if (EditorGUI.EndChangeCheck())
            {
                serializedUsers.ApplyModifiedProperties();
                for (int i = 0; i < users.Length; i++)
                {
                    EditorUtility.SetDirty(users[i]);
                    users[i].Rebuild();
                }
            }

            EditorGUI.BeginChangeCheck();
            BodyGUI();
            if (EditorGUI.EndChangeCheck())
            {
                serializedUsers.ApplyModifiedProperties();
                for (int i = 0; i < users.Length; i++)
                {
                    try
                    {
                        EditorUtility.SetDirty(users[i]);
                        users[i].Rebuild();
                    } catch (System.Exception ex)
                    {
                        Debug.Log(ex.Message);
                    }
                }
            }

            EditorGUI.BeginChangeCheck();
            FooterGUI();
            if (EditorGUI.EndChangeCheck())
            {
                serializedUsers.ApplyModifiedProperties();
                for (int i = 0; i < users.Length; i++)
                {
                    EditorUtility.SetDirty(users[i]);
                    users[i].Rebuild();
                }
            }
        }

        void DoRebuild()
        {
            for (int i = 0; i < users.Length; i++) users[i].Rebuild();
            doRebuild = false;
        }

        protected virtual void OnDestroy()
        {
            SplineUser user = (SplineUser)target;
            if (Application.isEditor && !Application.isPlaying)
            {
                if (user == null) OnDelete(); //The object or the component is being deleted
                else if (user.spline != null) user.Rebuild();
            }
            SplineComputerEditor.hold = false;
        }

        protected virtual void OnDelete()
        {

        }

        protected virtual void Awake()
        {
#if UNITY_2018_OR_NEWER
            foldoutHeader = EditorStyles.foldoutHeader;
#else
            foldoutHeaderStyle = EditorStyles.foldout;
#endif
        }

        protected virtual void OnEnable()
        {
            SplineUser user = (SplineUser)target;
            user.EditorAwake();
            settingsFoldout = EditorPrefs.GetBool("Dreamteck.Splines.Editor.SplineUserEditor.settingsFoldout", false);

            rotationModifierEditor = new RotationModifierEditor(user, this, user.rotationModifier);
            offsetModifierEditor = new OffsetModifierEditor(user, this, user.offsetModifier);
            colorModifierEditor = new ColorModifierEditor(user, this, user.colorModifier);
            sizeModifierEditor = new SizeModifierEditor(user, this, user.sizeModifier);

            users = new SplineUser[targets.Length];
            for (int i = 0; i < users.Length; i++) users[i] = (SplineUser)targets[i];
            Undo.undoRedoPerformed += OnUndoRedo;

        }


        protected virtual void OnDisable()
        {
            EditorPrefs.SetBool("Dreamteck.Splines.Editor.SplineUserEditor.settingsFoldout", settingsFoldout);
            Undo.undoRedoPerformed -= OnUndoRedo;
        }

        protected virtual void OnUndoRedo()
        {
            doRebuild = true;
        }

        public bool EditButton(bool selected)
        {
            float width = 40f;
            editButtonContent.image = ImageDB.GetImage("edit_cursor.png", "Splines/Editor/Icons");
            if (editButtonContent.image != null)
            {
                editButtonContent.text = "";
                width = 25f;
            }
            if (SplineEditorGUI.EditorLayoutSelectableButton(editButtonContent, true, selected, GUILayout.Width(width)))
            {
                SceneView.RepaintAll();
                return true;
            }
            return false;
        }
    }
}
