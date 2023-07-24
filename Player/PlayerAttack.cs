using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    MenuManager menuManager;
    UpgradesManager upgradesManager;
    AudioManager audioManager;

    public GameObject chargedAttack;
    [SerializeField] Transform chargedAttackSpawn;

    public GameObject regAttack;
    [SerializeField] Transform regAttackSpawn;

    [SerializeField] float maxChargeAttack = 5;
    [SerializeField] float currentChargeAttack;

    [SerializeField] float attackCooldown = 0.3f;
    [SerializeField] bool canAttack = false;

    public Slider chargedAttackUI;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();
        upgradesManager = FindObjectOfType<UpgradesManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        SetChargeToMax();
    }

    void OnFire(InputValue value)
    {
        if(FindObjectOfType<PlayerHealth>().isAlive == false){return;}
        if(FindObjectOfType<PlayerHealth>().isAlive)
        {
            if(canAttack)
            {
                Instantiate(regAttack, regAttackSpawn.position, transform.rotation);
                if(value.isPressed)
                {
                    PlaySelectSFX();
                    myAnimator.SetTrigger("regShot");
                }
            }

            StartCoroutine(AttackCoolDown());
        }
    }

    void OnShoot(InputValue value)
    {   
        if(FindObjectOfType<PlayerHealth>().isAlive == false){return;}
        if(FindObjectOfType<PlayerHealth>().isAlive)
        {
            if(currentChargeAttack >= 0)
            {
                if(canAttack)
                {
                    if(value.isPressed)
                    {
                        Instantiate(chargedAttack, chargedAttackSpawn.position, transform.rotation);
                        PlaySelectSFX();
                        myAnimator.SetTrigger("chargeShot");
                        currentChargeAttack--;
                        UpdateChargeUI();
                    }
                }
            }

            StartCoroutine(AttackCoolDown());
        }
    }

    IEnumerator AttackCoolDown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    public void SetChargeToMax()
    {
        currentChargeAttack = maxChargeAttack;
        UpdateChargeUI();
    }

    public void UpdateChargeUI()
    {
        chargedAttackUI.value = (currentChargeAttack/maxChargeAttack);
    }

    void PlaySelectSFX()
    {
        audioManager.PlaySFX(audioManager.shoot);
    }
}
