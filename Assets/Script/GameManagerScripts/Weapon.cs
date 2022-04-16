using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel = 1;
    public Sprite weaponSprite;
    public WeaponStats weaponStats;

    private void Awake()
    {
        GameManager.weapon = this;
        weaponStats = new WeaponStats();
    }
}