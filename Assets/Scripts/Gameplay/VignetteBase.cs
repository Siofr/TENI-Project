using UnityEngine;

public abstract class VignetteBase : MonoBehaviour
{
    public SceneHandler sceneHandler;
    public GameObject _currentMinigame;
    public int vignetteCount;
    public BaseInteractable[] objectiveInteractables;
    public int objectiveInteractableIndex;
    public bool changeSceneTrigger = false;

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
    public virtual void Start()
    {
        foreach (Transform vignette in transform)
        {
            vignetteCount += 1;
        }
        sceneHandler = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneHandler>();
    }

    public void Update()
    {
        if (changeSceneTrigger)
        {
            sceneHandler.SwapSceneAnimation();
        }
    }

    public virtual void StartMinigame()
    {
        // _currentMinigame = sceneHandler.sceneDatabase[minigameData];
        this.gameObject.SetActive(true);
        objectiveInteractables = this.transform.GetComponentsInChildren<BaseInteractable>();

        foreach (BaseInteractable objective in objectiveInteractables)
        {
            objective.minigameManager = this;
            objectiveCount += 1;
        }
    }

    public void ObjectiveComplete()
    {
        sceneHandler.minigameIndex += 1;
        // changeSceneTrigger = true;
        sceneHandler.SwapSceneAnimation();
    }
}
