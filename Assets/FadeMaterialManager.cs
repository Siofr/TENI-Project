using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeMaterialManager : MonoBehaviour
{
    private RawImage _rawImage;
    private Material _renderMat;
    public float updateSpeed;
    public float delayBeforeFadeIn;
    public bool fadeOutOnAwake;

    void Start()
    {
        _rawImage = GetComponent<RawImage>();
        _renderMat = _rawImage.material;
        if (fadeOutOnAwake)
        {
            FadeMaterialOut();
        }
    }

    public void FadeMaterialIn()
    {
        StartCoroutine(FadeIn(delayBeforeFadeIn));
    }

    public void FadeMaterialOut()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        AudioManager.instance.PlayAudioClip("TransitionOut");
        for (float i = 0f; i <= 1f; i += updateSpeed)
        {
            _renderMat.SetFloat("_Alpha", i);
            yield return new WaitForFixedUpdate();
        }
        _renderMat.SetFloat("_Alpha", 1f);
    }
    
    public IEnumerator FadeOut()
    {
        AudioManager.instance.PlayAudioClip("TransitionIn");
        for (float i = 1f; i >= 0f; i -= updateSpeed)
        {
            _renderMat.SetFloat("_Alpha", i);
            yield return new WaitForFixedUpdate();
        }
        _renderMat.SetFloat("_Alpha", 0f);
        
    }
}
