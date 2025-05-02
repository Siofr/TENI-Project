using UnityEngine;
using System.Collections.Generic;

public class VignetteSculptor : VignetteBase
{
    private float maxCursorDistance = 10f;
    private float shakeAmount = 0.01f;
    private float shakeSpeed = 10f;

    public delegate void OnIncorrectGuess();
    public OnIncorrectGuess onIncorrectGuess;
    private Transform activeBlock;

    public List<SculptorInteractable> sculptorBlocks = new List<SculptorInteractable>();
    public int currentIndex;

    public override void Start()
    {
        base.Start();
    }

    public void Awake()
    {
        activeBlock = sculptorBlocks[0].transform;
    }

    public override void StartMinigame(SceneData minigameData)
    {
        base.StartMinigame(minigameData);
    }

    public void Update()
    {
        Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 blockPosition = new Vector2(activeBlock.position.x, activeBlock.position.y);
        float distanceValue = maxCursorDistance - Vector2.Distance(cursorPosition, blockPosition);

        if (distanceValue < 0)
            distanceValue = 0;

        float newRotationZ = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // activeBlock.localRotation = new Quaternion(activeBlock.localRotation.x, activeBlock.localRotation.y, newRotationZ, activeBlock.localRotation.w);

        activeBlock.Rotate(Vector3.forward, newRotationZ * distanceValue);
    }

    public void ResetSculpture ()
    {

    }

    public bool CheckBlock(SculptorInteractable block)
    {
        if (block == sculptorBlocks[currentIndex])
        {
            return true;
        }

        onIncorrectGuess();
        return false;
    }

    public void UpdateActiveBlock()
    {
        currentIndex += 1;
        activeBlock = sculptorBlocks[currentIndex].transform;
    }
}
