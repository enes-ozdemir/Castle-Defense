using System;
using Firebase.Analytics;
using Script.GameManagerScripts;
using Script.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Script.LevelSceneScripts
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] levels;
        [SerializeField] public int currentLevel;
        [SerializeField] private Level levelObject;

        private void Awake()
        {
            LoadGameData();
        }

        private void Start()
        {
            SoundManager.PlaySound(SoundManager.Sound.BackgroundMusic);

            currentLevel = GameManager.currentLevel;
            SetLevelPrefab();

        }
    private void LoadGameData()
    {
        GameData gameData = SaveSystem.LoadGameData();

        if(gameData==null)
        {
            Debug.Log("Game data is null");
            return;
        }
        Debug.Log("Game data is loaded from the save");
        GameManager.currentLevel = gameData.currentLevel;
        GameManager.money = gameData.money;
        GameManager.diamond = gameData.diamond;

        Weapon.weaponLevel = gameData.weaponLevel;
        Arrow.arrowLevel = gameData.arrowLevel;

        WeaponStats.additionalWeaponDamage = gameData.additionalWeaponDamage;
        WeaponStats.weaponBaseDamage = gameData.weaponBaseDamage;
        WeaponStats.weaponUpgradeGoldCost = gameData.weaponUpgradeGoldCost;

        ArrowStats.arrowUpgradeCost = gameData.arrowUpgradeCost;
        ArrowStats.fireInterval = gameData.fireInterval;
        ArrowStats.arrowCount = gameData.arrowCount;
        ArrowStats.arrowCountUpgradeCost = gameData.arrowCountUpgradeCost;

        Castle.castleLevel = gameData.castleLevel;
        Castle.castleHealth = gameData.castleHealth;
        Castle.castleUpgradeCost = gameData.castleUpgradeCost;
    }

        private void SetLevelPrefab()
        {
            SetLevelButtonClicks();
            SetOldLevelImages();
            SetLockedLevelImages();
            SetCurrentLevelImage();
        }

        private void SetCurrentLevelImage()
        {
            var currentLevelImage = levels[currentLevel].gameObject.GetComponent<Image>();
            currentLevelImage.sprite = levelObject.currentLevelImage;
        }

        private void SetLockedLevelImages()
        {
            for (int j = levels.Length - 1; j > levels.Length - (levels.Length - currentLevel); j--)
            {
                var levelImage = levels[j].gameObject.GetComponent<Image>();
                levelImage.sprite = levelObject.lockImage;
            }
        }

        private void SetOldLevelImages()
        {
            for (int i = 0; i < currentLevel; i++)
            {
                var levelImage = levels[i].gameObject.GetComponent<Image>();
                levelImage.sprite = levelObject.oldLevelImage;
            }
        }

        private void SetLevelButtonClicks()
        {
            for (int i = 0; i <= currentLevel; i++)
            {
                var index = i;
                var levelButton = levels[index].GetComponent<Button>();
                levelButton.onClick.AddListener(() => LevelButtonClick(index));
            }
        }

        private void LevelButtonClick(int selectedLevel)
        {
            SaveSystem.SaveGame();
            Debug.Log($" {selectedLevel} Level selected");
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart);
            GameManager.selectedLevel = selectedLevel;
            SceneManagement.sceneNumber = Constant.GameScene;
            SceneManager.LoadScene("LoadingScene");
        }
    }
}