using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MushroomField : MonoBehaviour
{
    
    private BoxCollider2D area;
    public Mushroom prefab;
    public int amount = 50;

    private void Awake()
    {
        area = GetComponent<BoxCollider2D>();
        
    }

    public void Generate()
    {
        Bounds bounds = area.bounds;
        for (int i = 0; i < amount; i++)
        {
            Vector2 position = Vector2.zero;
            position.x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
            position.y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));
             Instantiate(prefab, position, Quaternion.identity, transform);
            
        }
    }

    public void Clear()
    {
        Mushroom[] _mushrooms = FindObjectsOfType<Mushroom>();
        foreach (Mushroom mushroom in _mushrooms)
        {
            Destroy(mushroom.gameObject);
        }
        
    }

    public void Heal()
    {
        Mushroom[] _mushrooms = FindObjectsOfType<Mushroom>();
        foreach (Mushroom mushroom in _mushrooms)
        {
            mushroom.Heal();
        }
    }
}
