using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int LifePoints;
    public int CurrentLifePoints;
    public bool IsInvincible;
    public int TouchDamage;

    protected void Start()
    {
        CurrentLifePoints = LifePoints;        
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

}
