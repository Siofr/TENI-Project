using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Yarn.Unity;

public class BubbleView : DialogueViewBase
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _bubblePrefab;
    [SerializeField] private float _textAppearanceTime;
    private float _startPosition;

    private void Start()
    {
        _startPosition = _rectTransform.anchoredPosition.y;
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        float rectTransformPreviousHeight = _rectTransform.sizeDelta.y;
        GameObject newBubble = Instantiate(_bubblePrefab, this.transform);
        BubbleObject newBubbleScript = newBubble.GetComponent<BubbleObject>();

        if (rectTransformPreviousHeight != 0)
        {
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _startPosition + (_rectTransform.sizeDelta.y - rectTransformPreviousHeight / 2));
        }

        StartCoroutine(PerformBubble());

        IEnumerator PerformBubble()
        {
            yield return newBubbleScript.StartCoroutine(newBubbleScript.ShowText(dialogueLine.TextWithoutCharacterName.Text));

            onDialogueLineFinished();
        }
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        onDialogueLineFinished();
    }
}
