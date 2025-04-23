using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIDyslexiaToggle : MonoBehaviour
{
    public UIManager uiManager;
    public TMP_Text dyslexiaToggleText;
    public Button uiButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        uiButton.onClick.AddListener(ToggleFont);
    }

    public void ToggleFont()
    {
        if (uiManager.CurrentFont == uiManager.dyslexicFont)
        {
            uiManager.DyslexicMode = false;
            dyslexiaToggleText.text = "OFF";
            return;
        }
        uiManager.DyslexicMode = true;
        dyslexiaToggleText.text = "ON";
    }
}
