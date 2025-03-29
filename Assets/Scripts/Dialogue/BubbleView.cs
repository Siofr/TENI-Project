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
    private BubbleObject _newBubbleScript;

    private IEnumerator coroutine;

    private void Start()
    {
        _startPosition = _rectTransform.anchoredPosition.y;
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        
        float rectTransformPreviousHeight = _rectTransform.sizeDelta.y;
        GameObject newBubble = Instantiate(_bubblePrefab, this.transform);
        _newBubbleScript = newBubble.GetComponent<BubbleObject>();

        if (dialogueLine.CharacterName != "Doctor")
        {

        }

        // if (rectTransformPreviousHeight != 0)
        //{
        //    _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, _startPosition + (_rectTransform.sizeDelta.y - rectTransformPreviousHeight / 2));
        //}

        _newBubbleScript.StartSpeechBubble(dialogueLine.TextWithoutCharacterName.Text);
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (_newBubbleScript != null)
        {
            _newBubbleScript.SkipText(dialogueLine.TextWithoutCharacterName.Text);
        }

        onDialogueLineFinished();
    }
}
