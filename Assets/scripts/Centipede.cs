using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Centipede : MonoBehaviour
{
    private List<CentipedeSegment> _segments = new List<CentipedeSegment>();
    public CentipedeSegment segmentPrefab;
    public Mushroom MushroomPrefab;
    
    public Sprite headSprite;
    public Sprite bodySprite;
    
    public int size = 12;
    public float speed = 20f;
    public int pointsHead = 100;
    public int pointsBody = 10;
    
    public LayerMask collisionLayMask;
    public BoxCollider2D homeArea;
    public LayerMask collisionMask;

    public void Respawn() {
        foreach (CentipedeSegment segment in _segments) {
            Destroy(segment.gameObject);
        }
        _segments.Clear();
        for (int i = 0; i < size; i++)
        {
            Vector2 position = GridPosition(transform.position) + (Vector2.left * i);
            CentipedeSegment segment = Instantiate(segmentPrefab, position, quaternion.identity);
            segment.spriteRenderer.sprite = i == 0 ? headSprite : bodySprite;
            segment.centipede = this;
            _segments.Add(segment);
        }

        for (int i = 0; i < _segments.Count; i++)
        {
            CentipedeSegment segment = _segments[i];
            segment.ahead = GetSegmentAt(i - 1);
            segment.behind = GetSegmentAt(i + 1);
        }
    }

    private CentipedeSegment GetSegmentAt(int index) {
        if (index >=0 && index < _segments.Count)
        {
            return _segments[index];
        }else
        {
            return null;
        }
    }

    private Vector2 GridPosition(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        return position;
    }

    public void remove(CentipedeSegment segment)
    {
        GameManager.Instance.IncreaseScore(segment.isHead ? pointsHead : pointsBody);
        Vector3 position = GridPosition(segment.transform.position);
        Instantiate(MushroomPrefab, position, Quaternion.identity);

        if (segment.ahead != null)
        {
            segment.ahead.behind = null;
        }
        if (segment.behind != null)
        {
            segment.behind.ahead = null;
            segment.behind.spriteRenderer.sprite = headSprite;
            segment.behind.updateHeadSegment();
        }
        
        _segments.Remove(segment);
        Destroy(segment.gameObject);
        if (_segments.Count ==0)
        {
            GameManager.Instance.NextLevel();
        }
    }
}
