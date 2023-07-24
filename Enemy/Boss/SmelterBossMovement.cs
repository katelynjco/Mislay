using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmelterBossMovement : MonoBehaviour
{
    MenuManager menuManager;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float waitTime = 1f;
    [SerializeField] Transform[] patrolPoints;

    Rigidbody2D myRigidBody;
    SmelterBossHealth smelterBossHealth;
    
    int currentPointIndex;
    bool once = false;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        menuManager = FindObjectOfType<MenuManager>();
        smelterBossHealth = GetComponent<SmelterBossHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(smelterBossHealth.bossAlive)
        // {
            Move();
        // }          
    }

    void Move()
    {
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, moveSpeed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
            }
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }

        once = false;
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
