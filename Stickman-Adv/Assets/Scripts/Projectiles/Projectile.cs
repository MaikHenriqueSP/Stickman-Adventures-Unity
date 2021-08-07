using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Settings")]
    public float DurationTime;
    public float Speed;
    public int Damage = 1;
    private Vector2 ShootDirection;
    private float LifeSpanTime;

    private Rigidbody2D rigidbody2D;
   
    protected Renderer renderer;

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();        
        renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        LifeSpanTime -= Time.deltaTime;

        if (LifeSpanTime < 0) 
        {
            Destroy(gameObject);
        }        
    }

    public void Shoot()
    {
        rigidbody2D.velocity = ShootDirection * Speed;
        LifeSpanTime = DurationTime;
        UpdateBulletDirection();        
    }

    private void UpdateBulletDirection()
    {
        if (ShootDirection.x < 0)
        {
            gameObject.transform.Rotate(0f, 180f, 0f);  
        }        
    }

    public void SetShootDirection(Vector2 direction) 
    {
        this.ShootDirection = direction;
    }
}

