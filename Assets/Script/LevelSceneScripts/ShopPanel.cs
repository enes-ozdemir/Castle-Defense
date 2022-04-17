using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    [FormerlySerializedAs("items")] [SerializeField]
    private SellableItem[] bowItems;

    [SerializeField] private SellableItem[] arrowItems;
    [SerializeField] private GameObject shopItemPrefab;

    [SerializeField] private GameObject arrowPanel;
    [SerializeField] private GameObject bowPanel;

    [SerializeField] private Button bowButton;
    [SerializeField] private Button arrowButton;

    void Start()
    {
        SetShopItems();
        SetButtonClicks();
    }

    private void SetButtonClicks()
    {
        bowButton.onClick.AddListener(DisplayBowPanel);
        arrowButton.onClick.AddListener(DisplayArrowPanel);
    }

    private void DisplayBowPanel()
    {
        bowPanel.SetActive(true);
        arrowPanel.SetActive(false);
    }

    private void DisplayArrowPanel()
    {
        bowPanel.SetActive(false);
        arrowPanel.SetActive(true);
    }

    private void SetShopItems()
    {
        SetBowItems();
        SetArrowItems();
    }

    private void SetBowItems()
    {
        foreach (var item in bowItems)
        {
            var newItem = Instantiate(shopItemPrefab, transform.position, quaternion.identity);
            newItem.transform.SetParent(bowPanel.gameObject.transform);
            newItem.GetComponent<ShopItem>().displayedItem = item;
        }
    }

    private void SetArrowItems()
    {
        foreach (var item in arrowItems)
        {
            var newItem = Instantiate(shopItemPrefab, transform.position, quaternion.identity);
            newItem.transform.SetParent(arrowPanel.gameObject.transform);
            newItem.GetComponent<ShopItem>().displayedItem = item;
        }
    }
}