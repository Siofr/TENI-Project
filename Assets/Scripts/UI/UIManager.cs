using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public UnityEvent changeFont = new UnityEvent();

    public TMP_FontAsset CurrentFont {
        get {
            return currentFont;
        }
        set
        {
            currentFont = value;
            changeFont.Invoke();
            if (dyslexicMode)
            {
                dyslexicMode = false;
                return;
            }
            dyslexicMode = true;
        }
    }

    public TMP_FontAsset defaultFont;
    public TMP_FontAsset dyslexicFont;
    private TMP_FontAsset currentFont;

    public bool DyslexicMode
    {
        get
        {
            return dyslexicMode;
        }
        set
        {
            dyslexicMode = value;

            if (dyslexicMode)
            {
                CurrentFont = dyslexicFont;
                return;
            }

            CurrentFont = defaultFont;
        }
    }

    private bool dyslexicMode;

    private void Awake()
    {
        changeFont.Invoke();
        if (!dyslexicMode)
        {
            CurrentFont = defaultFont;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
