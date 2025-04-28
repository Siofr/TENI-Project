using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SceneData", order = 1)]
public class SceneData : ScriptableObject
{
    public enum SceneType
    {
        DIALOGUE,
        VIGNETTE,
        EXTRA,
        CUTSCENE
    }

    public SceneType currentSceneType;

    // If its dialogue
    [HideInInspector] public string yarnNodeName;
    [HideInInspector] public int chapterNumber;

    // If its not dialogue
    [HideInInspector] public GameObject scenePrefab;
}
