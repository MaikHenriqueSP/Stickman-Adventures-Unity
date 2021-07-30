using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlTwoController : EnemyController
{
    public Transform player;
    public float TargetDistanceToPlayer;
    public float RetreatDistance;
    private Rigidbody2D rigidbody2D;
    private BoxCollider2D boxCollider2D;
    public float MovementSpeed;
    public float JumpSpeed;
    private bool isPlayerToTheLeft;
    private bool isTurnedLeft;
    public bool IsJumping;

    //Shooting related variables
    public GameObject ShurikenPrefab;
    public Transform ShurikenGizmod;
    public int ShurikenDamage;
    private bool isShooting;

    //Animation related variables
    private Animator animator;
    private float actionTimer;

    //Field of view
    public float viewDistance;

    //Shooting detection
    public float shotReactionDelay = 0.5f;
    private float reactionWindowWhenShotAt;
    public bool isDefendingFromShot;

    void Start()
    {
        base.Start();
        reactionWindowWhenShotAt = 0;
        isPlayerToTheLeft = true;
        isTurnedLeft = true;
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TurnToPlayer();
        reactionWindowWhenShotAt -= Time.deltaTime;

        if (reactionWindowWhenShotAt > 0 && !isDefendingFromShot)
        {
            actionTimer = 0;
        }

        if (reactionWindowWhenShotAt <= 0)
        {
            isDefendingFromShot = false;
        }

        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
            return;
        }

        IsInvincible = false;
        ChooseNextAction();
    }

    void FixedUpdate()
    {
        DetectCollision();
        CheckIfOnTheGround();  
    }

    void DetectCollision()
    {
        Vector2 boxScale = new Vector2(1f , transform.localScale.y);
        Vector2 direction = Vector2.right;
        float horizontalLengthCollider = transform.localScale.x;
        float yBoxStartPosition = boxCollider2D.bounds.min.y;
        Vector2 startPosition = new Vector2(transform.position.x + horizontalLengthCollider , yBoxStartPosition);
        
        if (isTurnedLeft)
        {
            direction = Vector2.left;
            startPosition = new Vector2(transform.position.x - horizontalLengthCollider, yBoxStartPosition);
        }

        RaycastHit2D hitInfo = Physics2D.BoxCast(startPosition, boxScale, 0f,  direction, viewDistance);

        if (hitInfo.collider != null)
        {   
            if (hitInfo.collider.CompareTag("Bullet"))         
            {
                reactionWindowWhenShotAt = shotReactionDelay;
            }
        }
    }

    void CheckIfOnTheGround() 
    {
        IsJumping = true;

        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        float groundDetectionDistance = 0.05f;

        Vector3 boxColliderCenter = boxCollider2D.bounds.center;
        boxColliderCenter.y = boxCollider2D.bounds.min.y + boxCollider2D.bounds.extents.y;
        
        Vector3 boxSize = boxCollider2D.bounds.size;
        float collisionAngle = 0f;
        Vector2 collisionDetectionDirection = Vector2.down;

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColliderCenter, boxSize, collisionAngle, collisionDetectionDirection, groundDetectionDistance, groundLayer);

        if (raycastHit2D.collider != null) 
        {
            IsJumping = false;
        }
    }

    private void ChooseNextAction()
    {
        var probability = Random.Range(0.0f, 1.0f) * 100;

        if (isDefendingFromShot || IsJumping)
        {
            return;
        }

        if (reactionWindowWhenShotAt > 0 && !isDefendingFromShot) {
            isDefendingFromShot = true;
            if (probability <= 40)
            {
                Debug.Log("Jump to defend");
                Jump();
            }
            else if (probability <= 80)
            {
                Debug.Log("Blocking attack");
                Defend();
            }
            else if (probability <= 100)
            {
               Shoot();
            }
            return;
        }

        if (IsPlayerFarAway())
        {
            if (probability <= 90)
            {
               Move();
            } 
            else if (probability <= 93)
            {
                Jump();
            }
            else if (probability <= 97) //@TODO: AND shoot delay
            {
                Shoot();
            }
            else if (probability <= 100)
            {
                Defend();
            }
        } 
        else
        {
            if (probability <= 50) //@TODO: and shoot delay
            {
                Shoot();
            }
            else if (probability <= 75)
            {
                Defend();
            }
            else if (probability <= 85)
            {
                Jump();
            }
            else if (probability <= 100)
            {
                Move();
            }
        }

    }

    private void TurnToPlayer()
    {        
        isPlayerToTheLeft = player.transform.position.x < transform.position.x;

        if ( (isPlayerToTheLeft && !isTurnedLeft) || (!isPlayerToTheLeft && isTurnedLeft)  )
        {
            transform.Rotate(0f, 180f, 0f);
            isTurnedLeft = !isTurnedLeft;
        }
    }

    public void Jump()
    {
        if (IsJumping)
        {
            Debug.Log("Is jumping alread");
            return;
        }

        IsJumping = true;
        WaitForAnimation("Boss_Jump");
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpSpeed);
    }

    public void Move()
    {
        if (IsPlayerFarAway())
        {
            rigidbody2D.velocity = isPlayerToTheLeft ? Vector2.left * MovementSpeed : Vector2.right * MovementSpeed;
            WaitForAnimation("Boss_Walking");
        } else
        {
            rigidbody2D.velocity = Vector2.zero;
            WaitForAnimation("Boss_Idle");
        }
    }

    private bool IsPlayerFarAway()
    {
        float playerXPosition = player.transform.position.x;
        float currentXPosition = transform.position.x;
        float distance = Mathf.Abs(playerXPosition - currentXPosition);
        
        return distance > TargetDistanceToPlayer;
    }

    public void Defend()
    {
        IsInvincible = true;
        WaitForAnimation("Boss_Defend", 0.3f);    
    }

    private void Shoot()
    {
        WaitForAnimation("Boss_Throw");
    }

    //Called at the end of the Boss_Throw animation
    public void ThrowShuriken()
    {
        GameObject shuriken = Instantiate(ShurikenPrefab, ShurikenGizmod.position, Quaternion.identity);
        ShurikenController shurikenController = shuriken.GetComponent<ShurikenController>();
        Vector2 shootDirection = isTurnedLeft ? Vector2.left : Vector2.right;
        shurikenController.ShootDirection = shootDirection;
        shurikenController.Damage = ShurikenDamage;
        shurikenController.Shoot();
    }

    private void WaitForAnimation(string animationClipName, float? preDefinedActionTimer = null)
    {
        animator.Play(animationClipName);
        AnimatorClipInfo[] animationInfo = animator.GetCurrentAnimatorClipInfo(0);

        actionTimer = preDefinedActionTimer ?? animationInfo[0].clip.length;
    }

    public void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
        }
    }
}
