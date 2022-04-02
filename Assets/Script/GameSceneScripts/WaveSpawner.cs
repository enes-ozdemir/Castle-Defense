using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public List<Wave> enemyWaves = new();
    public List<GameObject> enemiesToSpawn = new();
    public int currentWave;
    private int waveValue;

    public Transform[] spawnLocations;
    public int waveDuration;
    private float waveTimer;
    private float spawnInterval;
    private float spawnTimer;

    private Enemy currentEnemy;

    [SerializeField] private TextMeshProUGUI enemyCountText;
    private int maxEnemyCount;

    public static int currentEnemyCount;

    //new feature
    public float timeBetweenWaves = 2f;

    private void Awake()
    {
        currentWave = GameManager.CurrentLevel;
        waveDuration = 45;
        GenerateWave();

        maxEnemyCount = enemiesToSpawn.Count;
        currentEnemyCount = maxEnemyCount;
    }

    private void Update()
    {
        enemyCountText.text = currentEnemyCount + " / " + maxEnemyCount;
    }

    private void FixedUpdate()
    {
        
        
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                var spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)];
                var enemyObject = Instantiate(enemiesToSpawn[0],
                    spawnLoc.position,
                    Quaternion.identity);

                var enemyManager = enemyObject.GetComponent<EnemyManager>();
                enemyManager.prefab = enemyObject;

                enemiesToSpawn.RemoveAt(0);
                if (enemiesToSpawn.Count < 10)
                {
                    spawnTimer = 0.1f;
                }else spawnTimer = spawnInterval;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
            waveTimer -= Time.fixedDeltaTime;
        }
    }

    private void GenerateWave()
    {
        waveValue = currentWave * 200;
        GenerateEnemies();

        if (enemiesToSpawn != null)
        {
            spawnInterval = waveDuration / enemiesToSpawn.Count;
        }

        waveTimer = waveDuration;
    }

    private void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            Enemy enemyToGenerate = GenerateEnemy();

            if (enemyToGenerate != null)
            {
                int randEnemyCost = enemyToGenerate.enemyCost;
                
                if (waveValue - randEnemyCost >= 0)
                {
                    currentEnemy = enemyToGenerate;
                    generatedEnemies.Add(currentEnemy.enemyPrefab);
                    waveValue -= randEnemyCost;
                }
                else if (waveValue <= 5) break;
            }
            else Debug.Log("No enemy to generate");
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    private Enemy GenerateEnemy()
    {
        var waveUnits = enemyWaves[currentWave - 1].waveUnits;
        var enemyToGenerate = RandomEnemyGenerator.GetRandomEnemy(waveUnits);
        return enemyToGenerate;
    }
}