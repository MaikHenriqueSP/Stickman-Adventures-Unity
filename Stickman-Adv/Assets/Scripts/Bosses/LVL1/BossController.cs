using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private Animator animator;

    void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();        
    }

    void Update()
    {
        float halfLifePoints = LifePoints / 2;
        if (CurrentLifePoints <= halfLifePoints && CurrentLifePoints > 0)
        {
            animator.SetTrigger("rolling");
        }

        if (CurrentLifePoints <= 0)
        {
            animator.SetTrigger("dead");
        }

    }

}
