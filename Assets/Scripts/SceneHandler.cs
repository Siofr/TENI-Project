using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneHandler : MonoBehaviour
{
    public GameObject vignetteContainer;
    public FadeMaterialManager fadeMaterialManager;
    private MinigameManager _minigameManager;
    private int _chapterCount;
    public int currentVignetteNumber;

    public Camera mainCam;
    public Transform dialogueCamera;
    public Transform vignetteCamera;

    public enum GameState
    {
        DIALOGUE,
        VIGNETTE
    }

    public GameState currentGameState = GameState.DIALOGUE;

    private void Start()
    {
        mainCam = Camera.main;
        _minigameManager = vignetteContainer.GetComponent<MinigameManager>();
        mainCam.transform.position = dialogueCamera.position;
    }

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;
        fadeMaterialManager.FadeMaterialIn();

        switch (currentGameState)
        {
            case GameState.DIALOGUE:
                mainCam.transform.position = dialogueCamera.position;
                break;
            case GameState.VIGNETTE:
                mainCam.transform.position = vignetteCamera.position;
                _minigameManager.StartMinigame();
                break;
        }
    }

    public void ChangeToMinigameTest()
    {
        ChangeState(GameState.VIGNETTE);
    }
}
