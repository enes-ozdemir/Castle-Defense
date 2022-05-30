using Script.GameManagerScripts;
using Script.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
        [SerializeField] private Sprite buttonBackground;
        [SerializeField] private Sprite adsButtonBackground;

        [SerializeField] private Button buyButton;
        [SerializeField] private TextMeshProUGUI adText;

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
            if (displayedItem.isAd)
            {
                GoogleAds.currentItemReward = displayedItem;
                GoogleAds.ShowItemRewardAd();
            }
            else if (displayedItem is WeaponItem item)
            {
                WeaponStats.weaponBaseDamage = item.baseDamage;
                Weapon.weaponSprite = item.itemSprite;
                Weapon.weaponIndex = item.itemIndex;
            }
        }
        
        public static void SetRewardItem(SellableItem currentItemReward)
        {
            if (currentItemReward is ArrowItem arrowItem)
            {
                ArrowStats.fireInterval = arrowItem.baseInterval;
                Arrow.arrowSprite = arrowItem.itemSprite;
                Arrow.arrowIndex = arrowItem.itemIndex;
            }
            else if (currentItemReward is WeaponItem weaponItem)
            {
                WeaponStats.weaponBaseDamage = weaponItem.baseDamage;
                Weapon.weaponSprite = weaponItem.itemSprite;
                Weapon.weaponIndex = weaponItem.itemIndex;
            }
        }

        private void CheckIfArrowBought()
        {
            if (displayedItem.isAd)
            {
                GoogleAds.currentItemReward = displayedItem;
                GoogleAds.ShowItemRewardAd();
            }
            else if (displayedItem is ArrowItem item)
            {
                ArrowStats.fireInterval = item.baseInterval;
                Arrow.arrowSprite = item.itemSprite;
                Arrow.arrowIndex = item.itemIndex;
            }
        }

        private void SetItemUI()
        {
            buyButton.image.sprite = buttonBackground;
            buyButton.onClick.AddListener(SetButtonClick);

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
            CheckIfAdItem();
        }

        private void CheckIfAdItem()
        {
            if (displayedItem.isAd)
            {
                buyButton.image.sprite = adsButtonBackground;
                diamondCostText.gameObject.SetActive(false);
                goldCostText.gameObject.SetActive(false);
                adText.gameObject.SetActive(true);
            }
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