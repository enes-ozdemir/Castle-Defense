using TMPro;
using UnityEngine;

public class GameUIManager : MonoBehaviour
{
    public int tempMoney;

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
        tempMoney = 0;
    }

    private void Update()
    {
        enemyCountText.text = currentEnemyCount + " / " + maxEnemyCount;
        moneyText.text = tempMoney.ToString();
    }
}