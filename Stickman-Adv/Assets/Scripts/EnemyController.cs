using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int LifePoints;
    public int CurretLifePoints;
    public bool isInvincible;
    public int TouchDamage;

    // Start is called before the first frame update
    void Start()
    {
        CurretLifePoints = LifePoints;        
    }

    public void ReceiveDamage(int damage)
    {
        Debug.Log($"{damage}");
        if (!isInvincible)
        {
            CurretLifePoints -= damage;
            CheckAndDestroyIfDefeated();
        }
    }

    public void CheckAndDestroyIfDefeated()
    {
        if (CurretLifePoints <= 0)
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
