using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{
    [SerializeField] Transform watercooledUpgrade;
    [SerializeField] Transform watercooledCover;

    [SerializeField] Transform titaniumUpgrade;
    [SerializeField] Transform titaniumCover;

    [SerializeField] Transform overclockedUpgrade;
    [SerializeField] Transform overclockedCover;

    MenuManager menuManager;
    PlayerManager playerManager;
    PlayerMovement playerMovement;
    PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        playerManager  = FindObjectOfType<PlayerManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerHealth = FindObjectOfType<PlayerHealth>();

        watercooledUpgrade.gameObject.SetActive(true);
        watercooledCover.gameObject.SetActive(true);
        titaniumUpgrade.gameObject.SetActive(true);
        titaniumCover.gameObject.SetActive(true);
        overclockedUpgrade.gameObject.SetActive(true);
        overclockedCover.gameObject.SetActive(true);

    }


    public void ActivateWatercooledUpgrade()
    {
        watercooledCover.gameObject.SetActive(false);

        playerMovement.maxAdditionalJumps = 3;
    }

    public void ActivateTitaniumUpgrade()
    {
        titaniumCover.gameObject.SetActive(false);
        
        playerHealth.maxPlayerHealth = 12;
    }

    public void ActivateOverclockedUpgrade()
    {
        overclockedCover.gameObject.SetActive(false);

        playerMovement.runSpeed = 6;
    }
}
