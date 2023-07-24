using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button optionsButton;
    [SerializeField] Button quitButton;
    [SerializeField] Button backButton;

    [SerializeField] Transform startMenu;
    [SerializeField] Transform optionsMenu;
    
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(OpenOptions);
        quitButton.onClick.AddListener(QuitGame);
        backButton.onClick.AddListener(CloseOptions);

        optionsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    public void StartGame()
    {
        PlaySelectSFX();
        SceneManager.LoadScene(1);
    }

    public void OpenOptions()
    {
        PlaySelectSFX();
        startMenu.gameObject.SetActive(false);
        optionsMenu.gameObject.SetActive(true);
    }

    public void CloseOptions()
    {
        PlaySelectSFX();
        optionsMenu.gameObject.SetActive(false);
        startMenu.gameObject.SetActive(true);
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
