using UnityEngine;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameState currentGameState = GameState.DIALOGUE;
    public Dictionary<SceneData, GameObject> sceneDatabase = new Dictionary<SceneData, GameObject>();
    public GameObject[] officeScenes;
    private CursorHandler cursorHandler;

    [Header("Scene Scriptable Object List")]
    [SerializeField] private SceneData[] sceneList;
    private int sceneListIndex;
    public int minigameIndex;

    [Header("UI Variables")]
    [SerializeField] private DialogueRunner _dialogueRunner;
    [SerializeField] private GameObject _vignetteContainer;
    [SerializeField] private GameObject _extraContainer;
    [SerializeField] private GameObject _cutsceneContainer;
    [SerializeField] private GameObject _dialogueUI;
    [SerializeField] private Transform _bubbleContainer;
    [SerializeField] private BubbleView _bubbleView;
    [SerializeField] private FadeMaterialManager _fadeMaterialManager;
    private bool isSceneChangeActive = false;
    private Transform currentScene;

    private VignetteBase _minigameManager;

    [Header("Camera Positions")]
    private Camera _mainCam;
    // public Transform dialogueCamera;
    public Transform vignetteCamera;
    public Transform cutsceneCamera;

    public enum GameState
    {
        DIALOGUE,
        VIGNETTE,
        EXTRA,
        CUTSCENE
    }


    private void Start()
    {
        InstantiateAssets();
        _mainCam = Camera.main;
        _bubbleView.activeCharacter = sceneDatabase[sceneList[sceneListIndex]].GetComponentInChildren<CharacterBase>();
        _minigameManager = _vignetteContainer.GetComponent<VignetteBase>();
        cursorHandler = GameObject.FindWithTag("UIManager").GetComponent<CursorHandler>();
        // _mainCam.transform.position = dialogueCamera.position;
    }

    public void ChangeScene()
    {
        //if (sceneDatabase.ContainsKey(sceneList[sceneListIndex]))
        //{
        //    sceneDatabase[sceneList[sceneListIndex]].SetActive(false);
        //}

        if (sceneListIndex + 1 > sceneList.Length - 1)
        {
            ReturnToMenu();
            return;
        }

        sceneListIndex += 1;
        SceneData sceneData = sceneList[sceneListIndex];
        
        switch (sceneList[sceneListIndex].currentSceneType)
        {
            case SceneData.SceneType.DIALOGUE:
                currentGameState = GameState.DIALOGUE;

                _dialogueUI.SetActive(true);
                _vignetteContainer.transform.GetChild(minigameIndex).gameObject.SetActive(false);
                _dialogueRunner.StartDialogue(sceneList[sceneListIndex].yarnNodeName);
                currentScene.gameObject.SetActive(false);
                _bubbleView.activeCharacter = sceneDatabase[sceneList[sceneListIndex]].GetComponentInChildren<CharacterBase>();

                Vector3 newCamPosition = sceneDatabase[sceneList[sceneListIndex]].transform.position;
                _mainCam.transform.position = new Vector3(newCamPosition.x, newCamPosition.y, newCamPosition.z - 10);

                break;
            case SceneData.SceneType.VIGNETTE:
                currentGameState = GameState.VIGNETTE;

                ClearDialogue();
                _dialogueUI.SetActive(false);
                _mainCam.transform.position = vignetteCamera.position;
                currentScene = _vignetteContainer.transform.GetChild(minigameIndex);
                _vignetteContainer.transform.GetChild(minigameIndex).GetComponent<VignetteBase>().StartMinigame();
                break;
            case SceneData.SceneType.EXTRA:
                currentGameState = GameState.EXTRA;

                ClearDialogue();
                currentScene.gameObject.SetActive(false);
                _dialogueUI.SetActive(false);
                currentScene = sceneDatabase[sceneList[sceneListIndex]].transform;
                sceneDatabase[sceneList[sceneListIndex]].SetActive(true);
                break;
            case SceneData.SceneType.CUTSCENE:
                currentGameState = GameState.CUTSCENE;

                ClearDialogue();
                _dialogueUI.SetActive(false);
                currentScene = sceneDatabase[sceneList[sceneListIndex]].transform;
                currentScene.gameObject.SetActive(true);
                _mainCam.transform.position = _cutsceneContainer.transform.position;
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
                    // Vector3 characterSpawnPosition = characterPosition.position;
                    // GameObject newCharacter = Instantiate(sceneData.scenePrefab, characterSpawnPosition, Quaternion.identity);
                    sceneDatabase.Add(sceneData, officeScenes[sceneData.chapterNumber]);
                    break;
                case SceneData.SceneType.VIGNETTE:
                    // Vector3 sceneSpawnPosition = new Vector3(vignetteCamera.transform.position.x, vignetteCamera.transform.position.y, vignetteCamera.transform.position.z + 10);
                    GameObject newVignette = Instantiate(sceneData.scenePrefab, _vignetteContainer.transform);
                    sceneDatabase.Add(sceneData, newVignette);
                    break;
                case SceneData.SceneType.EXTRA:
                    GameObject newScene = Instantiate(sceneData.scenePrefab, _extraContainer.transform);
                    sceneDatabase.Add(sceneData, newScene);
                    break;
                case SceneData.SceneType.CUTSCENE:
                    GameObject newCutscene = Instantiate(sceneData.scenePrefab, _cutsceneContainer.transform);
                    sceneDatabase.Add(sceneData, newCutscene);
                    break;

            }
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    [YarnCommand("change_scene")]
    public void SwapSceneAnimation()
    {
        if(!isSceneChangeActive)
            StartCoroutine(Fade());
        

        IEnumerator Fade()
        {
            isSceneChangeActive = true;
            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeIn(0.5f));

            yield return new WaitForSeconds(1.5f);

            ChangeScene();

            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeOut());
            isSceneChangeActive = false;
        }
    }

    private Transform FindChildWithTag(Transform parent, string tag)
    {
        foreach (Transform c in parent)
        {
            if (c.gameObject.tag == tag)
            {
                return c;
            }
        }

        return null;
    }
}
