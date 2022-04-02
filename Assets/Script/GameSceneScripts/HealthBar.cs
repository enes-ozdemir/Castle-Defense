using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Image healthBar;
    [SerializeField] public Canvas healthCanvas;

    public void SetHealth(float amount)
    {
        healthBar.fillAmount /= amount;
    }

}