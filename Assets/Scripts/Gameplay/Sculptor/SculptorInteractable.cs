using UnityEngine;
using System.Collections.Generic;

public class SculptorInteractable : BaseInteractable
{
    public Sprite[] spriteList;
    public bool isOutside;
    private int blockHealth = 1;

    public VignetteSculptor vignetteSculptor;

    public List<List<int>> blockPositions = new List<List<int>>();
    public List<SculptorInteractable> neighbouringPieces = new List<SculptorInteractable>();

    public override void Activate()
    {
        if (vignetteSculptor.CheckBlock(this))
        {
            blockHealth -= 1;
        }
        else
        {
            vignetteSculptor.CurrentHealth -= 1;
            if (AudioManager.instance != null)
                AudioManager.instance.PlayAudioClip("BadClick");
        }

        if (blockHealth <= 0)
        {
            // Destroy Block
            base.col.enabled = false;
            StartCoroutine(base.FadeAnimation());
            base.DropHead();

            vignetteSculptor.CheckProgress();
            vignetteSculptor.UpdateActiveBlock();
            vignetteSculptor.CurrentHealth = 2;
            // UpdateNeighbouringPieces();

            // vignetteSculptor.UpdateSculptureBlocks(blockPositions);

            //if (!vignetteSculptor.CheckAllPositions())
            //{
            //    minigameManager.ObjectiveComplete();
            //}
        }
    }

    public bool CheckNeighbouringPieces()
    {
        for (int i = 0; i < neighbouringPieces.Count; i++)
        {
            if (neighbouringPieces[i] == null)
            {
                return true;
            }
        }
        return false;
    }

    public void UpdateNeighbouringPieces()
    {
        for (int i = 0; i < neighbouringPieces.Count; i++)
        {
            neighbouringPieces[i].isOutside = true;
        }
    }
}
