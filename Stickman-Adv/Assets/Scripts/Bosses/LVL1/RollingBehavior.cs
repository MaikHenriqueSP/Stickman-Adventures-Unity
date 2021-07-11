using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBehavior : StateMachineBehaviour
{
    private Transform player;
    public float rollingSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();        
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(player.position.x, animator.transform.parent.position.y);
        animator.transform.parent.position = Vector2.MoveTowards(animator.transform.parent.position, target, rollingSpeed * Time.deltaTime);        
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }

    
}
