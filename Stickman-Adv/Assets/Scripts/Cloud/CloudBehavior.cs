using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigidbody2D;
    private float deadEndX;
    private SpriteRenderer sprite;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (transform.position.x > deadEndX)
        {
            Destroy(gameObject);
        }        
    }    
    public void Move(float speed, float deadEndX, float alpha)
    {
        Debug.Log(alpha);
        this.rigidbody2D.velocity = Vector3.right * speed;
        this.deadEndX = deadEndX;
        SetAlpha(alpha);
    }

    private void SetAlpha(float alpha)
    {
        Color color = sprite.color;
        color.a = Mathf.Clamp(alpha, 0, 1);
        sprite.color = color;
    }

}
