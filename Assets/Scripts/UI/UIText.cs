using UnityEngine;
using TMPro;

public class UIText : MonoBehaviour
{
    private UIManager uiManager;
    private TMP_Text uiText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();

        if (uiManager != null)
        {
            uiText = GetComponent<TMP_Text>();
            uiManager.changeFont.AddListener(OnFontChanged);
            uiText.font = uiManager.CurrentFont;
        }
    }

    void OnFontChanged()
    {
        if (uiManager.DyslexicMode)
        {
            uiText.font = uiManager.dyslexicFont;
            return;
        }

        uiText.font = uiManager.defaultFont;
    }
}
