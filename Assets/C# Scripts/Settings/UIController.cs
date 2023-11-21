using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;
    public Image musicBtnImage, sfxBtnImage;

    private void Start()
    {
        LoadMusicValues();
        LoadSFXValues();
    }

    private void Update()
    {
        LoadToggleMusicValue();
        LoadToggleSFXValue();
    }

    public void ToggleMusic()
    {
        AudioManager.Instance.ToggleMusic();
    }

    public void ToggleSFX()
    {
        AudioManager.Instance.ToggleSFX();
    }

    public void MusicVolume()
    {   
        AudioManager.Instance.MusicVolume(_musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", _musicSlider.value);
        LoadMusicValues();
    }

    public void SFXVolume()
    {
        AudioManager.Instance.SFXVolume(_sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", _sfxSlider.value);
        LoadSFXValues();
    }

    private void LoadMusicValues()
    {
        float musicVolumeValue = PlayerPrefs.GetFloat("MusicVolume");
        _musicSlider.value = musicVolumeValue;
        AudioListener.volume = musicVolumeValue;
    }

    private void LoadSFXValues()
    {
        float SFXVolumeValue = PlayerPrefs.GetFloat("SFXVolume");
        _sfxSlider.value = SFXVolumeValue;
        //AudioListener.volume = SFXVolumeValue;
    }

    private void LoadToggleMusicValue()
    {
        string ToggleMusicValue = PlayerPrefs.GetString("ToggleMusic");

        if (ToggleMusicValue == "muted")
        {
            Color tempColor = musicBtnImage.color;
            tempColor.a = 0.75f;
            musicBtnImage.color = tempColor;
        }

        else
        {
            Color tempColor = musicBtnImage.color;
            tempColor.a = 1f;
            musicBtnImage.color = tempColor;
        }
    }

    private void LoadToggleSFXValue()
    {
        string ToggleSFXValue = PlayerPrefs.GetString("ToggleSFX");

        if (ToggleSFXValue == "muted")
        {
            Color tempColor = sfxBtnImage.color;
            tempColor.a = 0.75f;
            sfxBtnImage.color = tempColor;
        }

        else
        {
            Color tempColor = sfxBtnImage.color;
            tempColor.a = 1f;
            sfxBtnImage.color = tempColor;
        }
    }
}
