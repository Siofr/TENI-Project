using UnityEngine;
using UnityEngine.UI;
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

    void Start()
    {
        foreach (MenuButton item in menuButtons)
        {
            item.button.onClick.AddListener(delegate { 
                ShowHideMenuItem(item.go);
            });
        }
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
}
