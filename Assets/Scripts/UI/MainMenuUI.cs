using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MainMenuUI : MonoBehaviour
{
    [System.Serializable]
    public struct MenuButton
    {
        public Button button;
        public GameObject go;
    }

    public MenuButton[] menuButtons;
    public Button startGameButton, quitGameButton;
    public Button dyslexicModeButton;
    public TMP_Text dyslexicButtonText;
    public TMP_FontAsset readableFont, defaultFont;

    void Start()
    {
        foreach (MenuButton item in menuButtons)
        {
            item.button.onClick.AddListener(delegate { 
                ShowHideMenuItem(item.go);
            });
        }

        startGameButton.onClick.AddListener(StartGame);
        quitGameButton.onClick.AddListener(QuitGame);
        dyslexicModeButton.onClick.AddListener(DyslexicMode);
    }

    void ShowHideMenuItem(GameObject go)
    {
        Debug.Log(string.Format("Button Clicked: {0}", go.name));
        if (!go.activeSelf)
        {
            go.SetActive(true);
            return;
        }

        go.SetActive(false);
    }

    void StartGame()
    {

    }

    void QuitGame()
    {
        Application.Quit();
    }

    void DyslexicMode()
    {
        Debug.Log("Here");
        if (UIManager.instance.dyslexicMode)
        {
            UIManager.instance.CurrentFont = defaultFont;
            UIManager.instance.dyslexicMode = false;
            dyslexicButtonText.text = "OFF";
            return;
        }
        UIManager.instance.CurrentFont = readableFont;
        UIManager.instance.dyslexicMode = true;
        dyslexicButtonText.text = "ON";
    }
}
