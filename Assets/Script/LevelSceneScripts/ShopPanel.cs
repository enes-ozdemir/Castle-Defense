using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Image bowImage;
    [SerializeField] private int upgradeCost;

    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI moneyText;


    private void Start()
    {
        bowImage.sprite = GameManager.Weapon.weaponSprite;
        upgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        money = GameManager.Money;
        upgradeButton.onClick.AddListener(OnUpgradeClick);
    }

    private void OnUpgradeClick()
    {
        var currentUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        var currentMoney = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Weapon.WeaponStats.WeaponUpgradeCost += 10;
            GameManager.Weapon.weaponLevel++;
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void Update()
    {
        upgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        money = GameManager.Money;
        moneyText.text = money.ToString();
    }
}