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

    private Animator animator;
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
        Move();
        TurnToPlayer();
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
            animator.Play("Boss_Walking");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime );
        } else
        {
            animator.Play("Boss_Idle");
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    private void Turn()
    {
        transform.Rotate(0f, 180f, 0f);
    }

    public void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
        }
    }
}
