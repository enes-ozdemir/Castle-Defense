using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [Header("Bow")] [SerializeField] private Button bowUpgradeButton;
    [SerializeField] private Image bowImage;
    [SerializeField] private TextMeshProUGUI bowText;
    [SerializeField] private TextMeshProUGUI bowDamageText;
    [SerializeField] private TextMeshProUGUI bowUpgradeText;
    private int bowUpgradeCost;

    [Header("Arrow")] [SerializeField] private Button arrowUpgradeButton;
    [SerializeField] private Image arrowImage;
    [SerializeField] private TextMeshProUGUI arrowText;
    [SerializeField] private TextMeshProUGUI arrowDamageText;
    [SerializeField] private TextMeshProUGUI arrowUpgradeText;
    private int arrowUpgradeCost;

    [Header("Castle")] [SerializeField] private Button castleUpgradeButton;
    [SerializeField] private Image castleImage;
    [SerializeField] private TextMeshProUGUI castleText;
    [SerializeField] private TextMeshProUGUI castleHealthText;
    [SerializeField] private TextMeshProUGUI castleUpgradeText;
    private int castleUpgradeCost;

    [Header("Money")] [SerializeField] private int money;
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
        bowDamageText.text = "Damage " + GameManager.Weapon.WeaponStats.WeaponDamage;
        bowUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        bowUpgradeText.text = bowUpgradeCost.ToString();
        bowImage.sprite = GameManager.Weapon.weaponSprite;
    }

    private void SetArrowInfo()
    {
        arrowText.text = "Level " + GameManager.Arrow.arrowLevel;
        arrowImage.sprite = GameManager.Arrow.arrowSprite;
        arrowUpgradeCost = GameManager.Arrow.ArrowStats.arrowUpgradeCost;
        arrowDamageText.text = "Attack Speed " + GameManager.Arrow.ArrowStats.fireInterval;
        arrowUpgradeText.text = arrowUpgradeCost.ToString();
    }

    private void SetCastleInfo()
    {
        Debug.Log("SetCastleInfo called");
        castleText.text = "Level " + GameManager.Castle.castleLevel;
        castleHealthText.text = "Health " + GameManager.Castle.castleHealth;
        castleUpgradeText.text = GameManager.Castle.castleUpgradeCost.ToString();
    }

    public void OnArrowUpgrade()
    {
        Debug.Log("OnArrowUpgrade called");
        var currentUpgradeCost = GameManager.Arrow.ArrowStats.arrowUpgradeCost;
        var currentMoney = GameManager.Money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Arrow.ArrowStats.arrowUpgradeCost += 10;
            GameManager.Arrow.ArrowStats.fireInterval += 0.1f;
            GameManager.Arrow.arrowLevel++;
            SetArrowInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void OnCastleUpgrade()
    {
        Debug.Log("OnCastleUpgrade called");
        var currentUpgradeCost = GameManager.Castle.castleUpgradeCost;
        var currentMoney = GameManager.Money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Castle.castleUpgradeCost += 10;
            GameManager.Castle.castleHealth += 100;
            GameManager.Castle.castleLevel++;
            SetCastleInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void OnBowUpgrade()
    {
        Debug.Log("OnBowUpgrade called");
        var currentUpgradeCost = GameManager.Weapon.WeaponStats.WeaponUpgradeCost;
        var currentMoney = GameManager.Money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.Money -= currentUpgradeCost;
            GameManager.Weapon.WeaponStats.WeaponUpgradeCost += 10;
            GameManager.Weapon.WeaponStats.WeaponDamage += 10;
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
        money = GameManager.Money;
        moneyText.text = money.ToString();
    }
}