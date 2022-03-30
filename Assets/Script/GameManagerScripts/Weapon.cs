using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel = 1;
    public Sprite weaponSprite;
    public WeaponStats WeaponStats;

    private void Awake()
    {
        GameManager.Weapon = this;
        WeaponStats = new WeaponStats();
    }
}