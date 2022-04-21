using UnityEngine;

namespace Script.GameManagerScripts
{
    public class Arrow : MonoBehaviour
    {
        public static int arrowLevel = 1;
        public static Sprite arrowSprite;
        public StarterKit starterKit;

        private void Awake()
        {
            if (arrowSprite == null) arrowSprite = starterKit.arrowSprite;
        }
    }
}