using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NeuronGameManager : MonoBehaviour
{
    
    //Neuron activated
    [Range(0.1f, 10f)] public float Gamespeed = 1f;
    [Range(0.0001f, 1f)] public float MutationChance = 0.01f;
    [Range(0f, 1f)] public float MutationStrength = 0.5f;
    
    public int populationSize;//создаём численность популяции
    public GameObject prefab;//содержит префаб птички
    public int[] layers = new int[] { 4, 8, 8, 1 };//инициализация сети до нужного размера
    [Range(1f, 1000f)] public float timeframe;
    
    public List<NeuralNetwork> networks;
    private List<NeuronFlyBehavior> birds;
    public void Start()
    {
        if (instanceNeuron == null )
        {
            instanceNeuron = this;
        }

        InitNetworks();
        InvokeRepeating("CreateBirds", 0.1f, timeframe);//повторяющаяся функция
    }
    

    public void InitNetworks()
    {
        networks = new List<NeuralNetwork>();
        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Load("Assets/Save.txt");//при запуске загрузите сетевое сохранение
            networks.Add(net);
        }
    }

    
    // ReSharper disable Unity.PerformanceAnalysis
    public void CreateBirds()
    {
        Time.timeScale = Gamespeed;//устанавливает скорость игры, которая будет увеличиваться для ускорения тренировки
        if (birds != null)
        {
            for (int i = 0; i < birds.Count; i++)
            {
                if (!birds[i].IsDestroyed())
                {
                    GameObject.Destroy(birds[i].gameObject);//если в сцене есть Префабы, это позволит избавиться от них
                }
            }
        
            SortNetworks();//это сортирует сети и изменяет их
        }

        birds = new List<NeuronFlyBehavior>();
        for (int i = 0; i < populationSize; i++)
        {
            NeuronFlyBehavior bird = (Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0))).GetComponent<NeuronFlyBehavior>();//create birds
            bird.SetBrain(networks[i]);//развертывает сеть для каждой птицы
            birds.Add(bird);
        }
    }

    public void SortNetworks()
    {
        for (int i = 0; i < populationSize; i++)
        {
            if (birds[i].alive)
            {
                birds[i].UpdateFitness();
                networks[i].fitness = birds[i].fitness;
            }
        }
        Console.WriteLine(networks);
        networks.Sort();
        Console.WriteLine(networks);
        networks[populationSize - 1].Save("Assets/Save.txt");//сохраняет сетевые веса и смещения в файле для сохранения производительности сети
        for (int i = 0; i < populationSize / 2; i++)
        {
            networks[i] = networks[i + populationSize / 2].copy(new NeuralNetwork(layers));
            networks[i].Mutate((int)(1/MutationChance), MutationStrength);
        }
    }
    //Neuron activated
    
    public static NeuronGameManager instanceNeuron;
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
