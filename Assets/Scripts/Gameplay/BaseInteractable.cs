using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public abstract class BaseInteractable : MonoBehaviour
{
    public VignetteBase minigameManager;
    public Collider2D col;
    private SpriteRenderer _spriteRenderer;
    private float updateSpeed = 0.03f;

    public void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        minigameManager = GetComponentInParent<VignetteBase>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public abstract void Activate();

    public IEnumerator FadeAnimation()
    {
        Color tempColor = _spriteRenderer.color;

        for (float i = 1.0f; i >= 0f; i -= updateSpeed)
        {
            tempColor.a = i;
            _spriteRenderer.color = tempColor;
            yield return new WaitForFixedUpdate();
        }

        _spriteRenderer.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
    }

    public void DropHead()
    {
        var newRb = gameObject.AddComponent<Rigidbody2D>();
        newRb.AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * 100);
    }
}
