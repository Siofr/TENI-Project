using UnityEngine;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public GameObject _currentMinigame;
    public int vignetteCount;
    public int currentMinigameState;
    public SceneHandler sceneHandler;

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

    private void Start()
    {
        foreach (Transform vignette in transform)
        {
            vignetteCount += 1;
        }
        sceneHandler = GameObject.FindGameObjectWithTag("Manager").GetComponent<SceneHandler>();
    }

    public void StartMinigame(SceneData minigameData)
    {
        _currentMinigame = sceneHandler.sceneDatabase[minigameData];
        _currentMinigame.gameObject.SetActive(true);
        objectiveInteractables = _currentMinigame.GetComponentsInChildren<ObjectiveInteractable>();

        foreach (ObjectiveInteractable objective in objectiveInteractables)
        {
            objective.minigameManager = this;
            objectiveCount += 1;
        }
    }

    void ObjectiveComplete()
    {
        // _currentMinigame.gameObject.SetActive(false);
        sceneHandler.SwapSceneAnimation();
    }
}
