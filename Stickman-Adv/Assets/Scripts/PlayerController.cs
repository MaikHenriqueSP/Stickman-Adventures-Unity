using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D BoxCollider2D;
    private Rigidbody2D Rigidbody2D;
    public float MovementSpeed;
    public float JumpSpeed;
    private bool IsPlayerOnTheGround = false;
    Animator animator;

    private float horizontalMovement;
    private bool isJumping;

    private bool isTurnedRight;

    
    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        isTurnedRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetKeyDown(KeyCode.Space);

        UpdateMovement();   
        UpdateAnimation();
        UpdateDirection();
    }

    private void FixedUpdate() 
    {         
        DetectIfPlayerIsOnTheGround();
    }
    
    private void UpdateMovement()
    {
        Rigidbody2D.velocity = new Vector2(horizontalMovement * MovementSpeed, Rigidbody2D.velocity.y);

        if (IsPlayerOnTheGround && isJumping) 
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, JumpSpeed);
        }
    }



    private void UpdateAnimation()
    {
        if (IsPlayerOnTheGround) 
        {
            if (horizontalMovement != 0)
            {
                animator.Play("Player_Run");
            } 
            else
            {
                animator.Play("Player_Idle");
            }
        } 
        else 
        {
            animator.Play("Player_Jump");
        }
    }

    private void UpdateDirection() 
    {
        if (horizontalMovement > 0 && !isTurnedRight)
        {
            isTurnedRight = !isTurnedRight;
            transform.Rotate(0f, horizontalMovement * 180f, 0f);            
        }
        else if (horizontalMovement < 0 && isTurnedRight) 
        {
            isTurnedRight = !isTurnedRight;
            transform.Rotate(0f, horizontalMovement * 180f, 0f);            
        }
    }

    void DetectIfPlayerIsOnTheGround() 
    {
        IsPlayerOnTheGround = false;

        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        float groundDetectionDistance = 0.04f;

        Vector3 boxColliderCenter = BoxCollider2D.bounds.center;
        boxColliderCenter.y = BoxCollider2D.bounds.min.y + (BoxCollider2D.bounds.extents.y / 2f); //Gets 1/4 of the lower half of the player box collider
        
        Vector3 boxSize = BoxCollider2D.bounds.size;
        float collisionAngle = 0f;
        Vector2 collisionDetectionDirection = Vector2.down;

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColliderCenter, boxSize, collisionAngle, collisionDetectionDirection, groundDetectionDistance, groundLayer);

        if (raycastHit2D.collider != null) 
        {
            IsPlayerOnTheGround = true;
        }

    }
}
