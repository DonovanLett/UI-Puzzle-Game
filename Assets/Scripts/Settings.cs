using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Settings : MonoBehaviour
{
    [Header("Overall Volume")]
    public AudioMixer audioMixer;
    [Header("-UI")]
    public Slider _overallVolumeSlider;

    [Header("Music Volume")]
    /*
    public AudioSource musicAudioSource;
    */
    [Header("-UI")]
    public Slider _musicVolumeSlider;

    [Header("Brightness")]
    public Volume volume;
    private ColorAdjustments colorAdjustments;
    private Color baseColor;
    [Header("-UI")]
    public Slider _brightnessSlider;

    [Header("UISaveScript")]
    public UISaveScript _uISaveScript;

    // Start is called before the first frame update
    void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments))
        {
            baseColor = colorAdjustments.colorFilter.value;
        }


        /// Convert values to their sliders

        /*
        // Overall Volume
        float currentDB;

        if (audioMixer.GetFloat("MasterVolume", out currentDB))
        {
            _overallVolumeSlider.value = currentDB;
        }
        */

        // Music Volume
        //_musicVolumeSlider.value = musicAudioSource.volume;

        // Brightness
        /*
        float baseBrightness = baseColor.grayscale;
        float currentBrightness = colorAdjustments.colorFilter.value.grayscale;
        float t = 1f - (currentBrightness / baseBrightness);
        _brightnessSlider.value = Mathf.Clamp01(t);
        */

        // UI Save Script
        _uISaveScript.SaveState();
    }

    /*
    public void OnVolumeSliderChanged(float value)
    {
        // AudioMixer usually expects decibels
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);

        PlayerPrefs.SetFloat("Master Volume", value);
    }
    */

    public void OnBrightnessSliderChanged(float value)
    {
        colorAdjustments.colorFilter.value = Color.Lerp(Color.black, baseColor, value);
        PlayerPrefs.SetFloat("Brightness", value);
    }

    public void OnMusicSliderChanged(float value)
    {
        value = Mathf.Clamp(value, 0.0001f, 1f);

        float dB = Mathf.Log10(value) * 20f;
        audioMixer.SetFloat("MusicVolume", dB);
        /*
       // musicAudioSource.volume = value;
       // PlayerPrefs.SetFloat("Music Volume", value);
        /// or
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
        Debug.Log(Mathf.Log10(value) * 20);
        PlayerPrefs.SetFloat("Music Volume", value);
        */
    }
}