using UnityEngine;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public AudioMixer audioMixerMain;
    public AudioMixer audioMixerEffect;
    public void SetVolume(float volume)
    {
        audioMixerMain.SetFloat("volume", volume);
    }
    public void SetVolumeEffect(float volume)
    {
        audioMixerEffect.SetFloat("volume", volume);
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
