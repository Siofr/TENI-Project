using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeMaterialManager : MonoBehaviour
{
    private RawImage _rawImage;
    private Material _renderMat;
    public float updateSpeed;
    public float delayBeforeFadeIn;

    void Start()
    {
        _rawImage = GetComponent<RawImage>();
        _renderMat = _rawImage.material;
    }

    public void FadeMaterialIn()
    {
        StartCoroutine(FadeIn(delayBeforeFadeIn));
    }

    public void FadeMaterialOut()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        
        for (float i = 0f; i <= 1f; i += updateSpeed)
        {
            _renderMat.SetFloat("_Alpha", i);
            yield return new WaitForFixedUpdate();
        }
        _renderMat.SetFloat("_Alpha", 1f);
    }
    
    private IEnumerator FadeOut()
    {
        for (float i = 1f; i >= 0f; i -= updateSpeed)
        {
            _renderMat.SetFloat("_Alpha", i);
            yield return new WaitForFixedUpdate();
        }
        _renderMat.SetFloat("_Alpha", 0f);
        
    }
}
