namespace Dreamteck.Splines.Editor
{
    using UnityEngine;
    using System.Collections;
    using UnityEditor;

    [CustomEditor(typeof(SplineFollower), true)]
    [CanEditMultipleObjects]
    public class SplineFollowerEditor : SplineTracerEditor
    {
        SplineSample result = new SplineSample();
        void OnSetDistance(float distance)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                SplineFollower follower = (SplineFollower)targets[i];
                double travel = follower.Travel(0.0, distance, Spline.Direction.Forward);
                follower.startPosition = travel;
            }
        }

        protected override void BodyGUI()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Following", EditorStyles.boldLabel);
            SplineFollower follower = (SplineFollower)target;

            serializedObject.Update();
            SerializedProperty followMode = serializedObject.FindProperty("followMode");
            SerializedProperty wrapMode = serializedObject.FindProperty("wrapMode");
            SerializedProperty startPosition = serializedObject.FindProperty("_startPosition");
            SerializedProperty autoStartPosition = serializedObject.FindProperty("autoStartPosition");
            SerializedProperty follow = serializedObject.FindProperty("follow");


            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(follow);
            EditorGUILayout.PropertyField(followMode);
            if (followMode.intValue == (int)SplineFollower.FollowMode.Uniform)
            {
                SerializedProperty followSpeed = serializedObject.FindProperty("_followSpeed");
                EditorGUILayout.PropertyField(followSpeed, new GUIContent("Follow Speed"));
                if (followSpeed.floatValue < 0f) followSpeed.floatValue = 0f;
            }
            else follower.followDuration = EditorGUILayout.FloatField("Follow duration", follower.followDuration);

            EditorGUILayout.PropertyField(wrapMode);


            if (follower.motion.applyRotation) follower.applyDirectionRotation = EditorGUILayout.Toggle("Face Direction", follower.applyDirectionRotation);


            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Start Position", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(autoStartPosition, new GUIContent("Project"));
            EditorGUILayout.BeginHorizontal();
            EditorGUIUtility.labelWidth = 100f;
            if (!follower.autoStartPosition && !Application.isPlaying)
            {
                EditorGUILayout.PropertyField(startPosition, new GUIContent("Start Position"));
                if(GUILayout.Button("Set Distance", GUILayout.Width(85)))
                {
                    DistanceWindow w = EditorWindow.GetWindow<DistanceWindow>(true);
                    w.Init(OnSetDistance, follower.CalculateLength());
                }
            }
            else EditorGUILayout.LabelField("Start position", GUILayout.Width(EditorGUIUtility.labelWidth));
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                if (!Application.isPlaying && follower.spline.sampleCount > 0)
                {
                    if (!follower.autoStartPosition)
                    {
                        follower.SetPercent(startPosition.floatValue);
                        if (!follower.follow) SceneView.RepaintAll();
                    }
                }
            }
            base.BodyGUI();
        }


        protected override void OnSceneGUI()
        {
            base.OnSceneGUI();
            SplineFollower user = (SplineFollower)target;
            if (user == null) return;
            if (Application.isPlaying)
            {
                if (!user.follow) DrawResult(user.modifiedResult);
                return;
            }
            if (user.spline == null) return;
            if (user.autoStartPosition)
            {
                user.spline.Project(result, user.transform.position, user.clipFrom, user.clipTo);
                DrawResult(result);
            } else if(!user.follow) DrawResult(user.result);
            
        }
    }
}
