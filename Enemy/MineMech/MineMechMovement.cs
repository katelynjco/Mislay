using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMechMovement : MonoBehaviour
{
    MenuManager menuManager;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    [SerializeField] float mechMoveSpeed = 4f;
    [SerializeField] Transform target;
    [SerializeField] float mechMinimumDistance = 2f;
    [SerializeField] float mechMaxDistance = 3f;

    public Vector2 direction;

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
        if (FindObjectOfType<MineMechAttack>().mechIsAttacking == false)
        {
            if (Vector2.Distance(transform.position, target.position) < mechMaxDistance)
            {
                if (Vector2.Distance(transform.position, target.position) > mechMinimumDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, mechMoveSpeed * Time.deltaTime);

                    bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
                    myAnimator.SetBool("isMoving", hasHorizontalSpeed);
                }
            }
        }
        
    }

    void FlipSprite()
    {
        direction = target.position - transform.position;

        if (direction.x > 0)
        {
            transform.localScale = new Vector2(-1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector2(1, 1);
        }
    }
}
