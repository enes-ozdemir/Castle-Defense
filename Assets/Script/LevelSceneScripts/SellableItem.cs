using UnityEngine;

[CreateAssetMenu]
public class SellableItem : ScriptableObject
{
    public string itemName;
    public Sprite itemSprite;
    public int goldCost;
    public int diamondCost;
    public Sprite specialOfferSprite;
    public string specialOfferText;
}

[CreateAssetMenu]
public class WeaponItem : SellableItem
{
    public int baseDamage;
}

[CreateAssetMenu]
public class ArrowItem : SellableItem
{
    public int baseInterval;
}