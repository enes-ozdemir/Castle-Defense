using Script.GameManagerScripts;
using UnityEngine;

namespace Script.GameSceneScripts
{
    public class WeaponManager : MonoBehaviour
    {
        public GameObject arrowPrefab;
        private SpriteRenderer bowSprite;
        private SpriteRenderer arrowSprite;

        [SerializeField] private Sprite[] bowSprites;
        [SerializeField] private Sprite[] arrowSprites;

        private void Awake()
        {
            bowSprite = GetComponent<SpriteRenderer>();
            arrowSprite = arrowPrefab.GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetWeaponSprite();
        }

        private void SetWeaponSprite()
        {
            bowSprite.sprite = Weapon.weaponSprite;
            arrowSprite.sprite = Arrow.arrowSprite;
        }
    }
}