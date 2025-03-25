using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] sounds;
    private AudioSource source;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider volume_slider;

    private void Start()
    {
        // source = GetComponent<AudioSource>();
        LoadVolume();
    }

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.awake;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }

    public void Play(string sound)
    {
        // if (!(sound == "Music"))
        // {
        if (PlayerPrefs.GetInt("Sounds") == 0)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Play();
        }
        // }
        // if (sound == "Music")
        // {
        //     Sound s = Array.Find(sounds, item => item.name == sound);
        //     s.source.Play();
        // }

    }

    public void SetVolume(float sliderValue)
    {
        mixer.SetFloat("MusicVolume", sliderValue);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float volume = PlayerPrefs.GetFloat("MusicVolume");
            volume_slider.value = volume;
            mixer.SetFloat("MusicVolume", volume);
        }
    }

    // public void Stop(string sound)
    // {
    //     Sound s = Array.Find(sounds, item => item.name == sound);
    //     s.source.Stop();
    // }

}