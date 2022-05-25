using UnityEngine;

namespace Script.GameManagerScripts
{
    public class Weapon : MonoBehaviour
    {
        public static int weaponLevel = 1;
        public static int weaponIndex = 0;
        public static Sprite weaponSprite;
        public StarterKit starterKit;

        private void Awake()
        {
            if (weaponSprite == null) weaponSprite = starterKit.weaponSprite;
        }
    }
}