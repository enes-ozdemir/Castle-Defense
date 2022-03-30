using System;

public static class GameManager
{
    //Player 
    public static Castle Castle;
    public static Weapon Weapon;
    public static int ArrowLevel = 1;
    public static int CurrentLevel = 1;
    public static int Money = 0;
}

public class WeaponStats
{
    public static int WeaponDamage=10;
    public int WeaponUpgradeCost = 100;

}