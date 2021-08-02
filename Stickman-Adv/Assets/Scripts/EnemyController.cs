using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public int LifePoints;
    public int CurrentLifePoints;
    public bool IsInvincible;
    public int TouchDamage;
    private Transform player;
    private bool isPlayerToTheLeft;
    private bool isTurnedLeft;
    private BoxCollider2D boxCollider2D;
    public float AllowedDistanceToPlayer;

    protected void Start()
    {
        CurrentLifePoints = LifePoints;        
    }
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        boxCollider2D = GetComponent<BoxCollider2D>();
        isTurnedLeft = true;
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

    public void TurnToPlayer()
    {   
        isPlayerToTheLeft = player.transform.position.x < transform.position.x;

        if ( (isPlayerToTheLeft && !isTurnedLeft) || (!isPlayerToTheLeft && isTurnedLeft)  )
        {
            transform.Rotate(0f, 180f, 0f);
            isTurnedLeft = !isTurnedLeft;
        }
    }

    public int HorizontalMoveTowardsPlayer()
    {
        return isPlayerToTheLeft ? - 1 : 1;
    }

    public bool IsOnTheGround() 
    {
        int groundLayer = 1 << LayerMask.NameToLayer("Ground");
        float groundDetectionDistance = 0.05f;

        Vector3 boxColliderCenter = boxCollider2D.bounds.center;
        boxColliderCenter.y = boxCollider2D.bounds.min.y + boxCollider2D.bounds.extents.y;
        
        Vector3 boxSize = boxCollider2D.bounds.size;
        float collisionAngle = 0f;
        Vector2 collisionDetectionDirection = Vector2.down;

        RaycastHit2D raycastHit2D = Physics2D.BoxCast(boxColliderCenter, boxSize, collisionAngle, collisionDetectionDirection, groundDetectionDistance, groundLayer);

        if (raycastHit2D.collider != null) 
        {
           return true;
        }

        return false;
    }

    public bool IsBulletDetected()
    {
        Vector2 boxScale = new Vector2(0.01f , transform.localScale.y * 2);
        Vector2 direction = Vector2.right;
        float horizontalLengthCollider = transform.localScale.x;
        float yBoxStartPosition = boxCollider2D.bounds.min.y;
        Vector2 startPosition = new Vector2(transform.position.x + horizontalLengthCollider , yBoxStartPosition);
        
        if (isTurnedLeft)
        {
            direction = Vector2.left;
            startPosition = new Vector2(transform.position.x - horizontalLengthCollider, yBoxStartPosition);
        }

        RaycastHit2D hitInfo = Physics2D.BoxCast(startPosition, boxScale, 0f,  direction, AllowedDistanceToPlayer);

        if (hitInfo.collider != null)
        {   
            Debug.Log(hitInfo.collider.name);
            if (hitInfo.collider.CompareTag("Bullet"))         
            {
                return true;
            }
        }

        return false;
    }

}
