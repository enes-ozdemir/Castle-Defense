using System;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private int weaponLevel = 1;
    [SerializeField] private int arrowLevel;
    [SerializeField] public int damage;

    [SerializeField] private Sprite[] bowSprites;
    [SerializeField] private Sprite[] arrowSprites;

    public GameObject bowPrefab;
    public GameObject arrowPrefab;

    [SerializeField] private Sprite currentBowPrefab;
    [SerializeField] private Sprite currentArrowPrefab;

    private SpriteRenderer bowSprite;
    private SpriteRenderer arrowSprite;

    private void Awake()
    {
        bowSprite = GetComponent<SpriteRenderer>();
        arrowSprite = arrowPrefab.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //Move this to wave break later
        SetWeaponSprite();
    }

    private void SetWeaponSprite()
    {
        damage = GameManager.weapon.WeaponStats.WeaponDamage;
        bowSprite.sprite = GameManager.weapon.weaponSprite;
        arrowSprite.sprite = GameManager.arrow.arrowSprite;
    }
}