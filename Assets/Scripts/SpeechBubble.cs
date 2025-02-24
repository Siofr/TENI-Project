using UnityEngine;
using TMPro;

public class SpeechBubble : MonoBehaviour
{
    public TMP_Text speechBubbleText;
    public string dialogue;
    public float displayRate;

    private DialogueState currentState;

    public enum DialogueState
    {
        progress,
        finished
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case DialogueState.progress:
                Debug.Log("Progressing");
                break;
            case DialogueState.finished:
                Debug.Log("Finished");
                break;
        }
    }

    public void SkipDialogue()
    {
        speechBubbleText.text = dialogue;
        currentState = DialogueState.finished;
    }
}
