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

    private const string SpriteIndex = "<sprite=0>";


    private void Start()
    {
        SetBowInfo();
        SetArrowInfo();
        SetCastleInfo();

    }

    private void SetBowInfo()
    {
        bowText.text = GameManager.weapon.weaponLevel.ToString();
        bowDamageText.text = "Damage \n" + GameManager.weapon.weaponStats.additionalWeaponDamage;
        bowUpgradeCost = GameManager.weapon.weaponStats.weaponUpgradeCost;
        bowUpgradeText.text = bowUpgradeCost.ToString() + SpriteIndex;;
        bowImage.sprite = GameManager.weapon.weaponSprite;
    }

    private void SetArrowInfo()
    {
        arrowText.text = GameManager.arrow.arrowLevel.ToString();
        arrowImage.sprite = GameManager.arrow.arrowSprite;
        arrowUpgradeCost = GameManager.arrow.ArrowStats.arrowUpgradeCost ;
        arrowDamageText.text = "Attack Speed \n" + GameManager.arrow.ArrowStats.fireInterval;
        arrowUpgradeText.text = arrowUpgradeCost.ToString() + SpriteIndex;;
    }

    private void SetCastleInfo()
    {
        Debug.Log("SetCastleInfo called");
        castleText.text = GameManager.castle.castleLevel.ToString();
        castleHealthText.text = "Health \n" + GameManager.castle.castleHealth;
        castleUpgradeText.text = GameManager.castle.castleUpgradeCost.ToString() + SpriteIndex;
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
        var currentUpgradeCost = GameManager.weapon.weaponStats.weaponUpgradeCost;
        var currentMoney = GameManager.money;
        if (currentUpgradeCost <= currentMoney)
        {
            GameManager.money -= currentUpgradeCost;
            GameManager.weapon.weaponStats.weaponUpgradeCost += 10;
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