using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelComplete : MonoBehaviour
{
    MenuManager menuManager;
    AudioManager audioManager;

    [SerializeField] Transform completeScreen;
    [SerializeField] Button continueButton;

    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        completeScreen.gameObject.SetActive(false);
        continueButton.onClick.AddListener(LoadNextLevel);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player"))
        {
            audioManager.CompleteScreenCheck();
            menuManager.gameIsPaused = true;
            completeScreen.gameObject.SetActive(true);
        }
          
    }

    public void LoadNextLevel()
    {
        audioManager.PlaySFX(audioManager.select);
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex); 
        menuManager.gameIsPaused = false;
    }


}
