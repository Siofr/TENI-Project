using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;

    private InputSystem_Actions inputActions;
    private InputAction interact;

    private void Start()
    {
        sceneCamera = Camera.main;
    }

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        interact = inputActions.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
    }

    private void OnDisable()
    {
        interact.Disable();
    }

    void Interact(InputAction.CallbackContext content)
    {
        Debug.Log("Clicked");
        RaycastHit2D hit = Physics2D.Raycast(sceneCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit && hit.collider.gameObject.TryGetComponent<BaseInteractable>(out BaseInteractable interactable))
        {
            interactable.Activate();
        }
    }
}
