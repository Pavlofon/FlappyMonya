using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeuronNewBehaviourScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("NeuronPlayer"))
        {
            NeuronScore.instanceNeuron.UpdateScore();
        }
    }
}
