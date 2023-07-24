using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watercooled : MonoBehaviour
{
    MenuManager menuManager;
    AudioManager audioManager;
    ItemsManager itemsManager;

    bool wasCollected = false;

    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player") && !wasCollected)
        {
            audioManager.PlaySFX(audioManager.collectablePickup);
            wasCollected = true;
            FindObjectOfType<PlayerManager>().hasWatercooledUpgrade = true;
            itemsManager.ActivateWatercooledUpgrade();
            Destroy(gameObject);
            menuManager.PauseGame();
        }  
    }
}
