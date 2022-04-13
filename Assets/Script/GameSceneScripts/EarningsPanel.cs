using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarningsPanel : MonoBehaviour
{

    [SerializeField] private Image mainImage;
    
    [SerializeField] private Image goldImage;
    [SerializeField] private Image diamondImage;

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI diamondText;

    [SerializeField] private GameUIManager gameUIManager;

    public void UpdatePanel()
    {
        moneyText.text = gameUIManager.tempMoney.ToString();
        diamondText.text = gameUIManager.tempDiamond.ToString();
    }

}
