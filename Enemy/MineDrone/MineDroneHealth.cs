using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDroneHealth : MonoBehaviour
{
    [SerializeField] int maxDroneHealth = 3;
    [SerializeField] int currentDroneHealth;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    MenuManager menuManager;

    SpriteRenderer spriteRenderer;
    [SerializeField] float hitAnimationTime = 0.5f;
    Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();

        currentDroneHealth = maxDroneHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        spriteRenderer.color = initialColor;
    }

    public void TakeDamage(int amount)
    {
        currentDroneHealth-= amount;
        StartCoroutine(HitFeedback());

        if(currentDroneHealth<= 0)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator HitFeedback()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(hitAnimationTime);
        spriteRenderer.color = initialColor;
    }
}
