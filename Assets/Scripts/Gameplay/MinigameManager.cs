using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
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

    public void StartMinigame()
    {
        transform.GetChild(currentMinigameState).gameObject.SetActive(true);
        objectiveInteractables = transform.GetChild(currentMinigameState).GetComponentsInChildren<ObjectiveInteractable>();

        foreach (ObjectiveInteractable objective in objectiveInteractables)
        {
            objective.minigameManager = this;
            objectiveCount += 1;
        }
    }

    void ObjectiveComplete()
    {
        transform.GetChild(currentMinigameState).gameObject.SetActive(false);
        currentMinigameState += 1;
        sceneHandler.ChangeState(SceneHandler.GameState.DIALOGUE);
    }
}
