using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlThreeController : EnemyController
{
    private Animator animator;
    

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (CurrentLifePoints <= LifePoints / 2)
        {
            //animator.SetTrigger("Anger");
        }        
    }    

    //Called at the end of idle animation
    public void StartRunning()
    {
        animator.SetTrigger("Run");
    }
}
