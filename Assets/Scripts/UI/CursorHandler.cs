using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CursorHandler : MonoBehaviour
{
    public enum CursorState
    {
        IDLE,
        HOVER
    }

    public enum CursorStyle
    {
        DEFAULT,
        PLANT,
        MEAT,
        STONE
    }

    private CursorState _currentState;
    public CursorState CurrentState 
    {
        set
        {
            _currentState = value;
            if (_currentState == CursorState.IDLE)
            {
                _cursorSprites = _currentCursorAnimation.cursorTexturesIdle;
            }
            else if (_currentState == CursorState.HOVER)
            {
                _cursorSprites = _currentCursorAnimation.cursorTexturesHover;
            }
        }
        get { return _currentState; } 
    }
    private CursorStyle _currentStyle;

    public CursorStyle CurrentStyle
    {
        set
        {
            _currentStyle = value;

            switch (_currentStyle)
            {
                case CursorStyle.PLANT:
                    _currentCursorAnimation = _cursorAnimations[1];
                    break;
                case CursorStyle.MEAT:
                    _currentCursorAnimation = _cursorAnimations[2];
                    break;
                case CursorStyle.STONE:
                    _currentCursorAnimation = _cursorAnimations[3];
                    break;
                default:
                    _currentCursorAnimation = _cursorAnimations[0];
                    break;
            }

            CurrentState = CurrentState;
        }
        get
        {
            return _currentStyle;
        }
    }

    [SerializeField] private List<CursorAnimation> _cursorAnimations;
    private CursorAnimation _currentCursorAnimation;
    
    private Sprite[] _cursorSprites;

    public float animationSpeed;

    void Start()
    {
        CurrentStyle = CursorStyle.DEFAULT;
        CurrentState = CursorState.IDLE;
    }

    private float frames = 0f;
    private int currentSprite = 0;
    void Update()
    {
        // DebugFunct();
        
        frames += Time.deltaTime;
        if (frames >= animationSpeed)
        {
            frames = 0f;
            currentSprite++;

            if (currentSprite >= _cursorSprites.Length)
            {
                currentSprite = 0;
            }
            
            Cursor.SetCursor(_cursorSprites[currentSprite].texture, Vector2.zero, CursorMode.Auto);
        }
        
    }

    private void SetActiveCursorAnimation(CursorAnimation cursorAnimation)
    {
        this._currentCursorAnimation = cursorAnimation;   
    }

    private void DebugFunct()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentState == CursorState.IDLE)
            {
                CurrentState = CursorState.HOVER;
            }
            else
            {
                CurrentState = CursorState.IDLE;
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CurrentStyle = CursorStyle.DEFAULT;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            CurrentStyle = CursorStyle.PLANT;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CurrentStyle = CursorStyle.MEAT;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            CurrentStyle = CursorStyle.STONE;
        }
    }

    [System.Serializable]
    public class CursorAnimation
    {
        public CursorStyle cursorStyle;
        public Sprite[] cursorTexturesIdle;
        public Sprite[] cursorTexturesHover;
    }
}
