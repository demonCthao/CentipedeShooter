using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blaster : MonoBehaviour
{
   private Rigidbody2D _rigidbody2D;
   private Vector2 direction;
   private Vector2 spawnPosition;
   public float speed;
   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      spawnPosition = transform.position;
   }

   private void Update()
   {
      direction.x = Input.GetAxis("Horizontal");
      direction.y = Input.GetAxis("Vertical");
   }

   private void FixedUpdate()
   {
      Vector2 position = _rigidbody2D.position;
      position += direction.normalized * speed * Time.fixedDeltaTime;
      _rigidbody2D.MovePosition(position);
   }

   public void Respawn()
   {
      transform.position = spawnPosition;
      gameObject.SetActive(true);
      
   }
}
