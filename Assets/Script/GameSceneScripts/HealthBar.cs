using UnityEngine;
using UnityEngine.UI;

namespace Script.GameSceneScripts
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] public Image healthBar;
        [SerializeField] public Canvas healthCanvas;

        public void SetHealth(float amount)
        {
            healthBar.fillAmount = amount;
        }
    }
}