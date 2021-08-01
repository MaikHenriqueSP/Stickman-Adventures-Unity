using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehavior : StateMachineBehaviour
{
    public float MovementSpeed;
    private Transform player;
    private Rigidbody2D rigidbody2D;
    public float distanceToAttack;
    BossLvlThreeController bossController;    
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<BossLvlThreeController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.TurnToPlayer();

        int horizontalDirection = bossController.HorizontalMoveTowardsPlayer();
        rigidbody2D.velocity = new Vector2(MovementSpeed * horizontalDirection, rigidbody2D.velocity.y);

        if (Mathf.Abs(player.position.x - rigidbody2D.position.x) <= distanceToAttack)
        {
            Debug.Log("Attack");
        }
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //animator.ResetTrigger("Attack");
    }
}
