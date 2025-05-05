using UnityEngine;

public class SculptorCharacter : CharacterBase
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

    public override void SetEmotionState(string stateName)
    {
        if (!CheckAnimationParameter(stateName))
        {
            Debug.Log(string.Format("{0} is not a valid state!", stateName));
            return;
        }

        // Set all emotionstates that are NOT the currently requested state to false, and set
        // the requested state to true.
        emotionStates.ForEach(emotionState =>
            anim.SetBool(emotionState, (emotionState == stateName)));
    }
}
