using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class BaseInteractable : MonoBehaviour
{
    private Collider2D col;
    public void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }

    public abstract void Activate();
}
