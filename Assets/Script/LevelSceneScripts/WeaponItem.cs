using UnityEngine;

namespace Script.LevelSceneScripts
{
    [CreateAssetMenu]
    public class WeaponItem : SellableItem
    {
        public int baseDamage;
        public int itemIndex = 0;
    }
}