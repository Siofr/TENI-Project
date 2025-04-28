using UnityEngine;
using UnityEngine.Playables;

public class ChapterStartCutscene : MonoBehaviour
{
    private SceneHandler sceneHandler;
    private PlayableDirector director;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        sceneHandler = GameObject.FindWithTag("Manager").GetComponent<SceneHandler>();
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        director.Play();
    }

    public void ChangeScene()
    {
        sceneHandler.SwapSceneAnimation();
    }
}
