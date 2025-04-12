using UnityEngine;

public class ButcherInteractable : BaseInteractable
{
    public override void Activate()
    {
        if (base.minigameManager.objectiveInteractables[base.minigameManager.objectiveInteractableIndex] == this)
        {
            // Decrease a counter in game manager here
            minigameManager.ObjectiveCount -= 1;
            minigameManager.objectiveInteractableIndex += 1;

            // Turn off collider
            base.col.enabled = false;

            StartCoroutine(base.FadeAnimation());
            base.DropHead();
        }

        Debug.Log("Not correct");
    }
}
