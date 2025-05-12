using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using UnityEngine.Video;

public class TutorialUILogic : MonoBehaviour
{
    public float fadeInSpeed = 1f;
    public float fadeOutSpeed = 1f;
    public float timeToPopup = 60f;
    
    private bool hasShown = false;
    
    public bool isMinigame = false;
    public bool isSculpture = false;

    private CanvasGroup _canvasGroup;
    
    private VignetteBase _vignette;
    private VignetteSculptor _vignetteSculpture;
    private int _startVignetteVal;
    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();

        if (isMinigame)
        {
            _vignette = FindObjectOfType<VignetteBase>();
            _startVignetteVal = _vignette.ObjectiveCount;
        }

        if (isSculpture)
        {
            _vignetteSculpture = FindObjectOfType<VignetteSculptor>();
            _startVignetteVal = _vignetteSculpture.currentIndex;
        }
    }

    IEnumerator FadeIn()
    {
        while (_canvasGroup.alpha < 1)
        {
            _canvasGroup.alpha += Time.deltaTime * fadeInSpeed;
            
            yield return null;
        }
    }
    
    IEnumerator FadeOut()
    {
        while (_canvasGroup.alpha > 0)
        {
            _canvasGroup.alpha -= Time.deltaTime * fadeOutSpeed;
            
            yield return null;
        }
    }

    private float timer = 0f;
    private bool hasInteracted = false;
    void Update()
    {
        InteractionCheck();

        if (!hasInteracted || !hasShown)
        {
            timer += Time.deltaTime;
            if (timer >= timeToPopup)
            {
                StartCoroutine(FadeIn());
                
                hasShown = true;
            }
        }
        else if (hasInteracted)
        {
            StopAllCoroutines();
            
            StartCoroutine(FadeOut());
        }
    }

    private void InteractionCheck()
    {
        if (!isMinigame)
        {
            if (Input.GetMouseButtonDown(0))
            {
                hasInteracted = true;
            }
        }
        else
        {
            if (isSculpture && _vignetteSculpture.currentIndex != _startVignetteVal)
            {
                hasInteracted = true;
            }
            if (_startVignetteVal != _vignette.ObjectiveCount)
            {
                hasInteracted = true;
            }
        }
    }
}
