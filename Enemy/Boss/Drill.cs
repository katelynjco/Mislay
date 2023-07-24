using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    MenuManager menuManager;

    [SerializeField] float moveSpeed;
    [SerializeField] float waitTime;
    [SerializeField] float drillDuration;

    Vector3 initialPosition;
    bool isMoving = false;
    bool reachedTrigger = false;

    [SerializeField] Transform drillStopTrigger;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        menuManager = FindObjectOfType<MenuManager>();

        initialPosition = transform.position;
        RandomizeNumbers();
    }

    public void Move()
    {
        if (!isMoving)
        {
            RandomizeNumbers();
            StartCoroutine(MoveAndReturn());
        }
    }

    void RandomizeNumbers()
    {
        moveSpeed = Random.Range(2f, 4.5f);
        waitTime = Random.Range(1f, 2f);
        drillDuration = Random.Range(1f, 5f);
    }

     private System.Collections.IEnumerator MoveAndReturn()
    {
        isMoving = true;
        reachedTrigger = false;

        StartCoroutine(DrillTimer());

        // Move the drill forward
        while (!reachedTrigger && transform.position.y <= drillStopTrigger.position.y)
        {
            myRigidBody.velocity = new Vector2(0, moveSpeed);
            yield return null;
        }

        // Stop the drill and wait
        myRigidBody.velocity = Vector2.zero;
        yield return new WaitForSeconds(waitTime);

        // Return the drill to the initial position
        while (transform.position.y > initialPosition.y)
        {
            myRigidBody.velocity = new Vector2(0, -moveSpeed);
            yield return null;
        }

        // Reset the drill position and stop moving
        myRigidBody.velocity = Vector2.zero;
        transform.position = initialPosition;
        isMoving = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == drillStopTrigger)
        {
            reachedTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Boss Attack")
        {
            reachedTrigger = true;
        }
        else if (collision.collider.tag == "Moving Platform")
        {
            reachedTrigger = true;
        }
        else if (collision.collider.tag == "Drill")
        {
            reachedTrigger = true;
        }
    }

    IEnumerator DrillTimer()
    {
        yield return new WaitForSeconds(drillDuration);
        reachedTrigger = true;
    }
}

