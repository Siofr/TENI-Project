using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class BubbleObject : MonoBehaviour
{
    public TextMeshProUGUI bubbleText;

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
        StopCoroutine(ShowText(""));

        bubbleText.text = textToShow;
    }
}
