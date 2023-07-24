using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeUp : MonoBehaviour
{
    MenuManager menuManager;
    AudioManager audioManager;

    bool wasCollected = false;

    void Start() 
    {
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player") && !wasCollected)
        {
            audioManager.PlaySFX(audioManager.collectablePickup);
            wasCollected = true;
            FindObjectOfType<PlayerAttack>().SetChargeToMax();
            Destroy(gameObject);
        }  
    }
}
