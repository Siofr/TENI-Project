using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class ObjectiveInteractable : BaseInteractable
{
    public MinigameManager minigameManager;
    private float updateSpeed = 0.03f;
    private Collider2D _coll;
    private float _rotateSpeed;
    private SpriteRenderer _spriteRenderer;

    public void Awake()
    {
        _coll = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public override void Activate()
    {
        // Decrease a counter in game manager here
        minigameManager.ObjectiveCount -= 1;

        // Turn off collider
        _coll.enabled = false;
        
        StartCoroutine(FadeAnimation());
        DropHead();
    }

    private void DropHead()
    {
        var newRb = gameObject.AddComponent<Rigidbody2D>();
        newRb.AddForce( new Vector2( Random.Range(-1f, 1f),  1f ) * 100);
    }

    IEnumerator FadeAnimation()
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

    IEnumerator FallAnimation()
    {
        transform.Rotate(Vector3.forward * updateSpeed);

        yield return null;
    }
}
