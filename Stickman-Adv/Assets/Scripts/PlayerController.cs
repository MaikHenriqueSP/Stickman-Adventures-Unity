using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private BoxCollider2D boxCollider2D;
    private Rigidbody2D rigidbody2D;
    public float MovementSpeed;
    public float JumpSpeed;

    
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
        rigidbody2D.velocity = new Vector2(horizontalMovement * MovementSpeed, rigidbody2D.velocity.y);
         
    }
}
