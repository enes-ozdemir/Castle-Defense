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

    public static int currentEnemyCount;
    private int maxEnemyCount;

    private void Start()
    {
        SoundManager.PlaySound(SoundManager.Sound.BattleMusic,0.5f,true);
        maxEnemyCount = waveSpawner.enemiesToSpawn.Count;
        currentEnemyCount = maxEnemyCount;
        levelText.text = "Level " + GameManager.selectedLevel;
        tempMoney = 0;
        tempDiamond = 0;
    }

    private void Update()
    {
        enemyCountText.text = currentEnemyCount + " / " + maxEnemyCount;
        moneyText.text = tempMoney + " " + Constant.SpriteIndex;
        diamondText.text = tempDiamond + " " + Constant.SpriteIndex;
    }

    public void LoadMapScene()
    {
        Cursor.visible = true;
        SceneManager.LoadScene("MapScene");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void NextLevel()
    {
        if (GameManager.selectedLevel <= GameManager.currentLevel)
        {
            Debug.Log("Selected level increased");
            GameManager.selectedLevel++;
        }

        levelText.text = "Level " + GameManager.selectedLevel;
        SceneManager.LoadScene("GameScene");
    }
}