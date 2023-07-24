using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public static float musicVolume;
    public static float sfxVolume;

    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        LoadVolume(); 
    }

    public void SetMusicVolume()
    {
        musicVolume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume()
    {
        sfxVolume = sfxSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
        PlayerPrefs.Save();
    }

    public void LoadVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
            musicSlider.value = musicVolume;
            PlayerPrefs.Save();
        }

        if (PlayerPrefs.HasKey("sfxVolume"))
        {
            sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            sfxSlider.value = sfxVolume;
            PlayerPrefs.Save();
        }

        else
        {
            SetMusicVolume();
            SetSFXVolume();
        }
    }
}
