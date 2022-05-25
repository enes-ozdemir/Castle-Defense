using System.Collections;
using System.Collections.Generic;
using Script.GameManagerScripts;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public GameData()
    {
        currentLevel = GameManager.currentLevel;
        money = GameManager.money;
        diamond = GameManager.diamond;
        
        weaponLevel = Weapon.weaponLevel;
        arrowLevel = Arrow.arrowLevel;
        weaponItemIndex = Weapon.weaponIndex;
        arrowItemIndex = Arrow.arrowIndex;
        
        additionalWeaponDamage = WeaponStats.additionalWeaponDamage;
        weaponBaseDamage = WeaponStats.weaponBaseDamage;
        weaponUpgradeGoldCost = WeaponStats.weaponUpgradeGoldCost;
        
        arrowUpgradeCost = ArrowStats.arrowUpgradeCost;
        fireInterval = ArrowStats.fireInterval;
        arrowCount = ArrowStats.arrowCount;
        arrowCountUpgradeCost = ArrowStats.arrowCountUpgradeCost;
        
        castleLevel = Castle.castleLevel;
        castleHealth = Castle.castleHealth;
        castleUpgradeCost = Castle.castleUpgradeCost;
    }

    //GameManager data
    public int currentLevel;
    public int money;
    public int diamond;

    //Upgrade levels
    public int weaponLevel;
    public int arrowLevel;
    public int castleLevel;
    public int arrowCount;

    //Weapon
    public int additionalWeaponDamage;
    public int weaponBaseDamage;
    public int weaponUpgradeGoldCost;

    //Arrow
    public int arrowUpgradeCost;
    public float fireInterval;
    public int arrowCountUpgradeCost;

    //Castle
    public int castleHealth;
    public int castleUpgradeCost;
    
    //Weapon Item
    public int weaponItemIndex;
    public int arrowItemIndex;
}