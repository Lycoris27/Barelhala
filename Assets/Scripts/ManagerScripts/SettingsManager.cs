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
    }

    private void LoadSettings()
    {
        // When the game loads up, this should be accessed in order to set the audio immediately
    }
    private void SaveSettings()
    {
        // When the Confirm button is pressed for the settings, this should activate.
    }
}
