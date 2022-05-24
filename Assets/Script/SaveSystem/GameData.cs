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

    //Weapons levels
    public int weaponLevel;
    public int arrowLevel;

    //Weapon
    public int additionalWeaponDamage;
    public int weaponBaseDamage;
    public int weaponUpgradeGoldCost;

    //Arrow
    public int arrowUpgradeCost;
    public float fireInterval;
    public int arrowCount;
    public int arrowCountUpgradeCost;

    //Castle
    public int castleLevel;
    public int castleHealth;
    public int castleUpgradeCost;
}