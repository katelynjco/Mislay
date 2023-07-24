using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    // [SerializeField] AudioMixer myMixer;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Background Music")]
    public AudioClip titleMusic;
    public AudioClip deathMusic;
    public AudioClip creditsMusic;
    public AudioClip secretCreditsMusic;
    public AudioClip endLevelMusic;
    public AudioClip mineBossMusic;
    public AudioClip mineIntroMusic;
    public AudioClip mineBackgroundMusic;
    public AudioClip smelterBossMusic;
    public AudioClip smelterIntroMusic;
    public AudioClip smelterBackgroundMusic;
    public AudioClip smelterCrucibleMusic;

    [Header("Player Sounds")]
    public AudioClip death;
    public AudioClip hit;
    public AudioClip collectablePickup;
    public AudioClip shoot;
    public AudioClip select;
    public AudioClip upgrade;

    [Header("Scene Manager")]
    int currentSceneIndex;
    MenuManager menuManager;
    UpgradesManager upgradesManager;
    PlayerManager playerManager;
    VolumeSettings volumeSettings;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        upgradesManager = FindObjectOfType<UpgradesManager>();
        menuManager = FindObjectOfType<MenuManager>();
        playerManager  = FindObjectOfType<PlayerManager>();
        volumeSettings = FindObjectOfType<VolumeSettings>();

        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void Update() 
    {
        if(volumeSettings != null)
        {
            volumeSettings.LoadVolume();
        }
        else
        {
            SettingMusicVolume(VolumeSettings.musicVolume);
            SettingSFXVolume(VolumeSettings.sfxVolume);
        }
    }

    private void StopMusic()
    {
        if(musicSource)
        {
            musicSource.Stop();
        }
        else
        {
            return;
        }
    }
    
    public void SettingMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SettingSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        SettingSFXVolume(VolumeSettings.sfxVolume);
        sfxSource.PlayOneShot(clip);
    }

    public void DeathScreenCheck()
    {
        StopMusic();
        musicSource.clip = deathMusic;
        musicSource.Play();
    }

    public void CompleteScreenCheck()
    {
        StopMusic();
        musicSource.clip = endLevelMusic;
        musicSource.Play();
    }

    public void StartMenuCheck()
    {
        StopMusic();
        musicSource.clip = titleMusic;
        musicSource.Play();
    }

    public void MineLevelCheck()
    {
        StopMusic();
        musicSource.clip = mineIntroMusic;
        musicSource.Play();

        float delay = mineIntroMusic.length - 0.5f;
        Invoke("PlayMineBackgroundMusic", delay);
    }

    private void PlayMineBackgroundMusic()
    {
        StopMusic();
        musicSource.clip = mineBackgroundMusic;
        musicSource.Play();
    }

    public void MineBossCheck()
    {
        StopMusic();
        musicSource.clip = mineBossMusic;
        musicSource.Play();
    }

    public void SmelterLevelCheck()
    {
        StopMusic();
        musicSource.clip = smelterIntroMusic;
        musicSource.Play();

        float delay = smelterIntroMusic.length - 0.5f;
        Invoke("PlaySmelterBackgroundMusic", delay);
    }

    private void PlaySmelterBackgroundMusic()
    {
        StopMusic();
        musicSource.clip = smelterBackgroundMusic;
        musicSource.Play();
    }

    public void SmelterCrucibleCheck()
    {
        StopMusic();
        musicSource.clip = smelterCrucibleMusic;
        musicSource.Play();
    }

    public void SmelterBossCheck()
    {
        StopMusic();
        musicSource.clip = smelterBossMusic;
        musicSource.Play();
    }

    public void CreditsScreenCheck()
    {   
        StopMusic();
        musicSource.clip = creditsMusic;
        musicSource.Play();

        if(playerManager != null)
        {
            if(playerManager.hasVisorUpgrade)
            {
                StopMusic();
                musicSource.clip = secretCreditsMusic;
                musicSource.Play();
            }
        }  
    }

}
