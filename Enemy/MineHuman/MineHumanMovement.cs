using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineHumanMovement : MonoBehaviour
{
    MenuManager menuManager;

    Rigidbody2D myRigidBody;
    Animator myAnimator;

    [SerializeField] float humanMoveSpeed = 2f;
    [SerializeField] Transform target;
    [SerializeField] float humanMinimumDistance = 1.2f;
    [SerializeField] float humanMaxDistance = 3f;

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
        if (FindObjectOfType<MineHumanAttack>().humanIsAttacking == false)
        {
            if (Vector2.Distance(transform.position, target.position) < humanMaxDistance)
            {
                if (Vector2.Distance(transform.position, target.position) > humanMinimumDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, humanMoveSpeed * Time.deltaTime);

                    bool hasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
                    myAnimator.SetBool("isMoving", hasHorizontalSpeed);
                }
            }
        }
        
    }

    void FlipSprite()
    {
        Vector2 direction = target.position - transform.position;

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
