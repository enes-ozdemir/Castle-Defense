using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public List<EnemyUnit> enemies = new List<EnemyUnit>();
    public List<GameObject> enemiesToSpawn = new List<GameObject>();
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

    private void Start()
    {
        currentWave = GameManager.CurrentLevel;
        waveDuration = currentWave * 15;
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
                spawnTimer = spawnInterval;
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
        waveValue = currentWave * 100;
        GenerateEnemies();

        spawnInterval = waveDuration / enemiesToSpawn.Count;
        waveTimer = waveDuration;
    }

    private void GenerateEnemies()
    {
        List<GameObject> generatedEnemies = new List<GameObject>();
        while (waveValue > 0)
        {
            int randEnemyId = Random.Range(0, enemies.Count);
            int randEnemyCost = enemies[randEnemyId].cost;

            if (waveValue - randEnemyCost >= 0)
            {
                currentEnemy = enemies[randEnemyId].enemyObject;
                generatedEnemies.Add(currentEnemy.enemyPrefab);
                waveValue -= randEnemyCost;
            }
            else if (waveValue <= 2) break;
        }

        enemiesToSpawn.Clear();
        enemiesToSpawn = generatedEnemies;
    }

    [System.Serializable]
    public class EnemyUnit
    {
        public Enemy enemyObject;
        public int cost;
    }
}