using UnityEngine;
using System.Collections;
using DG.Tweening;

public class VignetteButcher : VignetteBase
{
    public Transform commandBubble;
    public ButcherBubble butcherBubble;

    public override void Start()
    {
        base.Start();
        base._currentMinigame = this.gameObject;
    }

    public override void StartMinigame()
    {
        base.StartMinigame();

        ShowNextItem();
    }

    public void ShowNextItem()
    {
        if (base.objectiveInteractableIndex == base.objectiveInteractables.Length)
        {
            return;
        }

        Sprite objectiveSprite = base.objectiveInteractables[objectiveInteractableIndex].GetComponent<SpriteRenderer>().sprite;
        butcherBubble.ShowBubble(objectiveSprite);
    }
}
