using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    [SerializeField] private SellableItem[] items;
    [SerializeField] private ShopItem shopItems;
    [SerializeField] private GameObject shopItemPrafab;

    void Start()
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