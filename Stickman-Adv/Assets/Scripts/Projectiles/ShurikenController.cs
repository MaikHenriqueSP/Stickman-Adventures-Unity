using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    public int Damage;
    public Rigidbody2D Rigidbody2D;
    public float speed;
    public Vector2 ShootDirection;
    private float BulletLifeSpanTime;
    public float DurationTime;

    
    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();        
    }
        
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
        Rigidbody2D.velocity = ShootDirection * speed;
        BulletLifeSpanTime = DurationTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
                player.ReceiveDamage(Damage, transform.position.x);
            }
        }

        if (!other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 0.02f);
        }
    }
    public void SetShootDirection(Vector2 direction) 
    {
        this.ShootDirection = direction;
    }
}
