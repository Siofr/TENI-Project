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
    [SerializeField] private GameObject _extraContainer;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Transform _bubbleContainer;

    [SerializeField] private FadeMaterialManager _fadeMaterialManager;

    public Dictionary<SceneData, GameObject> sceneDatabase = new Dictionary<SceneData, GameObject>();

    private MinigameManager _minigameManager;

    private Camera _mainCam;
    public Transform dialogueCamera;
    public Transform vignetteCamera;
    public Transform extraCamera;

    public enum GameState
    {
        DIALOGUE,
        VIGNETTE,
        EXTRA
    }

    public GameState currentGameState = GameState.DIALOGUE;

    private void Start()
    {
        InstantiateAssets();
        _mainCam = Camera.main;
        _minigameManager = _vignetteContainer.GetComponent<MinigameManager>();
        _mainCam.transform.position = dialogueCamera.position;
    }

    public void ChangeScene()
    {
        // sceneDatabase[sceneList[sceneListIndex]].SetActive(false);
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
                currentGameState = GameState.EXTRA;
                ClearDialogue();
                _dialogueUI.SetActive(false);
                sceneDatabase[sceneList[sceneListIndex]].SetActive(true);
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

    private void InstantiateAssets()
    {
        
        foreach (SceneData sceneData in sceneList)
        {
            switch(sceneData.currentSceneType)
            {
                case SceneData.SceneType.DIALOGUE:
                    // Vector3 characterSpawnPosition = new Vector3(vignetteCamera.transform.position.x, vignetteCamera.transform.position.y, dialogueCamera.transform.position.z + 10);
                    // GameObject newCharacter = Instantiate(sceneData.character, characterSpawnPosition, Quaternion.identity);
                    // sceneDatabase.Add(sceneData, newCharacter);
                    break;
                case SceneData.SceneType.VIGNETTE:
                    Vector3 sceneSpawnPosition = new Vector3(vignetteCamera.transform.position.x, vignetteCamera.transform.position.y, vignetteCamera.transform.position.z + 10);
                    GameObject newVignette = Instantiate(sceneData.scenePrefab, sceneSpawnPosition, Quaternion.identity);
                    sceneDatabase.Add(sceneData, newVignette);
                    break;
                case SceneData.SceneType.EXTRA:
                    GameObject newScene = Instantiate(sceneData.scenePrefab, _extraContainer.transform);
                    sceneDatabase.Add(sceneData, newScene);
                    break;
            }
        }
    }

    [YarnCommand("change_scene")]
    public void SwapSceneAnimation()
    {
        StartCoroutine(Fade());

        IEnumerator Fade()
        {
            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeIn(0.5f));

            yield return new WaitForSeconds(1.5f);

            ChangeScene();

            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeOut());
        }
    }
}
