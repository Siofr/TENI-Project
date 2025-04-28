using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CursorHandler cursorHandler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cursorHandler = GameObject.FindWithTag("UIManager").GetComponent<CursorHandler>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cursorHandler.CurrentState = CursorHandler.CursorState.HOVER;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cursorHandler.CurrentState = CursorHandler.CursorState.IDLE;
    }
}
