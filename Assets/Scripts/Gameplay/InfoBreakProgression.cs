using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class InfoBreakProgression : MonoBehaviour
{
    [SerializeField] private List<GameObject> Textpanels; 
    private int currentIndex = -1;
    private SceneHandler sceneHandler;

    void Start()
    {
        HideAllPanels();
    }

    private void Awake()
    {
        sceneHandler = GameObject.FindWithTag("Manager").GetComponent<SceneHandler>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AdvancePanel();
        }
    }

    public void AdvancePanel()
    {
        currentIndex++;

        HideAllPanels();

        if (currentIndex > Textpanels.Count)
        {
            sceneHandler.SwapSceneAnimation();
        }
        else if(Textpanels.Count > currentIndex)
        {
            
            Textpanels[currentIndex].SetActive(true);
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