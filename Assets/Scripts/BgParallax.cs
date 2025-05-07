using UnityEngine;

public class BgParallax : MonoBehaviour
{
    public float parallaxStrength = 1f;
    private float bgRatio;
    private Vector2 relativeMousePosition;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        bgRatio = (float) Screen.height  / (float) Screen.width;
    }

    void Update()
    {
        relativeMousePosition.x = Mathf.InverseLerp(0, Screen.width, Input.mousePosition.x) - 0.5f;
        relativeMousePosition.y = Mathf.InverseLerp(0, Screen.height, Input.mousePosition.y)  - 0.5f;
        
        transform.position =new Vector3(startPosition.x + parallaxStrength * relativeMousePosition.x, startPosition.y + bgRatio * parallaxStrength * relativeMousePosition.y, 0f);
    }
}
