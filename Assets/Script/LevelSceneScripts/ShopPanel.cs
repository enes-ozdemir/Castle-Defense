using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("Bow")]
    [SerializeField] private Button bowUpgradeButton;
    [SerializeField] private Image bowImage;
    [SerializeField] private TextMeshProUGUI bowText;
    private int bowUpgradeCost;
    
    [Header("Arrow")]
    [SerializeField] private Button arrowUpgradeButton;
    [SerializeField] private Image arrowImage;
    [SerializeField] private TextMeshProUGUI arrowText;
    private int arrowUpgradeCost;
    
    [Header("Castle")]
    [SerializeField] private Button castleUpgradeButton;
    [SerializeField] private Image castleImage;
    [SerializeField] private TextMeshProUGUI castleText;
    private int castleUpgradeCost;
    
    [Header("Money")]
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
        bowText.text = "Level " + GameManager.Weapon.weaponLevel;
        bowImage.sprite = GameManager.Weapon.weaponSprite;
        bowUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        bowUpgradeButton.onClick.AddListener(OnBowUpgrade);
    }

    private void SetArrowInfo()
    {
        arrowText.text = "Level " + GameManager.Arrow.arrowLevel;
        arrowImage.sprite = GameManager.Arrow.arrowSprite;
        arrowUpgradeCost = GameManager.Arrow.ArrowStats.ArrowUpgradeCost;
        arrowUpgradeButton.onClick.AddListener(OnArrowUpgrade);
    }

    private void SetCastleInfo()
    {
        Debug.Log("SetCastleInfo called");
        castleText.text = "Level " + GameManager.Castle.castleLevel;
        castleUpgradeCost = GameManager.Castle.castleUpgradeCost;
        castleUpgradeButton.onClick.AddListener(OnCastleUpgrade);
    }

    private void OnArrowUpgrade()
    {
        Debug.Log("OnArrowUpgrade called");
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
        Debug.Log("OnCastleUpgrade called");
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
        Debug.Log("OnBowUpgrade called");
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