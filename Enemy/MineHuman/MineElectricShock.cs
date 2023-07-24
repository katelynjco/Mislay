using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineElectricShock : MonoBehaviour
{
    [SerializeField] float mineElectricShockSpeed = 1f;
    [SerializeField] float mineElectricShockLife = 0.2f;
    
    Rigidbody2D myRigidBody;
    MineHumanMovement miner;
    MenuManager menuManager;

    float xSpeed;
    SpriteRenderer mineElectricShockSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        miner =  GetComponentInParent<MineHumanMovement>();
        menuManager = FindObjectOfType<MenuManager>();

        mineElectricShockSpriteRenderer = GetComponent<SpriteRenderer>();

        if(miner != null)
        {

            xSpeed = -miner.transform.localScale.x * mineElectricShockSpeed;
            FlipMineElectricShockSprite();
        }

        Destroy(gameObject, mineElectricShockLife);
    }

    // Update is called once per frame
    void Update()
    {
        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

     void FlipMineElectricShockSprite()
    {
        mineElectricShockSpriteRenderer.flipX = miner.transform.localScale.x < 0f;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(menuManager.gameIsPaused == false)
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
}
