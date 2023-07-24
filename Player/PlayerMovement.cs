using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    MenuManager menuManager;

    Vector2 moveInput;

    public float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 20f;

    [SerializeField] int additionalJumps = 2;
    public int maxAdditionalJumps = 2;

    [SerializeField] float rayLength;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform leftpoint;
    [SerializeField] Transform rightpoint;
    [SerializeField] bool grounded = true;

    [SerializeField] bool isWallSliding = false;
    [SerializeField] Transform leftwall;
    [SerializeField] Transform rightwall;

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
        if(FindObjectOfType<PlayerHealth>().isAlive == false){return;}
        if(FindObjectOfType<PlayerHealth>().isAlive)
        {
            Run();
            FlipSprite();
            CheckStatus();
            checkIfWallSliding();
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed)
        {
            if(FindObjectOfType<PlayerHealth>().isAlive)
            {
                if(grounded)
                {
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                }
                else if(additionalJumps == 1)
                {
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                    additionalJumps -= 1;
                }
                else if(additionalJumps == 2)
                {
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                    additionalJumps -= 1;
                }
                else if(additionalJumps == 3)
                {
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                    additionalJumps -= 1;
                }
                if(isWallSliding)
                {
                    myRigidBody.velocity += new Vector2(0f, jumpSpeed);
                }
            }
        }
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isMoving", playerHasHorizontalSpeed);
    }

    void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if(playerHasHorizontalSpeed)
        {    
            transform.localScale = new Vector2 (-Mathf.Sign(myRigidBody.velocity.x), 1f);
        }
        
    }

    void CheckStatus()
    {
        RaycastHit2D leftCheckHit = Physics2D.Raycast(leftpoint.position, Vector2.down, rayLength, groundLayer);
        RaycastHit2D rightCheckHit = Physics2D.Raycast(rightpoint.position, Vector2.down, rayLength, groundLayer);
        if(leftCheckHit || rightCheckHit)
        {
            grounded = true;
            additionalJumps = maxAdditionalJumps;
        }
        else
        {
            grounded = false;
        }
    }

    void checkIfWallSliding()
    {
        if (!grounded)
        {
            RaycastHit2D leftWallHit = Physics2D.Raycast(leftwall.position, Vector2.left, rayLength, groundLayer);
            RaycastHit2D rightWallHit = Physics2D.Raycast(rightwall.position, Vector2.right, rayLength, groundLayer);

            isWallSliding = ((leftWallHit || rightWallHit));
        }
        else
        {
            isWallSliding = false;
        }
    }
}
