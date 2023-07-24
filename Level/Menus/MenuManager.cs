using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Button resumeButton;
    [SerializeField] Button optionsButton;

    [SerializeField] Button backButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button quitButton;

    [SerializeField] Button continueButton;

    [SerializeField] Transform pauseMenu;
    [SerializeField] Transform optionsMenu;
    [SerializeField] Transform deathScreen;

    public bool gameIsPaused;
    public bool menuIsOpen;

    PlayerHealth playerHealth;
    AudioManager audioManager;
    VolumeSettings volumeSettings;

    // Start is called before the first frame update
    void Start()
    {
        gameIsPaused = false;

        resumeButton.onClick.AddListener(UnpauseGame);
        optionsButton.onClick.AddListener(OpenOptions);

        backButton.onClick.AddListener(CloseOptions);
        restartButton.onClick.AddListener(RestartLevel);
        quitButton.onClick.AddListener(QuitGame);

        continueButton.onClick.AddListener(RestartLevel);

        playerHealth = FindObjectOfType<PlayerHealth>();
        volumeSettings = FindObjectOfType<VolumeSettings>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        optionsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        deathScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
            {
                if(playerHealth.isAlive)
                {
                    UnpauseGame();
                }
                else
                {
                    return;
                }
            }
            else
            {
                PauseGame();
            }
        }

        volumeSettings.LoadVolume();
    }

    public void PauseGame()
    {
        PlaySelectSFX();
        gameIsPaused = true;
        pauseMenu.gameObject.SetActive(true);
        menuIsOpen = true;
    }

    public void UnpauseGame()
    {
        PlaySelectSFX();
        gameIsPaused = false;
        optionsMenu.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        menuIsOpen = false;
    }

    public void OpenOptions()
    {
        PlaySelectSFX();
        gameIsPaused = true;
        optionsMenu.gameObject.SetActive(true);
        menuIsOpen = false;
    }

    public void CloseOptions()
    {
        PlaySelectSFX();
        menuIsOpen = true;
        optionsMenu.gameObject.SetActive(false);
    }

    public void DeathScreen()
    {
        audioManager.DeathScreenCheck();
        gameIsPaused = true;
        menuIsOpen = false;
        deathScreen.gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        PlaySelectSFX();
        gameIsPaused = false;
        menuIsOpen = false;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void QuitGame()
    {
        PlaySelectSFX();
        Application.Quit();
    }

    void PlaySelectSFX()
    {
        audioManager.PlaySFX(audioManager.select);
    }




}

