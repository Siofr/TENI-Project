using UnityEngine;
using System.Collections.Generic;

public class SculptorInteractable : BaseInteractable
{
    private int blockHealth = 3;

    public VignetteSculptor vignetteSculptor;

    public List<List<int>> blockPositions = new List<List<int>>();

    public void Awake()
    {
        vignetteSculptor = GetComponentInParent<VignetteSculptor>();
    }

    public override void Activate()
    {
        if (vignetteSculptor.CheckNeighboringBlocks(blockPositions))
        {
            blockHealth -= 1;
        }

        if (blockHealth <= 0)
        {
            // Destroy Block
            base.col.enabled = false;
            StartCoroutine(base.FadeAnimation());
            base.DropHead();

            vignetteSculptor.UpdateSculptureBlocks(blockPositions);

            if (!vignetteSculptor.CheckAllPositions())
            {
                minigameManager.ObjectiveComplete();
            }
        }
    }
}
