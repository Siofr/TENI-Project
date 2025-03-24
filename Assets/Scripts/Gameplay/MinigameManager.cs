using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MinigameManager : MonoBehaviour
{
    public int vignetteCount;
    public int currentMinigameState;

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
        StartMinigame();
        foreach (Transform vignette in transform)
        {
            vignetteCount += 1;
        }
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
        StartMinigame();
    }
}
