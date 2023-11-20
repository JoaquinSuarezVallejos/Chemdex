using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] music_Sounds, SFX_Sounds;
    public AudioSource musicSource, SFXSource;

    private void Awake()    
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MainTheme");
    }

    public void PlayMusic(string name)
    {
        Sound tempMusicSound = Array.Find(music_Sounds, x => x.name == name);

        if (tempMusicSound == null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            musicSource.clip = tempMusicSound.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound tempSFXSound = Array.Find(SFX_Sounds, x => x.name == name);

        if (tempSFXSound == null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            SFXSource.PlayOneShot(tempSFXSound.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        SFXSource.mute = !SFXSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }
}
