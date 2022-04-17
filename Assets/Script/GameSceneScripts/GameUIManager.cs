using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : MonoBehaviour
{
    public int tempMoney;
    public int tempDiamond;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private WaveSpawner waveSpawner;

    private int currentEnemyCount;
    private int maxEnemyCount;

    private void Start()
    {
        maxEnemyCount = waveSpawner.enemiesToSpawn.Count;
        currentEnemyCount = maxEnemyCount;
        levelText.text = "Level " + GameManager.selectedLevel;
        tempMoney = 0;
        tempDiamond = 0;
    }

    private void Update()
    {
        enemyCountText.text = currentEnemyCount + " / " + maxEnemyCount;
        moneyText.text = tempMoney + Constant.SpriteIndex;
        diamondText.text = tempDiamond + Constant.SpriteIndex;
    }

    public void LoadMapScene()
    {
        SceneManager.LoadScene("MapScene");
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }
}