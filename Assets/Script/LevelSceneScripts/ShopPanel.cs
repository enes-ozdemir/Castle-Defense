using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private SellableItem[] items;
    [FormerlySerializedAs("shopItemPrafab")] [SerializeField] private GameObject shopItemPrefab;

    void Start()
    {
        SetShopItems();
    }

    private void SetShopItems()
    {
        foreach (var item in items)
        {
            var newItem = Instantiate(shopItemPrefab, transform.position, quaternion.identity);
            newItem.transform.SetParent(gameObject.transform);
            newItem.GetComponent<ShopItem>().displayedItem = item;
        }
    }

    void Update()
    {
    }
}