using System;
using System.Collections;
using UnityEngine;

public class TextboxBgFadeIn : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    
    public float fadeInSpeed;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator FadeIn()
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        
        float alpha = spriteRenderer.color.a;

        while (alpha < 1f)
        {
            alpha += Time.deltaTime * fadeInSpeed;
            
            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);
            
            yield return null;
        }
        
        print("Fade in done");
    }
}
