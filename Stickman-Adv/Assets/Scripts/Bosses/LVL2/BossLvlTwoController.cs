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

    private Animator animator;
    void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
        float playerXPosition = player.transform.position.x;
        float currentXPosition = transform.position.x;
        float distance = Mathf.Abs(playerXPosition - currentXPosition);
        if (distance > TargetDistanceToPlayer)
        {
            animator.Play("Boss_Walking");
            rigidbody2D.velocity = new Vector2(playerXPosition * movementSpeed, rigidbody2D.velocity.y);
        } else if (distance < RetreatDistance)
        {
            animator.Play("Boss_Walking");
            rigidbody2D.velocity = new Vector2(playerXPosition * movementSpeed * (-1), rigidbody2D.velocity.y);
        } else 
        {
            //animator.Play("Boss_Idle");
            rigidbody2D.velocity = Vector2.zero;
        }
    }

    public override void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
        }
    }
}
