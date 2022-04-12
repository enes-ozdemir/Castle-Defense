using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image castleHealth;

    private int maxHealth;
    [HideInInspector] public int currentHealth;

    void Start()
    {
        maxHealth = GameManager.castle.castleHealth;
        currentHealth = maxHealth;
        SetCastleHealthUI();
    }

    public void CastleGotHit(int enemyDamage)
    {
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
            GameController.Instance.UpdateGameState(GameController.State.Lose);
            Debug.Log("Game Over");
        }
    }
}