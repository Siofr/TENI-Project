using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectiveInteractable : BaseInteractable
{
    public override void Activate()
    {
        // Decrease a counter in game manager here
        minigameManager.ObjectiveCount -= 1;

        // Turn off collider
        base.col.enabled = false;
        
        StartCoroutine(base.FadeAnimation());
        base.DropHead();
    }
}
