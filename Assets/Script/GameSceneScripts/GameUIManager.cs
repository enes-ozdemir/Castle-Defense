using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    [SerializeField] private int money;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private WaveSpawner waveSpawner;

    private int currentEnemyCount;
    private int maxEnemyCount;


    private void Start()
    {
        maxEnemyCount = waveSpawner.enemiesToSpawn.Count;
        currentEnemyCount = maxEnemyCount;
        levelText.text = "Level " + GameManager.CurrentLevel;
    }

    private void Update()
    {
        enemyCountText.text = currentEnemyCount + " / " + maxEnemyCount;

        money = GameManager.Money;
        moneyText.text = money.ToString();
    }
}