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

        newBubbleScript.bubbleText.text = dialogueLine.Text.Text;
        onDialogueLineFinished();
    }
}
