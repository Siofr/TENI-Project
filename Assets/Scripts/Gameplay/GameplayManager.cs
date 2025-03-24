using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

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
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        ObjectiveCount = GameObject.FindGameObjectsWithTag("Objective").Length;
    }

    void ObjectiveComplete()
    {
        // What to do here when all of the objectives are complete
        Debug.Log("Objective Complete!");
    }
}
