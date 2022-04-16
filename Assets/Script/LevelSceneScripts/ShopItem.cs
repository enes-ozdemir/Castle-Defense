using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public SellableItem displayedItem;

    [Header("UI")] [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI goldCostText;
    [SerializeField] private TextMeshProUGUI diamondCostText;
    [SerializeField] private TextMeshProUGUI specialOfferText;
    [SerializeField] private TextMeshProUGUI damageText;

    [SerializeField] private Image itemImage;
    [SerializeField] private Image specialOffer;

    [SerializeField] private Button buyButton;

    private const string SpriteIndex = "<sprite=0>";

    private void Start()
    {
        SetItemUI();
    }

    private void SetButtonClick()
    {
        if (displayedItem.goldCost <= GameManager.money && displayedItem.diamondCost <= GameManager.diamond)
        {
            GameManager.money -= displayedItem.goldCost;
            GameManager.diamond -= displayedItem.diamondCost;

            CheckIfWeaponBought();
        }
        else
        {
            PopupManager.ShowCannotAffordPopup();
            Debug.Log("Cant afford");
        }
    }

    private void CheckIfWeaponBought()
    {
        if (displayedItem is WeaponItem item)
        {
            GameManager.weapon.weaponStats.weaponBaseDamage = item.baseDamage;
            GameManager.weapon.weaponSprite = item.itemSprite;
        }
    }

    private void SetItemUI()
    {
        buyButton.onClick.AddListener(SetButtonClick);

        Debug.Log("Start Shop Item");
        itemNameText.text = displayedItem.itemName;

        if (displayedItem.goldCost > 0)
        {
            goldCostText.text = displayedItem.goldCost + SpriteIndex;
        }
        else goldCostText.gameObject.SetActive(false);

        if (displayedItem.diamondCost > 0)
        {
            diamondCostText.text = displayedItem.diamondCost + SpriteIndex;
        }
        else diamondCostText.gameObject.SetActive(false);

        itemImage.sprite = displayedItem.itemSprite;
        if (displayedItem.specialOfferSprite != null)
        {
            specialOffer.sprite = displayedItem.specialOfferSprite;
            specialOfferText.text = displayedItem.specialOfferText;
        }

        CheckIfItemWeapon();
    }

    private void CheckIfItemWeapon()
    {
        if (displayedItem is WeaponItem item)
        {
            if (item != null) damageText.text = "Damage \n" + item.baseDamage;
        }
        else
        {
            damageText.transform.parent.gameObject.SetActive(false);
        }
    }
}