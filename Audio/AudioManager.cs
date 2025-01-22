using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    public AudioSource music;
    public Settings settingsScript;

    void Start()
    {
        if (settingsScript != null)
        {
            settingsScript.musicDisabled = ES3.Load<bool>("music", false);

            if (!settingsScript.musicDisabled)
                music.Play();
        }          
    }

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.mute = s.mute;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            //Debug.LogWarning("Sound " + name + " was not found");
            return;
        }

        s.source.Play();
    }

    public void MuteSounds()
    {
        foreach(Sound s in sounds)
        {
            s.source.mute = true;
        }
    }

    public void UnmuteSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.mute = false;
        }
    }
}
