using UnityEngine;
using System.Collections;

public class ButcherBubble : MonoBehaviour
{
    private SpriteRenderer objectiveDisplay;
    private float updateSpeed = 0.15f;
    private Animator bubbleAnim;
    private Sprite nextSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        objectiveDisplay = transform.GetChild(0).GetComponent<SpriteRenderer>();
        bubbleAnim = GetComponent<Animator>();
    }

    public void ShowBubble(Sprite nextItem)
    {
        nextSprite = nextItem;
        bubbleAnim.SetTrigger("ShowBubble");
    }

    public void HideBubble()
    {
        bubbleAnim.SetTrigger("HideBubble");
    }

    public void SwapSprite()
    {
        objectiveDisplay.sprite = nextSprite;
    }

    public IEnumerator FadeOutAnimation()
    {
        Color tempColor = objectiveDisplay.color;

        for (float i = 1.0f; i >= 0f; i -= updateSpeed)
        {
            tempColor.a = i;
            objectiveDisplay.color = tempColor;
            yield return new WaitForFixedUpdate();
        }

        objectiveDisplay.color = new Color(tempColor.r, tempColor.g, tempColor.b, 0);
    }

    public IEnumerator FadeInAnimation()
    {
        Color tempColor = objectiveDisplay.color;

        for (float i = 0.0f; i <= 1f; i += updateSpeed)
        {
            tempColor.a = i;
            objectiveDisplay.color = tempColor;
            yield return new WaitForFixedUpdate();
        }

        objectiveDisplay.color = new Color(tempColor.r, tempColor.g, tempColor.b, 1);
    }
}
