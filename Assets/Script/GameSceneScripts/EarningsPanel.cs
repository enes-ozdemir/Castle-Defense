using TMPro;
using UnityEngine;
public class EarningsPanel : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI diamondText;

    [SerializeField] private GameUIManager gameUIManager;

    public void UpdatePanel()
    {
        moneyText.text = gameUIManager.tempMoney +" "+ Constant.SpriteIndex;
        diamondText.text = gameUIManager.tempDiamond +" "+ Constant.SpriteIndex;
    }
}