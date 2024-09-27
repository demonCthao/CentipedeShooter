using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    public Sprite[] states;
    private SpriteRenderer _spriteRenderer;
    private int health;
    public int points = 1;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        health = states.Length;
        
    }

    private void Damage(int amount)
    {
        health -= amount;
        if (health >0)
        {
            _spriteRenderer.sprite = states[states.Length - health];
        }else{
            Destroy(gameObject);
            GameManager.Instance.IncreaseScore(points);
        }
    }

    public void Heal()
    {
        health = states.Length;
        _spriteRenderer.sprite = states[0];
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Dart")) {
            Damage(1);
        }
    }
}
