using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] music_Sounds, SFX_Sounds;
    public AudioSource musicSource, SFXSource;
    public Image musicBtnImage, sfxBtnImage;

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
        LoadToggleMusicValue();
        LoadToggleSFXValue();
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
        //Color tempColor = musicBtnImage.color;

        if (musicSource.mute == true)
        {
            /*tempColor.a = 0.75f;
            musicBtnImage.color = tempColor;*/
            PlayerPrefs.SetString("ToggleMusic", "muted");
        }

        else 
        {
            /*tempColor.a = 1f;
            musicBtnImage.color = tempColor;*/
            PlayerPrefs.SetString("ToggleMusic", "not muted");
        }

        LoadToggleMusicValue();
    }

    public void ToggleSFX()
    {
        SFXSource.mute = !SFXSource.mute;
        //Color tempColor = UIController.sfxBtnImage.color;

        if (SFXSource.mute == true)
        {
            /*tempColor.a = 0.75f;
            UIController.sfxBtnImage.color = tempColor;*/
            PlayerPrefs.SetString("ToggleSFX", "muted");
        }

        else 
        {
            /*tempColor.a = 1f;
            UIController.sfxBtnImage.color = tempColor;*/
            PlayerPrefs.SetString("ToggleSFX", "not muted");
        }

        LoadToggleSFXValue();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SFXVolume(float volume)
    {
        SFXSource.volume = volume;
    }

    private void LoadToggleMusicValue()
    {
        string ToggleMusicValue = PlayerPrefs.GetString("ToggleMusic");

        if (ToggleMusicValue == "muted")
        {
            musicSource.mute = true;
            /*Color tempColor = UIController.musicBtnImage.color;
            tempColor.a = 0.75f;
            UIController.musicBtnImage.color = tempColor;*/
        }

        else
        {
            musicSource.mute = false;
            /*Color tempColor = UIController.musicBtnImage.color;
            tempColor.a = 1f;
            UIController.musicBtnImage.color = tempColor;*/
        }
    }

    private void LoadToggleSFXValue()
    {
        string ToggleSFXValue = PlayerPrefs.GetString("ToggleSFX");

        if (ToggleSFXValue == "muted")
        {
            SFXSource.mute = true;
            /*Color tempColor = UIController.musicBtnImage.color;
            tempColor.a = 0.75f;
            UIController.musicBtnImage.color = tempColor;*/
        }

        else
        {
            SFXSource.mute = false;
            /*Color tempColor = UIController.musicBtnImage.color;
            tempColor.a = 1f;
            UIController.musicBtnImage.color = tempColor;*/
        }
    }
}
