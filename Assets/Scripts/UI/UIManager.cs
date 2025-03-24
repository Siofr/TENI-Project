using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private TMP_Text[] textObjects;

    public TMP_FontAsset CurrentFont {
        get {
            return currentFont;
        }
        set
        {
            currentFont = value;
            // textObjects = GameObject.FindObjectsOfType(typeof(TMP_Text));
            foreach (TMP_Text textObject in textObjects) {
                textObject.font = value;
            }
        }
    }

    private TMP_FontAsset currentFont;
    public bool dyslexicMode = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
