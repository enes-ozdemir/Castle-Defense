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

    [Header("Arrow Count")] [SerializeField]
    private Button arrowCountUpgradeButton;

    [SerializeField] private TextMeshProUGUI arrowCountLevelText;
    [SerializeField] private TextMeshProUGUI arrowCountText;
    [SerializeField] private TextMeshProUGUI arrowCountUpgradeText;
    private int arrowCountUpgradeCost;

    private void Start()
    {
        Debug.Log(Weapon.weaponLevel + "level");
        SetBowInfo();
        SetArrowInfo();
        SetCastleInfo();
        SetArrowCountInfo();
    }

    private void SetBowInfo()
    {
        bowText.text = Weapon.weaponLevel.ToString();
        bowDamageText.text = "Damage \n" + (WeaponStats.additionalWeaponDamage +
                                            WeaponStats.weaponBaseDamage);
        bowUpgradeCost = WeaponStats.weaponUpgradeGoldCost;
        bowUpgradeText.text = bowUpgradeCost + Constant.SpriteIndex;
        bowImage.sprite = Weapon.weaponSprite;
    }

    private void SetArrowCountInfo()
    {
        arrowCountLevelText.text = ArrowStats.arrowCount.ToString();
        arrowCountText.text = "Arrow Count \n" + ArrowStats.arrowCount;
        arrowCountUpgradeCost = ArrowStats.arrowCountUpgradeCost;
        arrowCountUpgradeText.text = arrowCountUpgradeCost + Constant.SpriteIndex;
    }

    private void SetArrowInfo()
    {
        arrowText.text = Arrow.arrowLevel.ToString();
        arrowImage.sprite = Arrow.arrowSprite;
        arrowUpgradeCost = ArrowStats.arrowUpgradeCost;
        arrowDamageText.text = "Attack Speed \n" + ArrowStats.fireInterval;
        arrowUpgradeText.text = arrowUpgradeCost + Constant.SpriteIndex;
    }

    private void SetCastleInfo()
    {
        castleText.text = Castle.castleLevel.ToString();
        castleHealthText.text = "Health \n" + Castle.castleHealth;
        castleUpgradeText.text = Castle.castleUpgradeCost + Constant.SpriteIndex;
    }

    public void OnArrowCountUpgrade()
    {
        Debug.Log("OnArrowCountUpgrade called");
        var currentUpgradeCost = ArrowStats.arrowCountUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            ArrowStats.arrowCountUpgradeCost += 5000 * ArrowStats.arrowCount;
            ArrowStats.arrowCount++;
            SetArrowCountInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }

    public void OnArrowUpgrade()
    {
        Debug.Log("OnArrowUpgrade called");
        var currentUpgradeCost = ArrowStats.arrowUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            ArrowStats.arrowUpgradeCost += 500;
            ArrowStats.fireInterval += 0.05f;
            Arrow.arrowLevel++;
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
        var currentUpgradeCost = Castle.castleUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            Castle.castleUpgradeCost += 500 * (Castle.castleLevel / 2);
            Castle.castleHealth += 500;
            Castle.castleLevel++;
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
        var currentUpgradeCost = WeaponStats.weaponUpgradeGoldCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            WeaponStats.weaponUpgradeGoldCost += 10;
            WeaponStats.additionalWeaponDamage += 10;
            Weapon.weaponLevel++;
            SetBowInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}