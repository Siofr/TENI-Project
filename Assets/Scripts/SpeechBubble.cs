using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    public TMP_Text speechBubbleText;
    public string dialogue;
    public float displayRate;
    private float nextTick;
    private int i;

    public DialogueState currentState;


    public enum DialogueState
    {
        PROGRESSING,
        FINISHED
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case DialogueState.PROGRESSING:
                if (i == dialogue.Length)
                {
                    currentState = DialogueState.FINISHED;
                    break;
                }

                if (Time.time >= nextTick)
                {
                    speechBubbleText.text += dialogue[i];
                    nextTick = Time.time + displayRate;
                    i++;
                }

                Debug.Log("Progressing");
                break;
            case DialogueState.FINISHED:
                Debug.Log("Finished");
                break;
        }
    }

    public void SkipDialogue()
    {
        speechBubbleText.text = dialogue;
        currentState = DialogueState.FINISHED;
    }
}
