using Unity.Mathematics;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private SellableItem[] items;
    [SerializeField] private GameObject shopItemPrafab;

    void Start()
    {
        SetShopItems();
    }

    private void SetShopItems()
    {
        foreach (var item in items)
        {
            var newItem = Instantiate(shopItemPrafab, transform.position, quaternion.identity);
            newItem.transform.SetParent(gameObject.transform);
            newItem.GetComponent<ShopItem>().displayedItem = item;
        }
    }

    void Update()
    {
    }
}