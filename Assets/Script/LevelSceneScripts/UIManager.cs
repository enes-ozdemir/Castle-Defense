using Script.GameManagerScripts;
using Script.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.LevelSceneScripts
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyText;
        [SerializeField] private TextMeshProUGUI diamondText;
        [SerializeField] private Button shopButton;
        [SerializeField] private Button upgradeButton;

        [SerializeField] private GameObject shopUI;
        [SerializeField] private GameObject upgradeUI;
        [SerializeField] private GameObject winUI;
        [SerializeField] private GameObject tutorialUI;


        private void Start()
        {
            if (GameManager.currentLevel == 0)
            {
                tutorialUI.SetActive(true);
            }

            shopButton.onClick.AddListener(OnShopClick);
            upgradeButton.onClick.AddListener(OnUpgradeClick);

            if (GameManager.currentLevel == Constant.LastLevel)
            {
                InteractWithWinUI();
            }
        }

        public void OpenTutorialUI()
        {
            Debug.Log("Active");
            tutorialUI.SetActive(true);
        }

        public void CloseTutorialUI()
        {
            Debug.Log("Not Active");
            tutorialUI.SetActive(false);
        }

        public void InteractWithWinUI()
        {
            winUI.SetActive(!winUI.activeInHierarchy);
        }

        public void OnShopClick()
        {
            shopUI.SetActive(!shopUI.activeInHierarchy);
            upgradeUI.SetActive(false);
        }

        public void OnUpgradeClick()
        {
            upgradeUI.SetActive(!upgradeUI.activeInHierarchy);
            shopUI.SetActive(false);
        }

        private void FixedUpdate()
        {
            moneyText.text = GameManager.money + Constant.SpriteIndex;
            diamondText.text = GameManager.diamond + Constant.SpriteIndex;
        }
    }
}