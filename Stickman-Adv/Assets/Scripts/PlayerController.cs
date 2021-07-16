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

    //Shooting related variables
    private bool isShooting;
    private bool isShootingKeyPressed;
    private bool isShootingKeyReleased;
    private float shootingStartInstant;
    public int Damage;

    public GameObject bulletPrefab;

    public Transform bulletGizmod;

    //Damage related variables
    public int LifePoints;
    public int CurrentLifePoints;
    public bool IsInvincible;
    public bool IsTakingDamage;


    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();        
        isTurnedRight = true;
        CurrentLifePoints = LifePoints;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTakingDamage) 
        {
            return;
        }

        UpdateInputs();
        UpdateShooting();
        UpdateMovement();   
        UpdateAnimation();
        UpdateDirection();
    }

    private void UpdateInputs()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        isJumping = Input.GetKeyDown(KeyCode.Space);
        isShootingKeyPressed = Input.GetKeyDown(KeyCode.C);
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
        string nextAnimationName = "";

        if (IsPlayerOnTheGround) 
        {
            if (horizontalMovement != 0)
            {
                nextAnimationName = isShooting ? "Player_Run_Shoot" : "Player_Run";
            } 
            else
            {
                nextAnimationName = isShooting ? "Player_Idle_Shoot" : "Player_Idle";
            }
        } 
        else 
        {
            nextAnimationName = isShooting ? "Player_Jump_Shoot" : "Player_Jump";            
        }

        animator.Play(nextAnimationName);
    }

    private void UpdateShooting()
    {
        if (isShootingKeyPressed && !isShootingKeyReleased)
        {
            isShooting = true;
            isShootingKeyReleased = true;
            shootingStartInstant = Time.time;
            Invoke("Shoot", 0.1f);
        }

        if (isShooting && !isShootingKeyPressed)
        {
        }

        if (isShooting && isShootingKeyReleased)
        {
            float shootingTimeLength = Time.time - shootingStartInstant;

            if (shootingTimeLength >= 0.35f)
            {
                isShooting = false;
                isShootingKeyReleased = false;               
            }
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

    void Shoot() 
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletGizmod.position, Quaternion.identity);
        bullet.name = bulletPrefab.name;

        BulletController bulletController = bullet.GetComponent<BulletController>();
        Vector2 shootDirection = isTurnedRight ? Vector2.right : Vector2.left;
        bulletController.ShootDirection = shootDirection;
        bulletController.Damage = this.Damage;
        bulletController.Shoot();
    }

    //Damage related methods
    public void ReceiveDamage(int damageReceived, float enemyHorizontalPosition)
    {

        if (!IsInvincible)
        {
            CurrentLifePoints -= damageReceived;
            updateHealthbar();
            HandleReceivedDamage(enemyHorizontalPosition);
        }
    }

    private void updateHealthbar() {
        float remainingLifePointsPercentage = CurrentLifePoints / (float) LifePoints;
        Healthbar.HealthbarSingleton.SetValue(remainingLifePointsPercentage);
    }

    public void HandleReceivedDamage(float enemyHorizontalPosition)
    {
        if (CurrentLifePoints <= 0)
        {
            //Destroy(gameObject);
        }
        else if (!IsTakingDamage)
        {
            IsTakingDamage = true;
            IsInvincible = true;
            PushPlayerWhenIsTakingDamage(enemyHorizontalPosition);
            animator.Play("Player_Hit");
        }
    }

    private void PushPlayerWhenIsTakingDamage(float enemyHorizontalPosition) 
    {
        int hitDirection = (transform.position.x > enemyHorizontalPosition) ? 1 : -1;
        Vector2 forceDirection = new Vector2(4.50f * hitDirection, 3.50f);
        
        Rigidbody2D.velocity = Vector2.zero;
        Rigidbody2D.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    //Called by animation event in Player_Hit animation
    public void StopHitAnimation()
    { 
        IsTakingDamage = false;
        IsInvincible = false;
    }

    public bool IsPlayerDead()
    {
        return CurrentLifePoints <= 0;
    }
}
