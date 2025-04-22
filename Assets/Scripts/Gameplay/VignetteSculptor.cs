using UnityEngine;
using System.Collections.Generic;

public class VignetteSculptor : VignetteBase
{
    [System.Serializable]
    public struct Row
    {
        public SculptorInteractable[] row;
    }

    public Row[] sculptureBlocks;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
    }

    public override void StartMinigame(SceneData minigameData)
    {
        base.StartMinigame(minigameData);

        for (int i = 0; i < sculptureBlocks.Length; i++)
        {
            for (int j = 0; j < sculptureBlocks[i].row.Length; j++)
            {
                List<int> blockPosition = new List<int> { i, j };
                sculptureBlocks[i].row[j].blockPositions.Add(blockPosition);
            }
        }
    }

    public bool CheckAllPositions()
    {
        for (int i = 0; i < sculptureBlocks.Length; i++)
        {
            for (int j = 0; j < sculptureBlocks[i].row.Length; j++)
            {
                if (sculptureBlocks[i].row[j] != null)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public void UpdateSculptureBlocks(List<List<int>> blocksToRemove)
    {
        for (int i = 0; i < blocksToRemove.Count; i++)
        {
            sculptureBlocks[blocksToRemove[i][0]].row[blocksToRemove[i][1]] = null;
        }
    }

    public bool CheckNeighboringBlocks(List<List<int>> blockPositions)
    {
        for (int i = 0; i < blockPositions.Count; i++)
        {
            if (blockPositions[i][0] - 1 < 0 || blockPositions[i][1] - 1 < 0)
            {
                return true;
            }

            if (blockPositions[i][1] + 1 > sculptureBlocks[blockPositions[i][0]].row.Length - 1 || blockPositions[i][0] + 1 > sculptureBlocks.Length - 1)
            {
                return true;
            }

            if (sculptureBlocks[blockPositions[i][0] - 1].row[blockPositions[i][1]] == null)
            {
                return true;
            }

            if (sculptureBlocks[blockPositions[i][0] + 1].row[blockPositions[i][1]] == null)
            {
                return true;
            }

            if (sculptureBlocks[blockPositions[i][0]].row[blockPositions[i][1] + 1] == null)
            {
                return true;
            }

            if (sculptureBlocks[blockPositions[i][0]].row[blockPositions[i][1] - 1] == null)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckBounds()
    {
        return true;
    }
}
