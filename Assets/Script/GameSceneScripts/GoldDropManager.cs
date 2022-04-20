using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GoldDropManager : MonoBehaviour
{
    public GameObject goldDrop;
    public GameObject diamondDrop;
    public Transform goldTarget;

    public static GoldDropManager Instance;

    [SerializeField] private GameUIManager gameUIManager;

    #region "Singleton"

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public void CheckLoot(int goldAmount, Vector3 position)
    {
        Debug.Log("Checking Gold");
        CheckGoldDrop(goldAmount, position);
        CheckDiamondDrop(goldAmount, position);
    }

    private void CheckDiamondDrop(int goldAmount, Vector3 position)
    {
        var diamondDropChange = Random.Range(0, 10);
        if (diamondDropChange >= 8)
        {
            Debug.Log("Daimond earned " + goldAmount);
            var coin = Instantiate(diamondDrop, position, Quaternion.identity);
            coin.transform.DOMove(goldTarget.position, 1f)
                .SetEase(Ease.InOutBack)
                .OnComplete((() => EarnDiamond(1)));
            //Add this to end of the animation later
        }
    }

    private void CheckGoldDrop(int goldAmount, Vector3 position)
    {
        var goldDropChange = Random.Range(0, 5);
        if (goldDropChange >= 3)
        {
            Debug.Log("Gold earned " + goldAmount);
            var coin = Instantiate(goldDrop, position, Quaternion.identity);
            int earnedGoldAmount = (int) (goldAmount * (Random.Range(0.9f, 1.2f)));
            coin.transform.DOMove(goldTarget.position, 1f)
                .SetEase(Ease.InOutBack)
                .OnComplete((() => EarnMoney(earnedGoldAmount)));
            //Add this to end of the animation later
        }
    }

    private void EarnMoney(int goldAmount)
    {
        GameManager.money += goldAmount;
        gameUIManager.tempMoney += goldAmount;
    }

    private void EarnDiamond(int diamondAmount)
    {
        GameManager.diamond += diamondAmount;
        gameUIManager.tempDiamond += diamondAmount;
    }
}