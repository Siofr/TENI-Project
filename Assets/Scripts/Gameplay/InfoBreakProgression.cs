using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InfoBreakProgression : MonoBehaviour
{
    [SerializeField] private List<GameObject> Textpanels; 
    private int currentIndex = -1;

    void Start()
    {
        HideAllPanels();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvancePanel();
        }
    }

    void AdvancePanel()
    {
        currentIndex++;

        HideAllPanels();

        if (currentIndex < Textpanels.Count)
        {
            Textpanels[currentIndex].SetActive(true);
        }
        else
        {
            Debug.Log("Reached the end of info break progression.");
        }
    }

    void HideAllPanels()
    {
        foreach (var panel in Textpanels)
        {
            panel.SetActive(false);
        }
    }
}