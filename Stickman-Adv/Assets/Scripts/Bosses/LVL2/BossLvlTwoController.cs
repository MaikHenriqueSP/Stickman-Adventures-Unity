using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlTwoController : EnemyController
{
    public Transform player;
    public float TargetDistanceToPlayer;
    public float RetreatDistance;
    private Rigidbody2D rigidbody2D;
    public float movementSpeed;
    private bool isPlayerToTheLeft;
    private bool isTurnedLeft;

    //Shooting related variables
    public GameObject ShurikenPrefab;
    public Transform ShurikenGizmod;
    public int ShurikenDamage;
    private bool isShooting;


    //Animation related variables
    private Animator animator;
    private float actionTimer;

    void Start()
    {
        base.Start();
        isPlayerToTheLeft = true;
        isTurnedLeft = true;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (actionTimer > 0)
        {
            actionTimer -= Time.deltaTime;
            return;
        }

        TurnToPlayer();
        ChooseNextAction();
    }

    private void ChooseNextAction()
    {
        var probs = Random.Range(0.0f, 1.0f);

        if (probs > 0.2)
        {
            Move();
        } else
        {
            Shoot();
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

    public void Move()
    {
        float playerXPosition = player.transform.position.x;
        float currentXPosition = transform.position.x;
        float distance = Mathf.Abs(playerXPosition - currentXPosition);

        if (distance > TargetDistanceToPlayer)
        {
            WaitForAnimation("Boss_Walking");
            //transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime );
            rigidbody2D.velocity = Vector2.left * movementSpeed;
        } else
        {
            WaitForAnimation("Boss_Idle");
            rigidbody2D.velocity = Vector2.zero;
        }

    }

    private void Shoot()
    {
        WaitForAnimation("Boss_Throw");
 
        GameObject shuriken = Instantiate(ShurikenPrefab, ShurikenGizmod.position, Quaternion.identity);
        ShurikenController shurikenController = shuriken.GetComponent<ShurikenController>();
        Vector2 shootDirection = isTurnedLeft ? Vector2.left : Vector2.right;

        shurikenController.ShootDirection = shootDirection;
        shurikenController.Damage = ShurikenDamage;

        shurikenController.Shoot();

    }

    private void WaitForAnimation(string animationClipName)
    {
        animator.Play(animationClipName);
        AnimatorClipInfo[] animationInfo = animator.GetCurrentAnimatorClipInfo(0);

        actionTimer= animationInfo[0].clip.length;
                
    }


    public void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
        }
    }
}
