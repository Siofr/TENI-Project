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
    private UIManager uiManager;
    private Image _bubbleSprite;
    public CharacterBase activeCharacter;
    private BubbleObject _newBubbleScript;

    public void Start()
    {
        uiManager = GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
    }

    public override void RunLine(LocalizedLine dialogueLine, Action onDialogueLineFinished)
    {
        GameObject newBubble = Instantiate(_bubblePrefab, this.transform);
        if (uiManager != null)
            newBubble.GetComponentInChildren<TMP_Text>().font = uiManager.CurrentFont;
        _newBubbleScript = newBubble.GetComponent<BubbleObject>();

        if (dialogueLine.CharacterName != "Doctor")
        {
            if (AudioManager.instance != null)
                AudioManager.instance.PlayAudioClip("SpeechPopup1");

            activeCharacter.gameObject.GetComponent<Animator>().SetTrigger("talk");
        }

        if (dialogueLine.CharacterName == "Doctor")
        {
            if (AudioManager.instance != null)
                AudioManager.instance.PlayAudioClip("SpeechPopup2");

            _bubbleSprite = newBubble.GetComponent<Image>();
            activeCharacter.gameObject.GetComponent<Animator>().SetTrigger("idle");
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
            activeCharacter.gameObject.GetComponent<Animator>().SetTrigger("idle");
        }

        onDialogueLineFinished();
    }

    [YarnCommand("play_animation")]
    public void PlayAnimation(string animName)
    {
        activeCharacter.PlayAnimation(animName);
    }
    
    [YarnCommand("set_emotion_state")]
    public void SetEmotionState(string stateName)
    {
        activeCharacter.SetEmotionState(stateName);
    }

    private void FlipSpeechBubble(Image speechBubble, TextMeshProUGUI text)
    {
        // make doctor bubble darker
        speechBubble.color = new Color(0.863f, 0.898f, 0.922f);
        speechBubble.rectTransform.localScale = new Vector3(speechBubble.rectTransform.localScale.x * -1, speechBubble.rectTransform.localScale.y, speechBubble.rectTransform.localScale.y);
        text.rectTransform.localScale = new Vector3(text.rectTransform.localScale.x * -1, text.rectTransform.localScale.y, text.rectTransform.localScale.z);
    }
}
