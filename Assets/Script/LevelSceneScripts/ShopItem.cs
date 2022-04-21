using Script.GameManagerScripts;
using Script.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.LevelSceneScripts
{
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
                CheckIfArrowBought();
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
                WeaponStats.weaponBaseDamage = item.baseDamage;
                Weapon.weaponSprite = item.itemSprite;
            }
        }

        private void CheckIfArrowBought()
        {
            if (displayedItem is ArrowItem item)
            {
                ArrowStats.fireInterval = item.baseInterval;
                Arrow.arrowSprite = item.itemSprite;
            }
        }

        private void SetItemUI()
        {
            buyButton.onClick.AddListener(SetButtonClick);

            Debug.Log("Start Shop Item");
            itemNameText.text = displayedItem.itemName;

            if (displayedItem.goldCost > 0)
            {
                goldCostText.text = displayedItem.goldCost + Constant.SpriteIndex;
            }
            else goldCostText.gameObject.SetActive(false);

            if (displayedItem.diamondCost > 0)
            {
                diamondCostText.text = displayedItem.diamondCost + Constant.SpriteIndex;
            }
            else diamondCostText.gameObject.SetActive(false);

            itemImage.sprite = displayedItem.itemSprite;
            if (displayedItem.specialOfferSprite != null)
            {
                specialOffer.sprite = displayedItem.specialOfferSprite;
                specialOfferText.text = displayedItem.specialOfferText;
            }

            CheckIfItemArrow();
            CheckIfItemWeapon();
        }

        private void CheckIfItemArrow()
        {
            if (displayedItem is ArrowItem item)
            {
                damageText.transform.parent.gameObject.SetActive(true);
                if (item != null) damageText.text = "Attack Speed \n" + item.baseInterval;
            }
            else if (displayedItem is not WeaponItem)
            {
                damageText.transform.parent.gameObject.SetActive(false);
            }
        }

        private void CheckIfItemWeapon()
        {
            if (displayedItem is WeaponItem item)
            {
                damageText.transform.parent.gameObject.SetActive(true);
                if (item != null) damageText.text = "Damage \n" + item.baseDamage;
            }
            else if (displayedItem is not ArrowItem)
            {
                damageText.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}