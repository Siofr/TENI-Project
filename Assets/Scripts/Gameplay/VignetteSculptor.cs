using UnityEngine;

public class VignetteSculptor : VignetteBase
{
    private SculptorInteractable[] row;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void StartMinigame(SceneData minigameData)
    {
        base.StartMinigame(minigameData);

        foreach (ObjectiveInteractable obj in objectiveInteractables)
        {

        }
    }
}
