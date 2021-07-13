using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();        
    }

    void Update()
    {
        if (CurrentLifePoints <= (LifePoints / 2))
        {
            animator.SetTrigger("Rolling");
        }

        if (CurrentLifePoints <= 0)
        {
            animator.SetTrigger("Dead");
        }

    }

}
