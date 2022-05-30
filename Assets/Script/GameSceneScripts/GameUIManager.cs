using Script.GameManagerScripts;
using Script.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Script.GameSceneScripts
{
    public class GameUIManager : MonoBehaviour
    {
        public int tempMoney;
        public int tempDiamond;

        public static int currentEnemyCount;
        private int maxEnemyCount;

        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI diamondText;
        [SerializeField] private TextMeshProUGUI enemyCountText;
        [SerializeField] private TextMeshProUGUI levelText;

        [SerializeField] private WaveSpawner waveSpawner;
        public GameObject rewardObject;

        [SerializeField] private GameObject tutorialUI;

        private void Start()
        {
            SoundManager.PlaySound(SoundManager.Sound.BattleMusic, 0.5f, true);
            InitUI();
            GoogleAds.HideBannerAd();

            OpenTutorialPopup();
        }

        private void OpenTutorialPopup()
        {
            if (GameManager.selectedLevel == 0)
            {
                tutorialUI.SetActive(true);
            }
        }

        private void InitUI()
        {
            maxEnemyCount = waveSpawner.enemiesToSpawn.Count;
            currentEnemyCount = maxEnemyCount;
            levelText.text = "Level " + (GameManager.selectedLevel + 1);
            tempMoney = 0;
            tempDiamond = 0;
        }

        private void Update()
        {
            enemyCountText.text = currentEnemyCount + " / " + maxEnemyCount;
            moneyText.text = tempMoney + "  " + Constant.SpriteIndex;
            diamondText.text = tempDiamond + "  " + Constant.SpriteIndex;
        }

        public void LoadMapScene()
        {
            Cursor.visible = true;
            SceneManagement.sceneNumber = Constant.MapScene;
            SceneManager.LoadScene("LoadingScene");
        }

        public void RestartLevel()
        {
            SceneManagement.sceneNumber = Constant.GameScene;
            SceneManager.LoadScene("LoadingScene");
        }

        public void NextLevel()
        {
            if (GameManager.selectedLevel <= GameManager.currentLevel && GameManager.currentLevel != Constant.LastLevel)
            {
                GameManager.selectedLevel++;
            }

            levelText.text = "Level " + GameManager.selectedLevel;
            SceneManagement.sceneNumber = Constant.GameScene;
            SceneManager.LoadScene("LoadingScene");
        }
    }
}