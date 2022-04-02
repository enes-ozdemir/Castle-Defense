using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
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
    [SerializeField] private float spawnInterval;
    private float spawnTimer;

    private Enemy currentEnemy;

    [SerializeField] private TextMeshProUGUI enemyCountText;
    private int maxEnemyCount;
    public static int currentEnemyCount;
    private bool isBossArrived;

    private void Awake()
    {
        isBossArrived = false;
        //Set Wave settings
        currentWave = GameManager.CurrentLevel;
        waveDuration = enemyWaves[currentWave].waveDuration;
        waveValue = enemyWaves[currentWave].waveValue;

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
                    Quaternion.identity, transform);

                var enemyManager = enemyObject.GetComponent<EnemyManager>();
                enemyManager.prefab = enemyObject;

                enemiesToSpawn.RemoveAt(0);
                
                if (enemiesToSpawn.Count < 5 && !isBossArrived)
                {
                    SpawnBoss(spawnLoc);
                }
                else if (enemiesToSpawn.Count < 25)
                {
                    Debug.Log("Enemies to spawn is less than 25");
                    spawnTimer = 0.1f;
                }
                else spawnTimer = spawnInterval;
            }
        }
        else
        {
            spawnTimer -= Time.fixedDeltaTime;
        }
    }

    private void SpawnBoss(Transform spawnLoc)
    {
        isBossArrived = true;
        spawnTimer = 1f;
        Debug.Log("Boss spawned");
        var enemyObject = Instantiate(enemyWaves[currentWave].boss.enemyPrefab,
            spawnLoc.position,
            Quaternion.identity, transform);

        var enemy = enemyObject.GetComponent<EnemyManager>();
        enemy.prefab = enemyObject;
    }

    private void GenerateWave()
    {
        GenerateEnemies();

        if (enemiesToSpawn != null)
        {
            spawnInterval = waveDuration / enemiesToSpawn.Count;
        }
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