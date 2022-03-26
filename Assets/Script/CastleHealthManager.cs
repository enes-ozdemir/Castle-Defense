using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CastleHealthManager : MonoBehaviour
{

    [SerializeField] private Image castleHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private int currentHealth;
    [SerializeField] private TextMeshProUGUI healthText;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthText.text = currentHealth + " / " + maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCastleLevel(int level)
    {
        maxHealth *=level;
        currentHealth = maxHealth;
    }

    public void Hit(int enemyDamage)
    {
        Debug.Log("Hit");
        currentHealth -= enemyDamage;
        castleHealth.fillAmount /= (float) maxHealth / currentHealth;
        healthText.text = currentHealth + " / " + maxHealth;

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over");
        }

    }
    
}
