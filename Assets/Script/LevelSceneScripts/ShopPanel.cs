using System.Collections.Generic;
using Sirenix.Utilities.Editor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Script.LevelSceneScripts
{
    public class ShopPanel : MonoBehaviour
    {
        [SerializeField] private SellableItem[] bowItems;
        [SerializeField] private List<GameObject> bowItemObjects;

        [SerializeField] private SellableItem[] arrowItems;
        [SerializeField] private List<GameObject> arrowItemObjects;

        [SerializeField] private GameObject shopItemPrefab;

        [SerializeField] private Button bowButton;
        [SerializeField] private Button arrowButton;

        void Start()
        {
            bowItemObjects = new List<GameObject>();
            arrowItemObjects = new List<GameObject>();
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
            SetBowItems();
        }

        private void DisplayArrowPanel()
        {
            SetArrowItems();
        }

        private void SetShopItems()
        {
            foreach (var item in arrowItems)
            {
                var newItem = Instantiate(shopItemPrefab, transform.position, quaternion.identity, transform);
                newItem.GetComponent<ShopItem>().displayedItem = item;
                arrowItemObjects.Add(newItem);
            }

            foreach (var item in bowItems)
            {
                var newItem = Instantiate(shopItemPrefab, transform.position, quaternion.identity, transform);
                newItem.GetComponent<ShopItem>().displayedItem = item;
                bowItemObjects.Add(newItem);
            }

            SetBowItems();
        }

        private void ChangeItemActive(List<GameObject> gameObjects, bool isActive)
        {
            foreach (var item in gameObjects)
            {
                item.GameObject().SetActive(isActive);
            }
        }

        private void SetBowItems()
        {
            ChangeItemActive(arrowItemObjects, false);
            ChangeItemActive(bowItemObjects, true);
        }

        private void SetArrowItems()
        {
            ChangeItemActive(bowItemObjects, false);
            ChangeItemActive(arrowItemObjects, true);
        }
    }
}