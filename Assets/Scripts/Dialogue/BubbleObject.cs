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
        for (int i = 0; i <= textToShow.Length; i++)
        {
            // Text fade in
            var tempText = textToShow;
            tempText = tempText.Insert(i, "<alpha=#00>");
            tempText = tempText.Insert(Mathf.Max(i -1, 0), "<alpha=#44>");
            tempText = tempText.Insert(Mathf.Max(i -2, 0), "<alpha=#66>");

            bubbleText.text = tempText;
            yield return new WaitForSeconds(0.035f);
        }
        bubbleText.text = textToShow;
        yield return null;
    }
    public void SkipText(string textToShow)
    {
        StopCoroutine(coroutine);
        bubbleText.text = textToShow;
    }
}
