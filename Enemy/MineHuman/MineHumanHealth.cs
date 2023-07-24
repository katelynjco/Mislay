using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHumanHealth : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField] int maxHumanHealth = 3;
    [SerializeField] int currentHumanHealth;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    SpriteRenderer spriteRenderer;
    [SerializeField] float hitAnimationTime = 0.5f;
    Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();

        currentHumanHealth = maxHumanHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        spriteRenderer.color = initialColor;
    }

    public void TakeDamage(int amount)
    {
        currentHumanHealth-= amount;
        StartCoroutine(HitFeedback());

        if(currentHumanHealth<= 0)
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
