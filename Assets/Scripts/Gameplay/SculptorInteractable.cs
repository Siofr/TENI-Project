using UnityEngine;
using System.Collections.Generic;

public class SculptorInteractable : BaseInteractable
{
    public bool isOutside;

    private int blockHealth = 2;

    public VignetteSculptor vignetteSculptor;

    public List<List<int>> blockPositions = new List<List<int>>();
    public List<SculptorInteractable> neighbouringPieces = new List<SculptorInteractable>();

    public void Awake()
    {
        // vignetteSculptor = GetComponentInParent<VignetteSculptor>();
    }

    public override void Activate()
    {
        if (vignetteSculptor.sculptorBlocks[vignetteSculptor.currentIndex] == this)
        {
            blockHealth -= 1;
        }

        if (blockHealth <= 0)
        {
            // Destroy Block
            base.col.enabled = false;
            StartCoroutine(base.FadeAnimation());
            base.DropHead();

            minigameManager.ObjectiveCount -= 1;
            vignetteSculptor.UpdateActiveBlock();
            // UpdateNeighbouringPieces();

            // vignetteSculptor.UpdateSculptureBlocks(blockPositions);

            //if (!vignetteSculptor.CheckAllPositions())
            //{
            //    minigameManager.ObjectiveComplete();
            //}
        }
    }

    public void UpdateRockSprite()
    {

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
