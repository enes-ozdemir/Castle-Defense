using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private Button bowUpgradeButton;
    [SerializeField] private Image bowImage;
     private int bowUpgradeCost;

    [SerializeField] private Button arrowUpgradeButton;
    [SerializeField] private Image arrowImage;
     private int arrowUpgradeCost;

    [SerializeField] private Button castleUpgradeButton;
    [SerializeField] private Image castleImage;
    private int castleUpgradeCost;

    [SerializeField] private int money;
    [SerializeField] private TextMeshProUGUI moneyText;


    private void Start()
    {
        SetBowInfo();
        SetArrowInfo();
        SetCastleInfo();

        money = GameManager.Money;
    }

    private void SetBowInfo()
    {
        bowImage.sprite = GameManager.Weapon.weaponSprite;
        bowUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        bowUpgradeButton.onClick.AddListener(OnBowUpgrade);
    }

    private void SetArrowInfo()
    {
        arrowImage.sprite = GameManager.Arrow.arrowSprite;
        arrowUpgradeCost = GameManager.Arrow.ArrowStats.ArrowUpgradeCost;
        arrowUpgradeButton.onClick.AddListener(OnArrowUpgrade);
    }

    private void SetCastleInfo()
    {
        castleUpgradeCost = GameManager.Castle.castleUpgradeCost;
        castleUpgradeButton.onClick.AddListener(OnCastleUpgrade);
    }

    private void OnArrowUpgrade()
    {
        var currentUpgradeCost = GameManager.Arrow.ArrowStats.ArrowUpgradeCost;
        var currentMoney = GameManager.Money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Arrow.ArrowStats.ArrowUpgradeCost += 10;
            GameManager.Arrow.arrowLevel++;
            SetArrowInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void OnCastleUpgrade()
    {
        var currentUpgradeCost = GameManager.Castle.castleUpgradeCost;
        var currentMoney = GameManager.Money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Castle.castleUpgradeCost += 10;
            GameManager.Castle.castleLevel++;
            SetCastleInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void OnBowUpgrade()
    {
        var currentUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        var currentMoney = GameManager.Money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Weapon.WeaponStats.WeaponUpgradeCost += 10;
            GameManager.Weapon.weaponLevel++;
            SetBowInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void Update()
    {
        bowUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        money = GameManager.Money;
        moneyText.text = money.ToString();
    }
}