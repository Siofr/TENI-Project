using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public AudioMixerGroup[] audioMixerGroups {
        get
        {
            return audioMixer.FindMatchingGroups("Master");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowAudioGroups();
    }
    
    void ShowAudioGroups()
    {
        foreach (AudioMixerGroup audioMixerGroup in audioMixerGroups)
        {
            print(audioMixerGroup.name);
        }
    }
}
