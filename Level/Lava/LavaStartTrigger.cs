using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaStartTrigger : MonoBehaviour
{
    LavaRise lavaRise;
    AudioManager audioManager;

    [SerializeField] GameObject fallingLava;

    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        lavaRise = FindObjectOfType<LavaRise>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (!once)
        {
            if(other.tag == ("Player"))
            {
                once = true;
                
                lavaRise.lavaStart = true;

                audioManager.SmelterCrucibleCheck();

                fallingLava.SetActive(true);
            }  
        }
    }
}
