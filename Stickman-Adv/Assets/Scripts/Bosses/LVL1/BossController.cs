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
        
        if (CurrentLifePoints <= HalfBossLife() && CurrentLifePoints > 0)
        {
            animator.SetTrigger("rolling");
        }
        else if (IsDead())
        {
            animator.SetTrigger("dead");
        }
    }

    public float HalfBossLife()
    {
        return LifePoints / 2;
    }

    public void BecomeUnbeatable()
    {
        isInvincible = true;
    }

    public void BecomeBeatable()
    {
        isInvincible = false;
    }

    public bool IsDead()
    {
        return CurrentLifePoints <= 0;
    }

}
