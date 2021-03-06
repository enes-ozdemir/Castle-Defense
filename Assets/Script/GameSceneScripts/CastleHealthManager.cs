using Script.GameManagerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.GameSceneScripts
{
    public class CastleHealthManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private Image castleHealth;
        [SerializeField] private GameController gameController;
        private int maxHealth;

        [HideInInspector] public int currentHealth;

        public Transform castleLocation;

        void Start()
        {
            SetCastleHealth();
            SetCastleHealthUI();
        }

        private void SetCastleHealth()
        {
            maxHealth = Castle.castleHealth;
            currentHealth = maxHealth;
        }

        public void CastleGotHit(int enemyDamage)
        {
            if (ArrowManager.canPlayerAttack == false) return;
                currentHealth -= enemyDamage;
            SetCastleHealthUI();
            CheckIfGameOver();
        }

        private void SetCastleHealthUI()
        {
            castleHealth.fillAmount = (float) currentHealth / maxHealth;
            healthText.text = currentHealth + " / " + maxHealth;
        }

        private void CheckIfGameOver()
        {
            if (currentHealth <= 0)
            {
                gameController.UpdateGameState(GameController.State.Lose);
                Debug.Log("Game Over");
            }
        }
    }
}