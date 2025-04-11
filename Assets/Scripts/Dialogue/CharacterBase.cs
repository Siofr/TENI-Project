using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    public Animator anim;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public abstract void PlayAnimation(string animName);

    public AnimationClip FindAnimationClip(string animClipName)
    {
        foreach (AnimationClip animClip in anim.runtimeAnimatorController.animationClips)
        {
            if (animClip.name == animClipName)
            {
                return animClip;
            }
        }

        return null;
    }

    public bool CheckAnimationParameter(string animParamName)
    {
        foreach(AnimatorControllerParameter animParameter in anim.parameters)
        {
            if (animParameter.name == animParamName)
            {
                return true;
            }
        }
        return false;
    }
}