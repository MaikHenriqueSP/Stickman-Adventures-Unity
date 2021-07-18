using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehavior : MonoBehaviour
{
    private float speed;
    private Rigidbody2D rigidbody2D;
    private float deadEndX;
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float speed, float deadEndX)
    {
        this.rigidbody2D.velocity = Vector3.right * speed;
        this.deadEndX = deadEndX;
    }

    void Update()
    {
        if (transform.position.x > deadEndX)
        {
            Destroy(gameObject);
        }        
    }

}
