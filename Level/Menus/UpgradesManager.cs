using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    [SerializeField] Toggle visorToggle;
    [SerializeField] Transform visorUpgrade;
    [SerializeField] Transform visorCover;

    [SerializeField] Animator defaultAnimator;
    [SerializeField] Animator altAnimator;

    [SerializeField] Toggle taserToggle;

    public GameObject chargeAttackTaser;
    public GameObject regAttackTaser;

    [SerializeField] Toggle rocketToggle;
    [SerializeField] Transform rocketUpgrade;
    [SerializeField] Transform rocketCover;

    [SerializeField] Toggle tntToggle;
    [SerializeField] Transform tntUpgrade;
    [SerializeField] Transform tntCover;

    public GameObject chargeAttackTnt;
    public GameObject regAttackTnt;

    MenuManager menuManager;
    PlayerManager playerManager;
    PlayerMovement player;
    PlayerAttack playerAttack;
    Animator playerAnimator;
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        tntToggle.isOn = false;
    }

    void OnEnable()
    {
        menuManager = FindObjectOfType<MenuManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        playerAttack = FindObjectOfType<PlayerAttack>();
        player = FindObjectOfType<PlayerMovement>();
        playerAnimator = player.GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        visorCover.gameObject.SetActive(true);
        rocketCover.gameObject.SetActive(true);
        tntCover.gameObject.SetActive(true);

        taserToggle.onValueChanged.AddListener(delegate 
        {
                ToggleValueChanged(CheckTaser);
        });
        visorToggle.onValueChanged.AddListener(delegate 
        {
                ToggleValueChanged(CheckVisor);
        });
        rocketToggle.onValueChanged.AddListener(delegate 
        {
            ToggleValueChanged(CheckRocket);
        });
        tntToggle.onValueChanged.AddListener(delegate 
        {
            ToggleValueChanged(CheckTnt);
        });
    }

    public void ActivateVisorUpgrade()
    {
        visorUpgrade.gameObject.SetActive(true);
        visorCover.gameObject.SetActive(false);
        visorToggle.interactable = true;
    }

    public void CheckVisor()
    {
        if(visorToggle.isOn)
        {
            playerAnimator.runtimeAnimatorController = defaultAnimator.runtimeAnimatorController;
        }
        else
        {
            playerAnimator.runtimeAnimatorController = altAnimator.runtimeAnimatorController;
        }
    }

    public void ActivateTaser()
    {
        taserToggle.interactable = true;
    }

    public void CheckTaser()
    {
        if(taserToggle.isOn)
        {
            tntToggle.isOn = false;
            playerAttack.chargedAttack = chargeAttackTaser;
            playerAttack.regAttack = regAttackTaser;
        }
    }

    public void ActivateRocketUpgrade()
    {
        rocketUpgrade.gameObject.SetActive(true);
        rocketCover.gameObject.SetActive(false);
        rocketToggle.interactable = false;
    }

    public void CheckRocket()
    {
        player.maxAdditionalJumps = 20;
    }

    public void ActivateTntUpgrade()
    {
        tntUpgrade.gameObject.SetActive(true);
        tntCover.gameObject.SetActive(false);
        tntToggle.interactable = true;

    }

    public void CheckTnt()
    {
        if(tntToggle.isOn)
        {
            taserToggle.isOn = false;
            playerAttack.chargedAttack = chargeAttackTnt;
            playerAttack.regAttack = regAttackTnt;
        }
    }

    public void ToggleValueChanged(Action action)
    {
        action.Invoke();
    }

    void PlaySelectSFX()
    {
        audioManager.PlaySFX(audioManager.select);
    }
}
