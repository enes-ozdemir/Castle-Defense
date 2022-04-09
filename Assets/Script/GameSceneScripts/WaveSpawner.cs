using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    public List<Wave> enemyWaves = new();
    public List<GameObject> enemiesToSpawn = new();
    public Transform[] spawnLocations;

    public int currentWave;
    private int waveValue;
    public int waveDuration;

    [SerializeField] private float spawnInterval;
    private float spawnTimer;

    private bool isBossArrived;
    public int maxEnemyCount;
    private static int _currentEnemyCount;

    private void Awake()
    {
        isBossArrived = false;

        //Set Wave settings
        currentWave = GameManager.selectedLevel;
        Debug.Log(currentWave + "currentWave");
        waveDuration = enemyWaves[currentWave].waveDuration;
        waveValue = enemyWaves[currentWave].waveValue;

        GenerateWave();

        SetEnemyCount();
    }

    private void SetEnemyCount()
    {
        maxEnemyCount = enemiesToSpawn.Count;
        if (enemyWaves[currentWave].boss != null)
        {
            _currentEnemyCount = maxEnemyCount + 1;
        }
        else
        {
            _currentEnemyCount = maxEnemyCount;
        }
    }

    public static void RemoveEnemyFromWave()
    {
        _currentEnemyCount--;
        CheckIfPlayerWon();
    }

    private static void CheckIfPlayerWon()
    {
        if (_currentEnemyCount <= 0) GameController.Instance.UpdateGameState(GameController.State.Win);
    }

    private void FixedUpdate()
    {
        if (spawnTimer <= 0)
        {
            if (enemiesToSpawn.Count > 0)
            {
                var spawnLoc = spawnLocations[Random.Range(0, spawnLocations.Length - 1)].position;

                var enemyObject = Instantiate(enemiesToSpawn[0], spawnLoc, Quaternion.identity, transform);

                enemyObject.GetComponent<EnemyManager>().prefab = enemyObject;

                enemiesToSpawn.RemoveAt(0);

                //Require further edit
                if (enemiesToSpawn.Count < 5 && !isBossArrived)
                {
                    SpawnBoss();
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

    private void SpawnBoss()
    {
        var shake = gameObject.AddComponent<CameraShake>();
        shake.ShakeCaller(1, 1f);
        isBossArrived = true;
        spawnTimer = 1f;
        Debug.Log("Boss spawned");

        var bossPrefab = enemyWaves[currentWave].boss.enemyPrefab;
        var enemyObject = Instantiate(bossPrefab,
            spawnLocations[1].position,
            Quaternion.identity, transform);

        enemyObject.GetComponent<EnemyManager>().prefab = enemyObject;
    }

    private void GenerateWave()
    {
        GenerateEnemies();

        if (enemiesToSpawn != null)
        {
            spawnInterval = waveDuration / (float) enemiesToSpawn.Count;
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
                    generatedEnemies.Add(enemyToGenerate.enemyPrefab);
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