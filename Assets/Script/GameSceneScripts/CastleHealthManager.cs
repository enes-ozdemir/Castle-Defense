using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthManager : MonoBehaviour
{
    [SerializeField] private Image castleHealth;
    [SerializeField] private int currentHealth;
    
    [SerializeField] private TextMeshProUGUI healthText;
    
    private int maxHealth;


    // Start is called before the first frame update
    void Start()
    {
        maxHealth = GameManager.Castle.castleHealth;
        currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;
    }

    public void GetHit(int enemyDamage)
    {
        currentHealth -= enemyDamage;
        castleHealth.fillAmount /= (float) maxHealth / currentHealth;
        healthText.text = currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }
    }
}