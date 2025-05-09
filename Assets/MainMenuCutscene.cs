using UnityEngine;

public class MainMenuCutscene : MonoBehaviour
{
    public void PlayCutsceneAudio(string clipName)
    {
        AudioManager.instance.PlayAudioClip(clipName);
    }
}
