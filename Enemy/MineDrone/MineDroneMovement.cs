using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDroneMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] Transform[] patrolPoints;

    Rigidbody2D myRigidBody;
    Animator myAnimator;
    MenuManager menuManager;
    
    int currentPointIndex;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FlipSprite();
    }

    void Move() 
    {
        if(transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed *Time.deltaTime);
        } 
        else
        {
            if(once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);
        if(currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false;
    }

    void FlipSprite()
    {
        // Get the direction of movement
        float movementDirection = Mathf.Sign(transform.position.x - patrolPoints[currentPointIndex].position.x);

        // Flip the sprite based on the movement direction
        if (movementDirection != 0)
        {
            transform.localScale = new Vector2(movementDirection, 1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerHealth player = other.gameObject.GetComponent<PlayerHealth>();
            if (player)
            {
                player.TakeDamage(1);
            }
        }
    }
}
