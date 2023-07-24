using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    MenuManager menuManager;
    AudioManager audioManager;

    public float maxPlayerHealth = 10;
    [SerializeField] float currentPlayerHealth;
    public bool isAlive = true;
    public bool hasDied = false;
    private bool hasIFrames = false;
    [SerializeField] float iFramesCooldown = 1f;

    [SerializeField] Vector2 deathKick = new Vector2(5f, 30f);

    public Slider healthUI;

    SpriteRenderer spriteRenderer;
    [SerializeField] float hitAnimationTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;
        SetHealthToMax();
    }

    void Update()
    {
        if(currentPlayerHealth <= 0)
        {
            if(!hasDied)
            {
                isAlive = false;
                hasDied = true;
                audioManager.PlaySFX(audioManager.death);
                menuManager.DeathScreen();
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.collider.CompareTag("Hazard"))
        {
            TakeDamage(2);
            myRigidBody.velocity = deathKick;
        }
    }

    public void TakeDamage(int amount)
    {
        if(!hasIFrames)
        {
            currentPlayerHealth -= amount;
            StartCoroutine(HitFeedback());
        }
        UpdateHealthUI();
    }

    IEnumerator HitFeedback()
    {
        audioManager.PlaySFX(audioManager.hit);
        spriteRenderer.color = Color.blue;
        hasIFrames = true;
        yield return new WaitForSeconds(hitAnimationTime);
        spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(iFramesCooldown);
        hasIFrames = false;
    }

    public void SetHealthToMax()
    {
        currentPlayerHealth = maxPlayerHealth;
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        healthUI.value = (currentPlayerHealth/maxPlayerHealth);
    }
}
