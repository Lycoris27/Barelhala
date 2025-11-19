using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private List<Toggle> toggles;
    [SerializeField] private List<Slider> sliders;

    private void Start()
    {
        LoadSettings();
        StoreDefaults();
    }

    public void LoadSettings()
    {
        // When the game loads up, this should be accessed in order to set the audio immediately

        foreach (Toggle toggle in toggles)
        {
            if (!PlayerPrefs.HasKey(toggle.name)) continue;
            
            toggle.isOn = PlayerPrefs.GetInt(toggle.name, 0) == 1;
        }

        foreach (Slider slider in sliders)
        {
            if (!PlayerPrefs.HasKey(slider.name)) continue;

            slider.value = PlayerPrefs.GetFloat(slider.name);
        }
    }
    public void SaveSettings()
    {
        // When the Confirm button is pressed for the settings, this should activate.

        foreach (Toggle toggle in toggles)
        {
            PlayerPrefs.SetInt(toggle.name, toggle.isOn? 1:0);
        }
        foreach (Slider slider in sliders)
        {
            PlayerPrefs.SetFloat(slider.name, slider.value);
        }
    }
    private void StoreDefaults()
    {
        foreach (Toggle toggle in toggles)
        {
            if (PlayerPrefs.HasKey($"{toggle.name}Default")) continue;
            
            PlayerPrefs.SetInt($"{toggle.name}Default", toggle.isOn ? 1 : 0);
        }
        foreach (Slider slider in sliders)
        {
            if (PlayerPrefs.HasKey($"{slider.name}Default")) continue;

            PlayerPrefs.SetFloat($"{slider.name}Default", slider.value);
        }
    }
    public void RestoreDefaults()
    {
        foreach (Toggle toggle in toggles)
        {
            toggle.isOn = PlayerPrefs.GetInt($"{toggle.name}Default", 0) == 1;
        }
        foreach (Slider slider in sliders)
        {
            slider.value = PlayerPrefs.GetFloat($"{slider.name}Default");
        }
    }
}
