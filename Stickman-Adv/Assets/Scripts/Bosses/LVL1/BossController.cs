using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    private Animator animator;
    public Animator cameraAnimator;

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

    public override  void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
            updateHealthbar();
        }
    }

    private void updateHealthbar() {
        float remainingLifePointsPercentage = CurrentLifePoints / (float) LifePoints;
        BossHealthBar.BossHealthbarSingleton.SetValue(remainingLifePointsPercentage);
    }


    public float HalfBossLife()
    {
        return LifePoints / 2;
    }

    public void BecomeUnbeatable()
    {
        IsInvincible = true;
    }

    public void BecomeBeatable()
    {
        IsInvincible = false;
    }

    public bool IsDead()
    {
        return CurrentLifePoints <= 0;
    }


    public void ShakeCamera()
    {
        cameraAnimator.SetTrigger("Earthquake");
    }

    public void UnFreeze()
    {
        animator.SetTrigger("jump");
    }


}
