using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CentipedeSegment : MonoBehaviour
{
    public SpriteRenderer spriteRenderer { get; private set; }
    public Centipede centipede { get; set; }
    public CentipedeSegment ahead { get; set; }
    public CentipedeSegment behind { get; set; }
    public bool isHead => ahead == null;
    
    private Vector2 direction = Vector2.right +Vector2.down;
    private Vector2 targetPosition;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetPosition = transform.position;
    }

    private void Update()
    {
        if (isHead && Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            updateHeadSegment();
        }
        Vector2 currentPosition = transform.position;
        float speed = centipede.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(currentPosition, targetPosition, speed);

        Vector2 movementDirection = (targetPosition - currentPosition).normalized;
        float angle = Mathf.Atan2(movementDirection.y, movementDirection.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg,Vector3.forward);
    }

    public void updateHeadSegment()
    {
        Vector2 gridPosition = GridPosition(transform.position);

        targetPosition = gridPosition;
        targetPosition.x += direction.x;

        if (Physics2D.OverlapBox(targetPosition,Vector2.zero,0f,centipede.collisionLayMask))
        {
            direction.x = -direction.x;
            targetPosition.x = gridPosition.x;
            targetPosition.y = gridPosition.y + direction.y;

            Bounds homeBounds = centipede.homeArea.bounds;
            if ((direction.y == 1f && targetPosition.y > homeBounds.max.y) ||
                (direction.y == -1f && targetPosition.y < homeBounds.min.y))
            {
                direction.y = -direction.y;
                targetPosition.y = gridPosition.y + direction.y;
            }
        }
        
        if (behind != null)
        {
            behind.updateBodySegment();
        }
    }
    private void updateBodySegment()
    {
        targetPosition = GridPosition(ahead.transform.position);
        direction = ahead.direction;
        if (behind != null)
        {
            behind.updateBodySegment();
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.ResetRound();
            return;
        }
        if (other.collider.enabled && other.gameObject.layer == LayerMask.NameToLayer("Dart"))
        {
            other.collider.enabled = false;
            centipede.remove(this);
        }
    }
}
