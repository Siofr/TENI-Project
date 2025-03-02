using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public DialogueSO[] dialogueObjects;
    public GameObject patientBubble;
    public GameObject doctorBubble;

    private SpeechBubble patientBubbleScript;
    private SpeechBubble doctorBubbleScript;

    public GameObject speechBubblePrefab;

    public int dialogueIndex;
    public ManagerState currentManagerState;

    public enum ManagerState
    {
        PROGRESSING,
        FINISHED
    }

    public void Awake()
    {
        patientBubbleScript = patientBubble.GetComponent<SpeechBubble>();
        doctorBubbleScript = doctorBubble.GetComponent<SpeechBubble>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentManagerState)
        {
            case ManagerState.PROGRESSING:
                if (dialogueObjects[dialogueIndex].speaker == 1)
                {
                    // Patient Dialogue
                    patientBubble.SetActive(true);
                }
                else
                {
                    // Doctor Dialogue
                    doctorBubble.SetActive(true);
                }
                Debug.Log("Manager Progressing");
                break;
            case ManagerState.FINISHED:
                Debug.Log("Manager Finished");
                break;
        }
    }

    void CreateSpeechBubblesHistory()
    {
        for (int i = 0; i <= dialogueObjects.Length; i++)
        {
            GameObject newSpeechBubble = Instantiate(speechBubblePrefab, transform.position, Quaternion.identity);
        }
    }
}
