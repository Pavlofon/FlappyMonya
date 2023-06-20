using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class NeuronFlyBehavior : MonoBehaviour
{
   [SerializeField] private float _velocity4P = 1.5f;
   [SerializeField] private float _velocity3P = 1.167f;
   [SerializeField] private float _velocity2P = 0.83f;
   [SerializeField] private float _velocity1P = 0.5f;
   [SerializeField] private float _rotationSpeed = 10f;
   private NeuronPipeSpawner _pipeSpawner;
   private Rigidbody2D _rb;

   private void Start()
   {
      _rb = GetComponent<Rigidbody2D>();
      spawnTime = Time.time;
      _pipeSpawner = new NeuronPipeSpawner();
   }

   private void Update()
   {
      UseNeuralNetwork();
   }

   private void FixedUpdate()
   {
      transform.rotation = Quaternion.Euler(0,0,_rb.velocity.y * _rotationSpeed);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.gameObject.CompareTag("Respawn"))
      {
         alive = false;
         GameObject.Destroy(this.gameObject);
      }
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
   
   //Neuron Activated
   
   private float spawnTime;
   
   private NeuralNetwork brain;
   
   public bool alive = true;
   
   public float fitness { get; set; }
   
   public void SetBrain(NeuralNetwork brain)
   {
      this.brain = brain;
   }

   private void UseNeuralNetwork()
   {
      float[] inputs = new float[4];
      inputs[0] = _pipeSpawner.GetPipe().transform.position.x;
      inputs[1] = _pipeSpawner.GetHighY();
      inputs[2] = _pipeSpawner.GetLowY();
      inputs[3] = gameObject.transform.position.y;
   
      var output = brain.FeedForward(inputs);
      if (output[0]> 0 && output[0]<=0.25)
      {
         Jump1P();
      } else if (output[0]>0.25 && output[0]<=0.5)
      {
         Jump2P();
      } else if (output[0] > 0.5 && output[0] <= 0.75)
      {
         Jump3P();
      } else if (output[0]>0.75 && output[0]<=1)
      {
         Jump4P();
      }
   }

   public void UpdateFitness()
   {
      fitness = Time.time - spawnTime;
   }
}
