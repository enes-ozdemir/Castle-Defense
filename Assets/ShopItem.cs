using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private SellableItem displayedItem;

    [Header("UI")] 
    private TextMeshProUGUI itemNameText;
    private TextMeshProUGUI goldCostText;
    private TextMeshProUGUI diamondCostText;
    private TextMeshProUGUI specialOfferText;

    private Image itemImage;
    private Image specialOffer;

    private Button buyButton;

    private void Start()
    {
        itemNameText.text = displayedItem.itemName;
        goldCostText.text = displayedItem.goldCost.ToString();
        diamondCostText.text = displayedItem.diamondCost.ToString();
        specialOfferText.text = displayedItem.specialOfferText;

        itemImage.sprite = displayedItem.itemSprite;
        specialOffer.sprite = displayedItem.specialOfferSprite;
        
    }

    public void BuyItem()
    {
        
        
    }
}

public class SellableItem:ScriptableObject
{

    public string itemName;
    public Sprite itemSprite;
    public int goldCost;
    public int diamondCost;
    public Sprite specialOfferSprite;
    public string specialOfferText;

}
