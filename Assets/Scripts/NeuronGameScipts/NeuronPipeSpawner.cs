using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class NeuronPipeSpawner : MonoBehaviour
{
    [SerializeField] private float _maxTime = 1.5f;
    [SerializeField] private float _heightRange = 0.45f;
    [SerializeField] private GameObject _pipe;
    
    private static float highY;
    private static float lowY;
    private static GameObject pipe;
    
    private float _timer;

    private void Start()
    {
        SpawnPipe();
    }

    private void Update()
    {
        if (_timer > _maxTime)
        {
            SpawnPipe();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }

    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + new Vector3(0, Random.Range(-_heightRange, _heightRange));
        pipe = Instantiate(_pipe, spawnPos, Quaternion.identity);
        highY = spawnPos.y + 0.3186f;
        lowY = spawnPos.y - 0.3186f;
        Destroy(pipe, 15f);
    }


    public GameObject GetPipe()
    {
        return pipe;
    }
    
    public float GetHighY()
    {
        return highY;
    }
    
    public float GetLowY()
    {
        return lowY;
    }
}
