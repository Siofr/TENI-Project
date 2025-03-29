using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectiveInteractable : BaseInteractable
{
    public MinigameManager minigameManager;
    private float updateSpeed = 0.15f;
    private Collider2D _coll;
    private float _rotateSpeed;
    private Color _spriteColor;

    public void Awake()
    {
        _coll = GetComponent<Collider2D>();
        _spriteColor = GetComponent<SpriteRenderer>().color;
    }

    public override void Activate()
    {
        // Decrease a counter in game manager here
        minigameManager.ObjectiveCount -= 1;

        // Turn off collider
        _coll.enabled = false;

        // Also activate an animation when we have it
        StartCoroutine(FadeAnimation());
    }

    IEnumerator FadeAnimation()
    {
        for (float i = 1.0f; i >= 0f; i -= updateSpeed)
        {
            _spriteColor.a = i;
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator FallAnimation()
    {
        transform.Rotate(Vector3.forward * updateSpeed);

        yield return null;
    }
}
