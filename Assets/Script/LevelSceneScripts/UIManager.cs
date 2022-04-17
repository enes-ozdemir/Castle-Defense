using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI diamondText;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button skillButton;

    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject upgradeUI;


    private void Start()
    {
        shopButton.onClick.AddListener(OnShopClick);
        upgradeButton.onClick.AddListener(OnUpgradeClick);
    }

    public void OnShopClick()
    {
        shopUI.SetActive(!shopUI.activeInHierarchy);
        upgradeUI.SetActive(false);
        GameManager.money += 100;
        GameManager.diamond += 100;
    }

    public void OnUpgradeClick()
    {
        upgradeUI.SetActive(!upgradeUI.activeInHierarchy);
        shopUI.SetActive(false);
        GameManager.money += 1000;
        GameManager.diamond += 1000;
    }

    private void FixedUpdate()
    {
        moneyText.text = GameManager.money + Constant.SpriteIndex;
        diamondText.text = GameManager.diamond + Constant.SpriteIndex;
    }
}