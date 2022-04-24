using Script.GameManagerScripts;
using UnityEngine;

namespace Script.GameSceneScripts
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private Sprite[] backgroundSprites;
        private SpriteRenderer spriteRenderer;


        private void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            var selectedLevel = GameManager.selectedLevel;
            switch (selectedLevel)
            {
                case < 4:
                    spriteRenderer.sprite = backgroundSprites[0];
                    break;
                case > 4 and <= 8:
                    spriteRenderer.sprite = backgroundSprites[1];
                    break;
                case > 8:
                    spriteRenderer.sprite = backgroundSprites[2];
                    break;
                default:
                    spriteRenderer.sprite = backgroundSprites[0];
                    break;
            }
        }
    }
}