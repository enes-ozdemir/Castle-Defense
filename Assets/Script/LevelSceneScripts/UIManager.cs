using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private int money;
    
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private Button skillButton;

    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject upgradeUI;


    private void Start()
    {
        money = GameManager.Money;
        shopButton.onClick.AddListener(OnShopClick);
        upgradeButton.onClick.AddListener(OnUpgradeClick);
    }

    private void OnShopClick()
    {
        shopUI.SetActive(true);
        GameManager.Money += 100;
    }

    private void OnUpgradeClick()
    {
        upgradeUI.SetActive(true);
        GameManager.Money += 1000;

    }

    private void Update()
    {
        money = GameManager.Money;
        moneyText.text = money.ToString();
    }
}