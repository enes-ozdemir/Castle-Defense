using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [SerializeField] private Level[] levelSystem;
    [SerializeField] private GameObject[] levelObjets;
    [SerializeField] public int currentLevel;

    private void Start()
    {
        SetLevelPrefab();
    }

    public void LevelChanged()
    {
        currentLevel++;
        SetLevelPrefab();
    }

    private void SetLevelPrefab()
    {
        for (int i = 0; i < currentLevel + 1; i++)
        {
            levelObjets[i].SetActive(true);
            var spriteRenderer = levelObjets[i].GetComponentsInChildren<Image>();
            var levelButton = levelObjets[i].GetComponent<Button>();
            levelButton.onClick.AddListener(() => LevelButtonClick(i));
            for (int j = 0; j < spriteRenderer.Length; j++)
            {
                switch (spriteRenderer[j].name)
                {
                    case "LevelImage":
                        spriteRenderer[j].sprite = levelSystem[i].levelImage;
                        break;
                    case "BackgroundImage":
                        spriteRenderer[j].sprite = levelSystem[i].backgroundImage;
                        break;
                    case "NumberImage":
                        spriteRenderer[j].sprite = levelSystem[i].levelCountImage;
                        break;
                    case "LockImage":
                        if (i > currentLevel + 1)
                        {
                            spriteRenderer[j].sprite = levelSystem[i].lockImage;
                        }

                        break;
                }
            }
        }
    }

    private void LevelButtonClick(int selectedLevel)
    {
        Debug.Log($" {selectedLevel} Level selected");
        //GameManager.Castle.castleHealth = 20000;
        GameManager.CurrentLevel = selectedLevel;
        GameManager.Weapon.weaponLevel = selectedLevel;
        SceneManager.LoadScene("GameScene");
    }
}