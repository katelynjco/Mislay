using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricChargedAttack : MonoBehaviour
{
    [SerializeField] float regShotSpeed = 5f;
    [SerializeField] float regShotLife = 1f;
    
    Rigidbody2D myRigidBody;
    PlayerMovement player;
    MenuManager menuManager;

    float xSpeed;
    SpriteRenderer regShotSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        menuManager = FindObjectOfType<MenuManager>();

        regShotSpriteRenderer = GetComponent<SpriteRenderer>();

        xSpeed = -player.transform.localScale.x * regShotSpeed;

        FlipRegShotSprite();

        Destroy(gameObject, regShotLife);
    }

    // Update is called once per frame
    void Update()
    {

        myRigidBody.velocity = new Vector2 (xSpeed, 0f);
    }

     void FlipRegShotSprite()
    {
        regShotSpriteRenderer.flipX = player.transform.localScale.x < 0f;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == ("PlayerAttack"))
        {
            Destroy(other.gameObject);
        }
        else if(other.tag == ("Enemy Human"))
        {
            MineHumanHealth human = other.gameObject.GetComponent<MineHumanHealth>();
            if (human)
            {
                human.TakeDamage(2);
            }
            Destroy(gameObject);
        }
        else if(other.tag == ("Drone"))
        {
            MineDroneHealth drone = other.gameObject.GetComponent<MineDroneHealth>();
            if (drone)
            {
                drone.TakeDamage(2);
            }
            Destroy(gameObject);
        }
        else if(other.tag == ("Enemy Mech"))
        {
            MineMechHealth mech = other.gameObject.GetComponent<MineMechHealth>();
            if (mech)
            {
                mech.TakeDamage(2);
            }
            Destroy(gameObject);
        }
        else if(other.tag == ("Boss"))
        {
            MineBossHealth mineBoss = other.gameObject.GetComponent<MineBossHealth>();
            SmelterBossHealth smelterBoss = other.gameObject.GetComponent<SmelterBossHealth>();
            if (mineBoss != null)
            {
                mineBoss.TakeDamage(1);
            }
            else if (smelterBoss != null)
            {
                smelterBoss.TakeDamage(1);
            }
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
