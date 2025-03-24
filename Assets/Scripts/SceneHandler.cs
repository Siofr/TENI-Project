using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneHandler : MonoBehaviour
{
    public List<GameObject> chapterPrefabs = new List<GameObject>();
    private int chapterCount;
    public int chapterNumber;
    public Camera mainCam;
    public Transform dialogueCamera;
    public List<Transform> vignetteCameras = new List<Transform>();

    public enum GameState
    {
        DIALOGUE,
        VIGNETTE
    }

    public GameState currentGameState = GameState.DIALOGUE;

    private void Start()
    {
        mainCam = Camera.main;
        chapterCount = chapterPrefabs.Count;
    }

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;

        switch (currentGameState)
        {
            case GameState.DIALOGUE:
                mainCam.transform.position = dialogueCamera.position;
                break;
            case GameState.VIGNETTE:
                mainCam.transform.position = vignetteCameras[chapterNumber].position;
                chapterPrefabs[chapterNumber].GetComponent<MinigameManager>().StartMinigame();
                break;
        }
    }
}
