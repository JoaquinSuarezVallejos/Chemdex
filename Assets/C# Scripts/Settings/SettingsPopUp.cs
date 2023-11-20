using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPopUp : MonoBehaviour
{
    [SerializeField] GameObject Chemdex_title, levels_btn, freeplay_btn, settings_popup, settings_button;

    public void SettingsButtonPressed()
    {
        Chemdex_title.SetActive(false);
        levels_btn.SetActive(false);
        freeplay_btn.SetActive(false);
        settings_button.SetActive(false);
        settings_popup.SetActive(true);
    }

    public void BackButtonPressed()
    {
        Chemdex_title.SetActive(true);
        levels_btn.SetActive(true);
        freeplay_btn.SetActive(true);
        settings_button.SetActive(true);
        settings_popup.SetActive(false);
    }
}
