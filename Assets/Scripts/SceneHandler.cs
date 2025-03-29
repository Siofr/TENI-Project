using UnityEngine;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] private SceneData[] sceneList;
    private int sceneListIndex;

    [SerializeField] private DialogueRunner _dialogueRunner;

    [SerializeField] private GameObject _vignetteContainer;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Transform _bubbleContainer;

    [SerializeField] private FadeMaterialManager _fadeMaterialManager;

    public Dictionary<SceneData, GameObject> vignetteDatabase = new Dictionary<SceneData, GameObject>();

    private MinigameManager _minigameManager;

    private Camera _mainCam;
    public Transform dialogueCamera;
    public Transform vignetteCamera;
    // public Transform extraCamera;

    public enum GameState
    {
        DIALOGUE,
        VIGNETTE,
        EXTRA
    }

    public GameState currentGameState = GameState.DIALOGUE;

    private void Start()
    {
        InstantiateVignettes();
        _mainCam = Camera.main;
        _minigameManager = _vignetteContainer.GetComponent<MinigameManager>();
        _mainCam.transform.position = dialogueCamera.position;
    }

    public void ChangeScene()
    {
        sceneListIndex += 1;

        switch (sceneList[sceneListIndex].currentSceneType)
        {
            case SceneData.SceneType.DIALOGUE:
                currentGameState = GameState.DIALOGUE;
                _dialogueUI.SetActive(true);
                _minigameManager._currentMinigame.gameObject.SetActive(false);
                _dialogueRunner.StartDialogue(sceneList[sceneListIndex].yarnNodeName);
                _mainCam.transform.position = dialogueCamera.position;
                break;
            case SceneData.SceneType.VIGNETTE:
                currentGameState = GameState.VIGNETTE;
                ClearDialogue();
                _dialogueUI.SetActive(false);
                _mainCam.transform.position = vignetteCamera.position;
                _minigameManager.StartMinigame(sceneList[sceneListIndex]);
                break;
            case SceneData.SceneType.EXTRA:
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

    private void InstantiateVignettes()
    {
        Vector3 spawnPosition = new Vector3(vignetteCamera.transform.position.x, vignetteCamera.transform.position.y, vignetteCamera.transform.position.z + 10);
        foreach (SceneData sceneData in sceneList)
        {
            if (sceneData.currentSceneType == SceneData.SceneType.VIGNETTE)
            {
                GameObject newVignette = Instantiate(sceneData.scenePrefab, spawnPosition, Quaternion.identity);
                vignetteDatabase.Add(sceneData, newVignette);
            }
        }
    }

    [YarnCommand("change_scene")]
    public void SwapSceneAnimation()
    {
        StartCoroutine(Fade());

        IEnumerator Fade()
        {
            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeIn(1.5f));

            ChangeScene();

            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeOut());
        }
    }
}
