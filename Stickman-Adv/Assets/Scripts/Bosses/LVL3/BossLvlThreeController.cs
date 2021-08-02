using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLvlThreeController : EnemyController
{
    private Animator animator;
    private Transform player;
    private Rigidbody2D rigidbody2D;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (CurrentLifePoints <= LifePoints / 2)
        {
            //animator.SetTrigger("Anger");
        }

        IsBulletDetected();

       // if (IsBulletDetected())
      //  {
     //       animator.SetTrigger("Jump");
     //   }



        //if detect bullet set jump with probability
    }    

    //Called at the end of idle animation
    public void StartRunning()
    {
        animator.SetTrigger("Run");
    }

    public bool IsCloseToThePlayer(float minimumDistance)   //@TODO: refactor it to be reusable on enemy controller
    {
        return Mathf.Abs(player.position.x - rigidbody2D.position.x) <= minimumDistance;
    }
}
