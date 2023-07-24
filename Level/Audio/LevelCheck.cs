using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using Cinemachine;

public class LevelCheck : MonoBehaviour
{
    AudioManager audioManager;

    public CinemachineVirtualCamera vcam;

    int currentSceneIndex;
    int lastSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex = lastSceneIndex;
        CheckLevel();
    }

    // Update is called once per frame
    void Update()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex != lastSceneIndex)
        {
            CheckLevel();
            currentSceneIndex = lastSceneIndex;
        }
    }

    void CheckLevel()
    {
        if(currentSceneIndex == 0)
        {
            audioManager.StartMenuCheck();
            vcam.m_Lens.OrthographicSize = 5;
        }
        else if(currentSceneIndex == 1)
        {
            audioManager.MineLevelCheck();
            vcam.m_Lens.OrthographicSize = 5;
        }
        else if(currentSceneIndex == 2)
        {
            audioManager.MineBossCheck();
            vcam.m_Lens.OrthographicSize = 7;
        }
        else if (currentSceneIndex == 3)
        {
            audioManager.SmelterLevelCheck();
            vcam.m_Lens.OrthographicSize = 5;
        }
        else if(currentSceneIndex == 4)
        {
            audioManager.SmelterBossCheck();
            vcam.m_Lens.OrthographicSize = 7;
        }
    }
}
