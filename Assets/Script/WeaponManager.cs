using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private int weaponLevel;
    [SerializeField] public int damage;

    [SerializeField] private Sprite[] bowSprites;
    public GameObject bowPrefab;
    [SerializeField] private Sprite[] arrowSprites;
    public GameObject arrowPrefab;

    [SerializeField] private Sprite currentBowPrefab;
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        damage *= level*10;
    }
}
