using UnityEngine;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;

public class SceneHandler : MonoBehaviour
{
    public int currentVignetteNumber;
    [SerializeField] private GameObject _vignetteContainer;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Transform _bubbleContainer;
    [SerializeField] private FadeMaterialManager _fadeMaterialManager;
    private MinigameManager _minigameManager;
    private int _chapterCount;

    private Camera _mainCam;
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
        _mainCam = Camera.main;
        _minigameManager = _vignetteContainer.GetComponent<MinigameManager>();
        _mainCam.transform.position = dialogueCamera.position;
    }

    [YarnCommand("start_vignette")]
    public void ChangeState()
    {
        switch (currentGameState)
        {
            case GameState.VIGNETTE:
                currentGameState = GameState.DIALOGUE;
                _dialogueUI.SetActive(true);
                _mainCam.transform.position = dialogueCamera.position;
                break;
            case GameState.DIALOGUE:
                currentGameState = GameState.VIGNETTE;
                ClearDialogue();
                _dialogueUI.SetActive(false);
                _mainCam.transform.position = vignetteCamera.position;
                _minigameManager.StartMinigame();
                break;
        }
    }

    private void ClearDialogue()
    {
        foreach (Transform bubble in _bubbleContainer)
        {
            Destroy(bubble.gameObject);
        }
    }
}
