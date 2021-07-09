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
     //   Destroy(gameObject);
    }

    public void SetShootDirection(Vector2 direction) 
    {
        this.ShootDirection = direction;
    }
}
