using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;
    public float DurationTime;
    public Vector2 ShootDirection;
    public int Damage = 1;
    private float BulletLifeSpanTime;

    public float speed;
    
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();        
    }

    // Update is called once per frame
    void Update()
    {
        BulletLifeSpanTime -= Time.deltaTime;

        if (BulletLifeSpanTime < 0) 
        {
            Destroy(gameObject);
        }        
    }

    public void Shoot()
    {
        rigidbody2D.velocity = ShootDirection * speed;
        BulletLifeSpanTime = DurationTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.ReceiveDamage(Damage);
            }
        }

        if (!other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject, 0.02f);
        }
    }

    public void SetShootDirection(Vector2 direction) 
    {
        this.ShootDirection = direction;
    }
}
