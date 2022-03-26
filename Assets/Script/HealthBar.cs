using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;

    public void SetHealth(float amount)
    {
        Debug.Log("Health changed to" + amount);
        healthBar.fillAmount /= amount;
    }

}