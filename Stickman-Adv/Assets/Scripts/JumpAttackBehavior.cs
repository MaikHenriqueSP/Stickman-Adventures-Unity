using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttackBehavior : StateMachineBehaviour
{

    private Transform playerPos;
    public float speed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    
    }
    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector2 target = new Vector2(playerPos.position.x, animator.transform.parent.position.y);
        animator.transform.parent.position = Vector2.MoveTowards(animator.transform.parent.position, target, speed * Time.deltaTime);
        Debug.Log($"p={playerPos.position.x} s={speed}  s*t={speed * Time.deltaTime}");
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }


}
