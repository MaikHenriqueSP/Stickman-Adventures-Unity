using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlTwoController : EnemyController
{
    private Rigidbody2D rigidbody2D;
    
    //Animation related variables
    private Animator animator;
    private float actionTimer;

    [Header("Moving Settings")]
    public float MovementSpeed;
    public float JumpSpeed;
    private bool IsJumping;

    [Header("Combat Settings")]
    public float TargetDistanceToPlayer;
    public float RetreatDistance;
    public float viewDistance;
    public float MeleeAttackDistance;    

    [Header("Shooting Settings")]
    public GameObject ShurikenPrefab;
    public Transform ShurikenGizmod;
    public int ShurikenDamage;
    private bool isShooting;

    [Header("Shot Reaction Settings")]
    public float shotReactionDelay = 0.5f;
    private float reactionWindowWhenShotAt;
    private bool isDefendingFromShot;

    void Start()
    {
        base.Start();
        reactionWindowWhenShotAt = 0;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TurnToPlayer();
        reactionWindowWhenShotAt -= Time.deltaTime;
        UpdateDefeated();

        if (IsDead())
        {
            return;
        }


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
        UpdateBulletDetection();
        UpdateJumping();  
    }

    private void UpdateDefeated()
    {
        if (IsDead())
        {
            animator.Play("Boss_Dying");
        }

    }

    private void UpdateBulletDetection()
    {
        if (IsBulletDetected())
        {            
            reactionWindowWhenShotAt = shotReactionDelay;            
        }
    }

    private void UpdateJumping() 
    {
        IsJumping = true;

        if (IsOnTheGround())
        {
            IsJumping = false;            
        }
    }

    private void ChooseNextAction()
    {
        if (isDefendingFromShot || IsJumping)
        {
            return;
        }

        if (GetDistanceToPlayer() < MeleeAttackDistance)
        {
            MeleeAttack();
            return;
        }

        var probability = Random.Range(0, 100);

        if (reactionWindowWhenShotAt > 0 && !isDefendingFromShot) {
            isDefendingFromShot = true;
            if (probability <= 40)
            {
                Jump();
            }
            else if (probability <= 80)
            {
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

    public void Jump()
    {
        if (IsJumping)
        {
            return;
        }

        IsJumping = true;
        WaitForAnimation("Boss_Jump");
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, JumpSpeed);
    }

    public void MeleeAttack()
    {
        WaitForAnimation("Boss_Melee");
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
        float distance = GetDistanceToPlayer();        
        return distance > TargetDistanceToPlayer;
    }

    private float GetDistanceToPlayer()
    {
        float playerXPosition = player.transform.position.x;
        float currentXPosition = transform.position.x;
        return Mathf.Abs(playerXPosition - currentXPosition);
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

    public override void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
        }
    }
}
