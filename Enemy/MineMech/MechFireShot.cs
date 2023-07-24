using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechFireShot : MonoBehaviour
{
    [SerializeField] float mineFireShockSpeed = 10f;
    [SerializeField] float mineFireShockLife = 1f;
    
    Rigidbody2D myRigidBody;
    MineMechMovement mech;
    MenuManager menuManager;

    float xSpeed;
    SpriteRenderer drillSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        mech = GetComponentInParent<MineMechMovement>();
        menuManager = FindObjectOfType<MenuManager>();

        drillSpriteRenderer = GetComponent<SpriteRenderer>();

        if (mech != null)
        {
            xSpeed = -mech.transform.localScale.x * mineFireShockSpeed;
            FlipMineFireShockSprite();
        }
        else
        {
            Debug.LogWarning("MechFireShot: Could not find MineMechMovement component in parent object.");
        }

        Destroy(gameObject, mineFireShockLife);
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

     void FlipMineFireShockSprite()
    {
        drillSpriteRenderer.flipX = mech.transform.localScale.x < 0f;
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
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
