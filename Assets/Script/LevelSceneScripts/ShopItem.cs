using System;
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

    [SerializeField] private Image itemImage;
    [SerializeField] private Image specialOffer;

    [SerializeField] private Button buyButton;

    private const string SpriteIndex = "<sprite=0>";

    private void Start()
    {
        Debug.Log("Start Shop Item");
        itemNameText.text = displayedItem.itemName;
        if (displayedItem.goldCost > 0)
        {
            goldCostText.text = displayedItem.goldCost + SpriteIndex;
        }else goldCostText.gameObject.SetActive(false);

        if (displayedItem.diamondCost > 0)
        {
            diamondCostText.text = displayedItem.diamondCost + SpriteIndex;
        }else diamondCostText.gameObject.SetActive(false);

        itemImage.sprite = displayedItem.itemSprite;
        if (displayedItem.specialOfferSprite != null)
        {
            specialOffer.sprite = displayedItem.specialOfferSprite;
            specialOfferText.text = displayedItem.specialOfferText;
        }
    }
 
}