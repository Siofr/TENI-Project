using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class BubbleView : DialogueViewBase
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float textAppearanceTime;

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        Debug.Log("Start Line");
        GameObject newBubble = Instantiate(bubblePrefab, this.transform);
        BubbleObject newBubbleScript = newBubble.GetComponent<BubbleObject>();

        StartCoroutine(PerformBubble());

        IEnumerator PerformBubble()
        {
            yield return newBubbleScript.StartCoroutine(newBubbleScript.ShowText(dialogueLine.TextWithoutCharacterName.Text));
        }
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        onDialogueLineFinished();
    }
}
