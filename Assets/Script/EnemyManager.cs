using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

   // private EnemyWaves[] enemyWaves;
    
    private GameObject[] spawnPoints;

    private int waveCount;

    private int waveDifficulty;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpawnMonster();


    }

    private void SpawnMonster()
    {
        int randomPosition = Random.Range(0, spawnPoints.Length - 1);

    }
}
