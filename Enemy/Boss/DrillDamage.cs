using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillDamage : MonoBehaviour
{
    MenuManager menuManager;

    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("Player"))
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (player)
            {
                player.TakeDamage(1);
            }
        }
    }
}
