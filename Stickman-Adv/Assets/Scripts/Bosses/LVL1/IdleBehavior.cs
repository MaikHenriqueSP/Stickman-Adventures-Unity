using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour
{

    public float duration = 15f;
    private BossController boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<BossController>();

        if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        }
    
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        duration -= Time.deltaTime;
        if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        } 
        else if (duration <= 0)
        {
            animator.SetTrigger("rolling");
        }    
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss.IsDead())
        {
            animator.SetTrigger("dead");
        }
    }
    
}
