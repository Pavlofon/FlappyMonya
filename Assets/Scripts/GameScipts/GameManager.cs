using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject _gameOverCanvas;
    private bool alive = true;
    private void Awake()
    {
        if (instance == null )
        {
            instance = this;
        }

        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame ||
            Keyboard.current.xKey.wasPressedThisFrame ||
            Keyboard.current.cKey.wasPressedThisFrame ||
            Keyboard.current.vKey.wasPressedThisFrame)
        {
            if (alive)
            {
               Time.timeScale = 1f; 
            }
            
        }
    }

    public void GameOver()
    {
        _gameOverCanvas.SetActive(true);
        alive = false;
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        alive = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
