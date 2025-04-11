using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class MainPanel : MonoBehaviour
{
    [System.Serializable]
    public struct MenuButton
    {
        public Button button;
        public CanvasGroup can;
    }

    public MenuButton[] menuButtons;

    [SerializeField] private float _fadeTime;

    // Main Panel needs to be able to Quit the game, Start the game, and open the settings panel
    [SerializeField] private FadeMaterialManager _fadeMaterialManager;

    private void Start()
    {
        foreach (MenuButton item in menuButtons)
        {
            item.button.onClick.AddListener(delegate {
                ShowHideMenuItem(item.can);
            });
        }
    }
    
    void StartGame()
    {

    }

    void ShowHideMenuItem(CanvasGroup panel)
    {
        if (panel.alpha <= 0)
        {
            FadePanelIn(panel);
            return;
        }
        FadePanelOut(panel);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void FadePanelOut(CanvasGroup panel)
    {
        panel.DOKill();
        panel.DOFade(0, _fadeTime);
    }

    void FadePanelIn(CanvasGroup panel)
    {
        panel.DOKill();
        panel.DOFade(1, _fadeTime);
    }
}
