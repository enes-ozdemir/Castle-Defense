using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
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

        money = GameManager.money;
    }

    private void SetBowInfo()
    {
        bowText.text = "Level " + GameManager.weapon.weaponLevel;
        bowDamageText.text = "Damage " + GameManager.weapon.WeaponStats.WeaponDamage;
        bowUpgradeCost = GameManager.weapon.WeaponStats.WeaponUpgradeCost;
        bowUpgradeText.text = bowUpgradeCost.ToString();
        bowImage.sprite = GameManager.weapon.weaponSprite;
    }

    private void SetArrowInfo()
    {
        arrowText.text = "Level " + GameManager.arrow.arrowLevel;
        arrowImage.sprite = GameManager.arrow.arrowSprite;
        arrowUpgradeCost = GameManager.arrow.ArrowStats.arrowUpgradeCost;
        arrowDamageText.text = "Attack Speed " + GameManager.arrow.ArrowStats.fireInterval;
        arrowUpgradeText.text = arrowUpgradeCost.ToString();
    }

    private void SetCastleInfo()
    {
        Debug.Log("SetCastleInfo called");
        castleText.text = "Level " + GameManager.castle.castleLevel;
        castleHealthText.text = "Health " + GameManager.castle.castleHealth;
        castleUpgradeText.text = GameManager.castle.castleUpgradeCost.ToString();
    }

    public void OnArrowUpgrade()
    {
        Debug.Log("OnArrowUpgrade called");
        var currentUpgradeCost = GameManager.arrow.ArrowStats.arrowUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.arrow.ArrowStats.arrowUpgradeCost += 10;
            GameManager.arrow.ArrowStats.fireInterval += 0.1f;
            GameManager.arrow.arrowLevel++;
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
        var currentUpgradeCost = GameManager.castle.castleUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.castle.castleUpgradeCost += 10;
            GameManager.castle.castleHealth += 100;
            GameManager.castle.castleLevel++;
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
        var currentUpgradeCost = GameManager.weapon.WeaponStats.WeaponUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.weapon.WeaponStats.WeaponUpgradeCost += 10;
            GameManager.weapon.WeaponStats.WeaponDamage += 10;
            GameManager.weapon.weaponLevel++;
            SetBowInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    private void Update()
    {
        money = GameManager.money;
        moneyText.text = money.ToString();
    }
}