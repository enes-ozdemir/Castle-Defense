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
        SetBowInfo();
        SetArrowInfo();
        SetCastleInfo();
        SetArrowCountInfo();
    }

    private void SetBowInfo()
    {
        bowText.text = GameManager.weapon.weaponLevel.ToString();
        bowDamageText.text = "Damage \n" + (GameManager.weapon.weaponStats.additionalWeaponDamage +
                             GameManager.weapon.weaponStats.weaponBaseDamage);
        bowUpgradeCost = GameManager.weapon.weaponStats.weaponUpgradeGoldCost;
        bowUpgradeText.text = bowUpgradeCost + Constant.SpriteIndex;
        bowImage.sprite = GameManager.weapon.weaponSprite;
    }

    private void SetArrowCountInfo()
    {
        arrowCountLevelText.text = GameManager.arrow.arrowStats.arrowCount.ToString();
        arrowCountText.text = "Arrow Count \n" + GameManager.arrow.arrowStats.arrowCount;
        arrowCountUpgradeCost = GameManager.arrow.arrowStats.arrowCountUpgradeCost;
        arrowCountUpgradeText.text = arrowCountUpgradeCost + Constant.SpriteIndex;
    }

    private void SetArrowInfo()
    {
        arrowText.text = GameManager.arrow.arrowLevel.ToString();
        arrowImage.sprite = GameManager.arrow.arrowSprite;
        arrowUpgradeCost = GameManager.arrow.arrowStats.arrowUpgradeCost;
        arrowDamageText.text = "Attack Speed \n" + GameManager.arrow.arrowStats.fireInterval;
        arrowUpgradeText.text = arrowUpgradeCost + Constant.SpriteIndex;
    }

    private void SetCastleInfo()
    {
        castleText.text = GameManager.castle.castleLevel.ToString();
        castleHealthText.text = "Health \n" + GameManager.castle.castleHealth;
        castleUpgradeText.text = GameManager.castle.castleUpgradeCost + Constant.SpriteIndex;
    }

    public void OnArrowCountUpgrade()
    {
        Debug.Log("OnArrowCountUpgrade called");
        var currentUpgradeCost = GameManager.arrow.arrowStats.arrowCountUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.arrow.arrowStats.arrowCountUpgradeCost += 10;
            GameManager.arrow.arrowStats.arrowCount++;
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
        var currentUpgradeCost = GameManager.arrow.arrowStats.arrowUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.arrow.arrowStats.arrowUpgradeCost += 10;
            GameManager.arrow.arrowStats.fireInterval += 0.05f;
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
        var currentUpgradeCost = GameManager.weapon.weaponStats.weaponUpgradeGoldCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.weapon.weaponStats.weaponUpgradeGoldCost += 10;
            GameManager.weapon.weaponStats.additionalWeaponDamage += 10;
            GameManager.weapon.weaponLevel++;
            SetBowInfo();
        }
        else
        {
            Debug.Log("Not enough money");
        }
    }
}