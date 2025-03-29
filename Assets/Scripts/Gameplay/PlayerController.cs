using UnityEngine;
using Yarn.Unity;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera sceneCamera;
    [SerializeField] private SceneHandler _sceneHandler;
    [SerializeField] private BubbleView _bubbleView;

    private InputSystem_Actions _inputActions;
    private InputAction _interact;

    private void Start()
    {
        sceneCamera = Camera.main;
    }

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _interact = _inputActions.Player.Interact;
        _interact.Enable();
        _interact.performed += Interact;
    }

    private void OnDisable()
    {
        _interact.Disable();
    }

    void Interact(InputAction.CallbackContext content)
    {
        switch (_sceneHandler.currentGameState)
        {
            case SceneHandler.GameState.DIALOGUE:

                _bubbleView.requestInterrupt();
                break;
            case SceneHandler.GameState.VIGNETTE:
                RaycastHit2D hit = Physics2D.Raycast(sceneCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

                if (hit && hit.collider.gameObject.TryGetComponent<BaseInteractable>(out BaseInteractable interactable))
                {
                    interactable.Activate();
                }
                break;
            case SceneHandler.GameState.EXTRA:
                _sceneHandler.SwapSceneAnimation();
                break;
        }
    }
}
