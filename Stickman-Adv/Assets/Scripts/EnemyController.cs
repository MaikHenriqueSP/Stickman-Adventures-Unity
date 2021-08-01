using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int LifePoints;
    public int CurrentLifePoints;
    public bool IsInvincible;
    public int TouchDamage;
    private Transform player;
    private bool isPlayerToTheLeft;
    private bool isTurnedLeft;

    protected void Start()
    {
        CurrentLifePoints = LifePoints;        
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isTurnedLeft = true;
    }

    public virtual void ReceiveDamage(int damage)
    {
        if (!IsInvincible)
        {
            CurrentLifePoints -= damage;
            CheckAndDestroyIfDefeated();
        }
    }

    public void CheckAndDestroyIfDefeated()
    {
        if (CurrentLifePoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            float enemyHorizontalPosition = transform.position.x;
            player.ReceiveDamage(TouchDamage, enemyHorizontalPosition);
        }
    }

    public void TurnToPlayer()
    {   
        isPlayerToTheLeft = player.transform.position.x < transform.position.x;

        if ( (isPlayerToTheLeft && !isTurnedLeft) || (!isPlayerToTheLeft && isTurnedLeft)  )
        {
            transform.Rotate(0f, 180f, 0f);
            isTurnedLeft = !isTurnedLeft;
        }
    }


    public int HorizontalMoveTowardsPlayer()
    {
        return isPlayerToTheLeft ? - 1 : 1;
    }

}
