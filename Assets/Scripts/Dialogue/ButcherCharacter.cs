using UnityEngine;

public class ButcherCharacter : MonoBehaviour
{
    public override void PlayAnimation(string animName)
    {
        if (!CheckAnimationParameter(animName))
        {
            Debug.Log(string.Format("{0} is not a valid animation", animName));
            return;
        }

        anim.SetTrigger(animName);
    }
}
