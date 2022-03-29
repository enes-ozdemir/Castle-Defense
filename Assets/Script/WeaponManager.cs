using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private int arrowLevel;
    [SerializeField] public int damage;

    [SerializeField] private Sprite[] bowSprites;
    public GameObject bowPrefab;
    [SerializeField] private Sprite[] arrowSprites;
    public GameObject arrowPrefab;
    [SerializeField] private Sprite currentBowPrefab;
    [SerializeField] private Sprite currentArrowPrefab;

    private SpriteRenderer bowSprite;
    private SpriteRenderer arrowSprite;


    private void Awake()
    {
        weaponLevel = 1;
        bowSprite = GetComponent<SpriteRenderer>();
        damage = 10;
        arrowSprite = arrowPrefab.GetComponent<SpriteRenderer>();
        SetWeaponLevel(1);
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        damage = level * 10;
    }

    private void Update()
    {
        //Move this to wave break later
        SetWeaponSprite();
    }

    private void SetWeaponSprite()
    {
        currentBowPrefab = bowSprites[weaponLevel];
        damage = weaponLevel * 20;
        bowSprite.sprite = currentBowPrefab;

        currentArrowPrefab = arrowSprites[arrowLevel];
        arrowSprite.sprite = currentArrowPrefab;
    }
}