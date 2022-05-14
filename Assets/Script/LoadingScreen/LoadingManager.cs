using Script.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tipsText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Sprite[] loadingBackgrounds;


    private void Start()
    {
        int rand = Random.Range(1, Constant.Tips.Length);
        tipsText.text = Constant.Tips[rand];
        
        int random = Random.Range(1, loadingBackgrounds.Length);
        backgroundImage.sprite = loadingBackgrounds[random];

    }
}