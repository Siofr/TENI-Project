using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioData
    {
        public string audioName;
        public AudioClip audioClip;
        public AudioMixerGroup audioMixerGroup;
        public bool isLooping;
    }

    public AudioData[] audioDataArray;
    private Dictionary<string, AudioSource> _audioSourceDict = new Dictionary<string, AudioSource>();

    public static AudioManager instance;
    public AudioMixer audioMixer;

    private float _fadeRate = 0.0075f;

    public AudioMixerGroup[] audioMixerGroups {
        get
        {
            return audioMixer.FindMatchingGroups("Master");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        CreateAudioSources();
    }
    
    void ShowAudioGroups()
    {
        foreach (AudioMixerGroup audioMixerGroup in audioMixerGroups)
        {
            print(audioMixerGroup.name);
        }
    }

    public void PlayAudioClip(string audioName)
    {
        if (!_audioSourceDict.ContainsKey(audioName))
        {
            return;
        }

        _audioSourceDict[audioName].Play();
    }

    public void StopAudioClip(string audioName)
    {
        if (!_audioSourceDict.ContainsKey(audioName))
        {
            return;
        }

        _audioSourceDict[audioName].Stop();
    }

    private void CreateAudioSources()
    {
        foreach (AudioData item in audioDataArray)
        {
            AudioSource newAudioSource = gameObject.AddComponent<AudioSource>();
            newAudioSource.clip = item.audioClip;
            newAudioSource.outputAudioMixerGroup = item.audioMixerGroup;
            newAudioSource.playOnAwake = false;
            newAudioSource.loop = item.isLooping;

            if (item.isLooping)
                newAudioSource.volume = 0;

            _audioSourceDict.Add(item.audioName, newAudioSource);
        }
    }

    public void FadeIn(string audioName)
    {
        if (audioName == null)
            return;

        if (!_audioSourceDict.ContainsKey(audioName))
        {
            return;
        }

        StartCoroutine(FadeInAudio(_audioSourceDict[audioName]));
    }

    public void FadeOut(string audioName)
    {
        if (audioName == null)
            return;

        if (!_audioSourceDict.ContainsKey(audioName))
        {
            return;
        }

        StartCoroutine(FadeOutAudio(_audioSourceDict[audioName]));
    }

    private IEnumerator FadeInAudio(AudioSource audioSource)
    {
        audioSource.Play();
        for (float i = 0f; i <= 1f; i += _fadeRate)
        {
            audioSource.volume = i;
            yield return new WaitForFixedUpdate();
        }
        audioSource.volume = 1;
    }

    private IEnumerator FadeOutAudio(AudioSource audioSource)
    {
        for (float i = 1f; i >= 0f; i -= _fadeRate)
        {
            audioSource.volume = i;
            yield return new WaitForFixedUpdate();
        }
        audioSource.volume = 0;
        audioSource.Stop();
    }
}
