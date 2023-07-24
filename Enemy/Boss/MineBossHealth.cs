using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MineBossHealth : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField] int maxBossHealth = 20;
    [SerializeField] int currentBossHealth;

    public bool bossAlive = true;
    private bool hasIFrames = false;

    [SerializeField] Transform exitDoor;
    [SerializeField] ParticleSystem bossExplode;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    AudioManager audioManager;
    PlayerManager playerManager;

    SpriteRenderer spriteRenderer;
    [SerializeField] float hitAnimationTime = 0.5f;
    Color initialColor;

    [SerializeField] Transform unlockMenu;
    [SerializeField] Button continueButton;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();
        playerManager  = FindObjectOfType<PlayerManager>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        currentBossHealth = maxBossHealth;
        exitDoor.gameObject.SetActive(true);

        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        spriteRenderer.color = initialColor;

        unlockMenu.gameObject.SetActive(false);
    }

    void Update() 
    {
        CheckStatus();
    }

    void CheckStatus()
    {
        if(currentBossHealth == (maxBossHealth/2))
        {
            MineBossAttack mineBossAttack = GetComponent<MineBossAttack>();
            mineBossAttack.attackCooldown = 3f;
        }

        if(currentBossHealth <= 0)
        {
            exitDoor.gameObject.SetActive(false);
            bossAlive = false;
            StartCoroutine(DeathAnimation());
            UnlockScreen();
        }
    }

    public void TakeDamage(int amount)
    {
        if(!hasIFrames)
        {
            if(bossAlive)
            {
                currentBossHealth-= amount;
                StartCoroutine(HitFeedback());
            }
        }
    }

    IEnumerator HitFeedback()
    {
        spriteRenderer.color = Color.red;
        hasIFrames = true;
        yield return new WaitForSeconds(hitAnimationTime);
        spriteRenderer.color = initialColor;
        hasIFrames = false;
    }

    IEnumerator DeathAnimation()
    {
        bossExplode.Play();
        yield return new WaitForSeconds(hitAnimationTime * 5);
        spriteRenderer.gameObject.SetActive(false);
        bossExplode.Stop();
    }

    void UnlockScreen()
    {
        audioManager.PlaySFX(audioManager.upgrade);
        menuManager.gameIsPaused = true;
        unlockMenu.gameObject.SetActive(true);
        playerManager.hasTntUpgrade = true;
        continueButton.onClick.AddListener(CloseUnlockScreen);
    }

    public void CloseUnlockScreen()
    {
        audioManager.PlaySFX(audioManager.select);
        menuManager.gameIsPaused = false;
        unlockMenu.gameObject.SetActive(false);
    }
}
