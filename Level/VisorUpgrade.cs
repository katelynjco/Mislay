using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisorUpgrade : MonoBehaviour
{
    MenuManager menuManager;
    AudioManager audioManager;
    UpgradesManager upgradesManager;

    bool wasCollected = false;

    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        upgradesManager = FindObjectOfType<UpgradesManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player") && !wasCollected)
        {
            audioManager.PlaySFX(audioManager.collectablePickup);
            wasCollected = true;
            FindObjectOfType<PlayerManager>().hasVisorUpgrade = true;
            upgradesManager.CheckVisor();
            Destroy(gameObject);
            menuManager.PauseGame();
        }  
    }
}
