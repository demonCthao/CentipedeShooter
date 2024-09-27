using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _collider2D;
    private Transform parent;
    public float speed;
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;

        _collider2D = GetComponent<BoxCollider2D>();
        _collider2D.enabled = false;
        parent = transform.parent;
    }

    private void Update()
    {
        if (_rigidbody2D.isKinematic && Input.GetButton("Fire1")) {
            transform.SetParent(null);
            _rigidbody2D.bodyType = RigidbodyType2D.Dynamic;
            _collider2D.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        if (!_rigidbody2D.isKinematic)
        {
            Vector2 position = _rigidbody2D.position;
            position += Vector2.up * speed * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(position);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Va chạm với " + other.gameObject.name);
        transform.SetParent(parent);
        transform.localPosition = new Vector3(0f, 0.9f, 0f);
        _rigidbody2D.bodyType = RigidbodyType2D.Kinematic;
        _collider2D.enabled = false;

    }
}
