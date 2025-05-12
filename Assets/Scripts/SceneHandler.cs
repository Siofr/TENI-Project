using UnityEngine;
using Yarn.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public GameState currentGameState = GameState.CUTSCENE;
    public Dictionary<SceneData, GameObject> sceneDatabase = new Dictionary<SceneData, GameObject>();
    public GameObject[] officeScenes;
    private CursorHandler cursorHandler;

    [Header("Scene Scriptable Object List")]
    [SerializeField] private SceneData[] sceneList;
    private int sceneListIndex = -1;
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
    public Transform currentScene;
    private SceneData sceneData;

    private VignetteBase _minigameManager;

    [Header("Camera Positions")]
    private Camera _mainCam;
    // public Transform dialogueCamera;
    public Transform vignetteCamera;
    public Transform cutsceneCamera;
    private int sceneChangeQueue = 0;

    public enum GameState
    {
        DIALOGUE,
        VIGNETTE,
        EXTRA,
        CUTSCENE
    }

    public void Update()
    {
        if (sceneChangeQueue > 0)
        {
            PlaySwap();
        }
    }


    private void Start()
    {
        InstantiateAssets();
        _mainCam = Camera.main;
        _bubbleView.activeCharacter = sceneDatabase[sceneList[sceneListIndex + 2]].GetComponentInChildren<CharacterBase>();
        _minigameManager = _vignetteContainer.GetComponent<VignetteBase>();
        cursorHandler = GameObject.FindWithTag("UIManager").GetComponent<CursorHandler>();
        // sceneData = sceneList[sceneListIndex];
        ChangeScene();
        // ChangeAmbientAudio(sceneData);
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
        sceneData = sceneList[sceneListIndex];
        ChangeCursorStyle(sceneData);
        ChangeAmbientAudio(sceneData);

        switch (sceneList[sceneListIndex].currentSceneType)
        {
            case SceneData.SceneType.DIALOGUE:
                currentGameState = GameState.DIALOGUE;
                _dialogueUI.SetActive(true);
                _vignetteContainer.transform.GetChild(minigameIndex).gameObject.SetActive(false);
                if (sceneList[sceneListIndex].yarnNodeName != null)
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

                if (currentScene)
                    currentScene.gameObject.SetActive(false);

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

    public void ChangeAmbientAudio(SceneData sceneData)
    {
        if (AudioManager.instance != null)
            AudioManager.instance.FadeIn(sceneData.ambientAudioName);
    }

    public void ChangeCursorStyle(SceneData currentSceneData)
    {
        if (currentSceneData.currentSceneType != SceneData.SceneType.VIGNETTE)
        {
            cursorHandler.CurrentStyle = CursorHandler.CursorStyle.DEFAULT;
            return;
        }

        switch (currentSceneData.currentVignetteType)
        {
            case SceneData.VignetteType.PLANT:
                cursorHandler.CurrentStyle = CursorHandler.CursorStyle.PLANT;
                break;
            case SceneData.VignetteType.BUTCHER:
                cursorHandler.CurrentStyle = CursorHandler.CursorStyle.MEAT;
                break;
            case SceneData.VignetteType.SCULPTOR:
                cursorHandler.CurrentStyle = CursorHandler.CursorStyle.STONE;
                break;
            default:
                cursorHandler.CurrentStyle = CursorHandler.CursorStyle.DEFAULT;
                break;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    [YarnCommand("change_scene")]
    public void SwapSceneAnimation()
    {
        if (currentGameState != GameState.EXTRA)
        {
            sceneChangeQueue += 1;
        }
        else
        {
            sceneChangeQueue = 1;
        }
    }

    public void PlaySwap()
    {
        if (!isSceneChangeActive)
            StartCoroutine(Fade());

        IEnumerator Fade()
        {
            Debug.Log("Fade Start");
            isSceneChangeActive = true;

            if (AudioManager.instance != null)
                AudioManager.instance.FadeOut(sceneData.ambientAudioName);

            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeIn(0.5f));

            yield return new WaitForSeconds(1.5f);

            ChangeScene();
            Debug.Log("Change Scene");

            yield return _fadeMaterialManager.StartCoroutine(_fadeMaterialManager.FadeOut());
            isSceneChangeActive = false;
            sceneChangeQueue -= 1;
            Debug.Log("Fade End");
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
