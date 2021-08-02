using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlThreeController : EnemyController
{
    private Animator animator;
    private Transform player;
    private Rigidbody2D rigidbody2D;
    private bool isEnraged;
    private int currentStage; //@TODO: create enum class for this
    private bool isBulletDetected;
    public float minimumDistance;
    

    void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        TurnToPlayer();
        if (CurrentLifePoints <= LifePoints / 2 && currentStage == 0)
        {
            animator.SetTrigger("Anger");
            currentStage++;
        }

        isBulletDetected = IsBulletDetected();

        switch (currentStage) {
            case 0:
                ChooseFirstStageAction();
                break;
            case 1:
                ChooseSecondaStageAction();
                break;
        }
    }

    private void ChooseFirstStageAction()
    {
        if (IsCloseToThePlayer())
        {
            animator.SetTrigger("Attack");
        } else if (isBulletDetected)
        {
            var probability = Random.Range(0, 100);

            if (probability < 50)
            {
                animator.SetTrigger("Jump");
            }
        } 
        else 
        {
            animator.SetTrigger("Run");
        }

    }

    private void ChooseSecondaStageAction()
    {

    }

    //Called at the end of idle animation
    public void StartRunning()
    {
        animator.SetTrigger("Run");
    }

    public bool IsCloseToThePlayer()   //@TODO: refactor it to be reusable on enemy controller
    {
        return Mathf.Abs(player.position.x - rigidbody2D.position.x) <= minimumDistance;
    }
}
