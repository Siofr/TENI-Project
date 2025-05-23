using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneData))]
public class SceneData_Editor : Editor
{
    SerializedProperty currentSceneType;
    SerializedProperty ambientAudioName;
    SerializedProperty currentVignetteType;
    SerializedProperty scenePrefab;
    SerializedProperty yarnNodeName;
    SerializedProperty chapterNumber;

    private void OnEnable()
    {
        currentSceneType = serializedObject.FindProperty(nameof(SceneData.currentSceneType));
        ambientAudioName = serializedObject.FindProperty(nameof(SceneData.ambientAudioName));
        currentVignetteType = serializedObject.FindProperty(nameof(SceneData.currentVignetteType));
        scenePrefab = serializedObject.FindProperty(nameof(SceneData.scenePrefab));
        yarnNodeName = serializedObject.FindProperty(nameof(SceneData.yarnNodeName));
        chapterNumber = serializedObject.FindProperty(nameof(SceneData.chapterNumber));
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        serializedObject.Update();

        switch(currentSceneType.intValue)
        {
            case 0:
                EditorGUILayout.PropertyField(yarnNodeName);
                EditorGUILayout.PropertyField(chapterNumber);
                break;
            case 1:
                EditorGUILayout.PropertyField(currentVignetteType);
                EditorGUILayout.PropertyField(scenePrefab);
                break;
            case 2:
                EditorGUILayout.PropertyField(scenePrefab);
                break;
            case 3:
                EditorGUILayout.PropertyField(scenePrefab);
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }
}
