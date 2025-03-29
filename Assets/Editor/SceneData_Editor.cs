using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneData))]
public class SceneData_Editor : Editor
{
    SerializedProperty currentSceneType;
    SerializedProperty scenePrefab;
    SerializedProperty yarnNodeName;

    private void OnEnable()
    {
        currentSceneType = serializedObject.FindProperty(nameof(SceneData.currentSceneType));
        scenePrefab = serializedObject.FindProperty(nameof(SceneData.scenePrefab));
        yarnNodeName = serializedObject.FindProperty(nameof(SceneData.yarnNodeName));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        switch(currentSceneType.intValue)
        {
            case 0:
                EditorGUILayout.PropertyField(yarnNodeName);
                break;
            case 1:
                EditorGUILayout.PropertyField(scenePrefab);
                break;
            case 2:
                EditorGUILayout.PropertyField(scenePrefab);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
