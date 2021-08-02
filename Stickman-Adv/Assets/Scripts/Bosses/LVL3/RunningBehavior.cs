using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningBehavior : StateMachineBehaviour
{
    public float MovementSpeed;
    private Rigidbody2D rigidbody2D;
    BossLvlThreeController bossController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rigidbody2D = animator.GetComponent<Rigidbody2D>();
        bossController = animator.GetComponent<BossLvlThreeController>();
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int horizontalDirection = bossController.HorizontalMoveTowardsPlayer();
        rigidbody2D.velocity = new Vector2(MovementSpeed * horizontalDirection, rigidbody2D.velocity.y);

        if (bossController.IsCloseToThePlayer())
        {
            animator.SetTrigger("Attack");
        }

        if (bossController.IsBulletDetected())
        {
            var probability = Random.Range(0, 100);

            if (probability < 50)
            {
                animator.SetTrigger("Jump");
            }
        } 
    }
    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Jump");
    }
}
