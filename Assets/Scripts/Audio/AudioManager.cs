using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer;

    public AudioMixerGroup[] audioMixerGroups {
        get
        {
            return audioMixer.FindMatchingGroups("Master");
        }
    }

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
