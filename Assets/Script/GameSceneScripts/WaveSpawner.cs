using System;
using System.Collections.Generic;
using Script.GameManagerScripts;
using Script.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.GameSceneScripts
{
    public class WaveSpawner : MonoBehaviour
    {
        public List<Wave> enemyWaves = new();
        public List<GameObject> enemiesToSpawn = new();
        public Transform[] spawnLocations;
        public List<Vector3> usedSpawnLocations;

        public int currentWave;
        private int waveValue;
        private int waveDuration;

        [SerializeField] private float spawnInterval;
        public float spawnTimer;

        private bool isBossArrived;
        public int maxEnemyCount;
        private static int _currentEnemyCount;

        [SerializeField] private GameController gameController;

        private void Awake()
        {
            GetSpawnLocations();
            isBossArrived = false;

            currentWave = GameManager.selectedLevel;
            Debug.Log(currentWave + "currentWave");
            waveDuration = enemyWaves[currentWave].waveDuration;
            waveValue = enemyWaves[currentWave].waveValue;

            GenerateWave();

            SetEnemyCount();
        }

        private void GetSpawnLocations()
        {
            foreach (var loc in spawnLocations)
            {
                usedSpawnLocations.Add(loc.transform.position);
            }
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

        public void RemoveEnemyFromWave()
        {
            _currentEnemyCount--;
            CheckIfPlayerWon();
        }

        private void CheckIfPlayerWon()
        {
            if (_currentEnemyCount <= 0)
            {
                gameController.UpdateGameState(GameController.State.Win);
            }
        }

        private void FixedUpdate()
        {
            if (spawnTimer <= 0)
            {
                if (enemiesToSpawn.Count > 0)
                {
                    Vector3 spawnLoc;
                    int range;
                    if (usedSpawnLocations.Count > 0)
                    {
                        range = Random.Range(0, usedSpawnLocations.Count - 1);
                        spawnLoc = usedSpawnLocations[range];
                        usedSpawnLocations.Remove(spawnLoc);
                    }
                    else
                    {
                        if (spawnTimer > 0.1)
                        {
                            spawnTimer -= 0.1f;
                        }

                        GetSpawnLocations();
                        spawnLoc = spawnLocations[0].transform.position;
                        range = 1;
                    }

                    var enemyObject = Instantiate(enemiesToSpawn[0], spawnLoc, Quaternion.identity, transform);

                    enemyObject.GetComponent<SpriteRenderer>().sortingOrder = range;

                    enemyObject.GetComponent<EnemyManager>().prefab = enemyObject;

                    enemiesToSpawn.RemoveAt(0);

                    //Require further edit
                    if (enemiesToSpawn.Count < 2 && !isBossArrived)
                    {
                        SpawnBoss();
                    }
                    else if (enemiesToSpawn.Count < 10)
                    {
                        Debug.Log("Enemies to spawn is less than 10" + spawnTimer);
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
            if (enemyWaves[currentWave].boss != null)
            {
                AddCameraShake();
                SoundManager.PlaySound(SoundManager.Sound.BossGrowl);
                Debug.Log("Boss spawned");

                isBossArrived = true;
                spawnTimer = 1f;
                var bossPrefab = enemyWaves[currentWave].boss.enemyPrefab;
                var enemyObject = Instantiate(bossPrefab,
                    spawnLocations[3].position,
                    Quaternion.identity, transform);

                enemyObject.GetComponent<EnemyManager>().prefab = enemyObject;
                enemyObject.GetComponent<SpriteRenderer>().sortingOrder = 3;
            }
        }

        private void AddCameraShake()
        {
            var shake = gameObject.AddComponent<CameraShake>();
            shake.ShakeCaller(0.7f, 1f);
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
                    else break;
                }
                else Debug.Log("No enemy to generate");
            }

            enemiesToSpawn.Clear();
            enemiesToSpawn = generatedEnemies;
        }

        private Enemy GenerateEnemy()
        {
            var waveUnits = enemyWaves[currentWave].waveUnits;
            var enemyToGenerate = RandomEnemyGenerator.GetRandomEnemy(waveUnits);
            return enemyToGenerate;
        }

        private void OnDestroy()
        {
            SaveSystem.SaveGame();
            Debug.Log("OnDestroy called");
        }
    }
}