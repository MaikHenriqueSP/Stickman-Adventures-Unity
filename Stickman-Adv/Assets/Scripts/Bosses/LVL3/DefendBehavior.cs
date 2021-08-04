using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefendBehavior : StateMachineBehaviour
{
    private BossLvlThreeController bossController;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController = animator.GetComponent<BossLvlThreeController>();
        bossController.IsInvincible = true;    
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossController.IsInvincible = false;    
    }
}
