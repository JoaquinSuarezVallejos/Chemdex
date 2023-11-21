using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;

    private void Start()
    {
        LoadMusicValues();
        LoadSFXValues();
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
}
