using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class BubbleObject : MonoBehaviour
{
    public TextMeshProUGUI bubbleText;
    private IEnumerator coroutine;

    public void StartSpeechBubble(string textToShow)
    {
        coroutine = ShowText(textToShow);
        StartCoroutine(coroutine);
    }

    public IEnumerator ShowText(string textToShow)
    {
        foreach (char c in textToShow)
        {
            bubbleText.text += c;
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    public void SkipText(string textToShow)
    {
        StopCoroutine(coroutine);
        bubbleText.text = textToShow;
    }
}
