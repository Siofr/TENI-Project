using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void Slider(float value)
    {
        float audioVolume = Mathf.Log10(value) * 20;
        audioVolume = Mathf.Clamp(audioVolume, -60, 0);
        // audioMixer.SetFloat(mixerGroup, audioVolume);
    }
}
