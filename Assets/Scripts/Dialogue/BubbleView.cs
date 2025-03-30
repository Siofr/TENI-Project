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
    [SerializeField] private GameObject _speaker;
    private Image _bubbleSprite;
    private Animator _speakerAnim;
    private BubbleObject _newBubbleScript;

    private void Start()
    {
        _speakerAnim = _speaker.GetComponent<Animator>();
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        
        float rectTransformPreviousHeight = _rectTransform.sizeDelta.y;
        GameObject newBubble = Instantiate(_bubblePrefab, this.transform);
        _newBubbleScript = newBubble.GetComponent<BubbleObject>();

        if (dialogueLine.CharacterName != "Doctor")
        {
            _speakerAnim.SetBool("isTalking", true);
        }
        else
        {
            _bubbleSprite = newBubble.GetComponent<Image>();
            FlipSpeechBubble(_bubbleSprite, _newBubbleScript.bubbleText);
        }

        _newBubbleScript.StartSpeechBubble(dialogueLine.TextWithoutCharacterName.Text);
    }

    public override void InterruptLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        if (_newBubbleScript != null)
        {
            _newBubbleScript.SkipText(dialogueLine.TextWithoutCharacterName.Text);
        }

        if (dialogueLine.CharacterName != "Doctor")
        {
            _speakerAnim.SetBool("isTalking", false);
        }

        onDialogueLineFinished();
    }

    private void FlipSpeechBubble(Image speechBubble, TextMeshProUGUI text)
    {
        speechBubble.rectTransform.localScale = new Vector3(speechBubble.rectTransform.localScale.x * -1, speechBubble.rectTransform.localScale.y, speechBubble.rectTransform.localScale.y);
        text.rectTransform.localScale = new Vector3(text.rectTransform.localScale.x * -1, text.rectTransform.localScale.y, text.rectTransform.localScale.z);
    }
}
