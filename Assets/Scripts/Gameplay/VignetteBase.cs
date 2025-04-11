using UnityEngine;

public abstract class VignetteBase : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public int vignetteCount;
    public ObjectiveInteractable[] objectiveInteractables;

    public int ObjectiveCount
    {
        get
        {
            return objectiveCount;
        }
        set
        {
            objectiveCount = value;

            if (objectiveCount <= 0)
            {
                ObjectiveComplete();
            }
        }
    }

    private int objectiveCount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Transform vignette in transform)
        {
            vignetteCount += 1;
        }
        sceneHandler = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void StartMinigame();

    public void ObjectiveComplete()
    {
        sceneHandler.SwapSceneAnimation();
    }
}
