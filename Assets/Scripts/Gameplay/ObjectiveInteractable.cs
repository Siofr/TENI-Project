using UnityEngine;

public class ObjectiveInteractable : BaseInteractable
{
    public MinigameManager minigameManager;

    public override void Activate()
    {
        // Decrease a counter in game manager here
        minigameManager.ObjectiveCount -= 1;

        // Also activate an animation when we have it
    }
}
