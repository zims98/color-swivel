using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;

    public GameObject musicOn;
    public GameObject musicOff;

    public GameObject vibrationOn;
    public GameObject vibrationOff; // Change these to Images?

    [HideInInspector]
    public bool soundDisabled;
    public bool musicDisabled;
    public bool vibrationDisabled;

    public Animator musicAnim;
    public Animator soundAnim;
    public Animator vibrationAnim;

    void Awake()
    {
        LoadSettings();
    }

    void Update()
    {
        if (soundDisabled) // Sound
        {
            FindObjectOfType<AudioManager>().MuteSounds();
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
        else
        {
            FindObjectOfType<AudioManager>().UnmuteSounds();
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }

        if (musicDisabled) // Music
        {
            FindObjectOfType<AudioManager>().music.mute = true;
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            FindObjectOfType<AudioManager>().music.mute = false;
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }

        if (vibrationDisabled) // Vibration
        {
            Vibrator.vibratorSelected = false;
            vibrationOn.SetActive(false);
            vibrationOff.SetActive(true);
        }
        else
        {
            Vibrator.vibratorSelected = true;
            vibrationOn.SetActive(true);
            vibrationOff.SetActive(false);
        }
    }

    public void ToggleVibration()
    {
        vibrationDisabled = !vibrationDisabled;
        vibrationAnim.SetTrigger("vibration_interacted");
        SaveSettings();
    }

    public void ToggleSound()
    {
        soundDisabled = !soundDisabled;
        soundAnim.SetTrigger("sound_interacted");
        SaveSettings();
    }

    public void ToggleMusic()
    {
        musicDisabled = !musicDisabled;
        musicAnim.SetTrigger("music_interacted");
        SaveSettings();
    }

    public void SaveSettings()
    {
        ES3.Save("sound", soundDisabled);
        ES3.Save("music", musicDisabled);
        ES3.Save("vibration", vibrationDisabled);
    }

    public void LoadSettings()
    {
        soundDisabled = ES3.Load<bool>("sound", false);
        musicDisabled = ES3.Load<bool>("music", false);
        vibrationDisabled = ES3.Load<bool>("vibration", false);
    }
}
