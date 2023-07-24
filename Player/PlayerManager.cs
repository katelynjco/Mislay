using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    public bool hasVisorUpgrade;
    public bool hasRocketUpgrade;
    public bool hasTntUpgrade;

    public bool hasWatercooledUpgrade;
    public bool hasTitaniumUpgrade;
    public bool hasOverclockedUpgrade;

    UpgradesManager upgradesManager;
    ItemsManager itemsManager;

    void Awake() 
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        upgradesManager = FindObjectOfType<UpgradesManager>();
        itemsManager = FindObjectOfType<ItemsManager>();
    }

    void Update()
    {
        if (hasRocketUpgrade || hasTntUpgrade)
        {
            upgradesManager.ActivateTaser();
        }
        
        if(hasVisorUpgrade)
        {
            upgradesManager.ActivateVisorUpgrade();
        }

        if(hasRocketUpgrade)
        {
            upgradesManager.ActivateRocketUpgrade();
        }

        if(hasTntUpgrade)
        {
            upgradesManager.ActivateTntUpgrade();
        }

        if(hasWatercooledUpgrade)
        {
            itemsManager.ActivateWatercooledUpgrade();
        }

        if(hasTitaniumUpgrade)
        {
            itemsManager.ActivateTitaniumUpgrade();
        }

        if(hasOverclockedUpgrade)
        {
            itemsManager.ActivateOverclockedUpgrade();
        }
    }
}
