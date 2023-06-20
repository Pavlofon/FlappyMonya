using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyBehavior : MonoBehaviour
{
   [SerializeField] private float _velocity4P = 1.5f;
   [SerializeField] private float _velocity3P = 1.167f;
   [SerializeField] private float _velocity2P = 0.83f;
   [SerializeField] private float _velocity1P = 0.5f;
   [SerializeField] private float _rotationSpeed = 10f;

   private Rigidbody2D _rb;

   private void Start()
   {
      _rb = GetComponent<Rigidbody2D>();
   }

   private void Update()
   {
      if (Keyboard.current.zKey.wasPressedThisFrame)
      {
         Jump1P();
      } else if (Keyboard.current.xKey.wasPressedThisFrame)
      {
         Jump2P();
      } else if (Keyboard.current.cKey.wasPressedThisFrame)
      {
         Jump3P();
      } else if (Keyboard.current.vKey.wasPressedThisFrame)
      {
         Jump4P();
      }
   }

   private void FixedUpdate()
   {
      transform.rotation = Quaternion.Euler(0,0,_rb.velocity.y * _rotationSpeed);
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
      GameManager.instance.GameOver();
   }
   
   private void Jump4P()
   {
      _rb.velocity = Vector2.up * _velocity4P;
   }
   private void Jump3P()
   {
      _rb.velocity = Vector2.up * _velocity3P;
   }
   private void Jump2P()
   {
      _rb.velocity = Vector2.up * _velocity2P;
   }
   private void Jump1P()
   {
      _rb.velocity = Vector2.up * _velocity1P;
   }
}
