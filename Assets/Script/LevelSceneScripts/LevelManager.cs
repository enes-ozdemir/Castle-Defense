using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levels;
    [SerializeField] public int currentLevel;
    [SerializeField] private Level levelObject;

    private void Start()
    {
        currentLevel = GameManager.currentLevel;
        SetLevelPrefab();
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
        Debug.Log($" {selectedLevel} Level selected");
        //GameManager.Castle.castleHealth = 20000;
        GameManager.selectedLevel = selectedLevel;
        SceneManager.LoadScene("GameScene");
    }
}