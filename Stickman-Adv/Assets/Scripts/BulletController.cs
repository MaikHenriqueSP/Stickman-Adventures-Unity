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

    public GameObject ParticleExplosion;
    public GameObject HitExplosion;

    public float speed;

    public AudioSource Explosion;
    private Renderer renderer;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();        
        renderer = GetComponent<Renderer>();
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
        UpdateBulletDirection();        
    }

    private void UpdateBulletDirection()
    {
        if (ShootDirection.x < 0)
        {
            gameObject.transform.Rotate(0f, 180f, 0f);  
        }        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && renderer.enabled)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            Instantiate(ParticleExplosion, transform.position, Quaternion.identity);
            Instantiate(HitExplosion, transform.position, Quaternion.identity);
            Explosion.Play();
            
            if (enemy != null)
            {
                enemy.ReceiveDamage(Damage);
            }
        }

        if (!other.gameObject.CompareTag("Player"))
        {
            renderer.enabled = false;
            Destroy(gameObject, 1f);
        }
    }
    public void SetShootDirection(Vector2 direction) 
    {
        this.ShootDirection = direction;
    }
}
