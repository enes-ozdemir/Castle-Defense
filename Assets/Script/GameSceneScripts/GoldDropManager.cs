using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class GoldDropManager : MonoBehaviour
{
    public GameObject goldDrop;
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
        var goldDropChange = Random.Range(0, 5);
        if (goldDropChange >= 3)
        {
            Debug.Log("Gold earned " + goldAmount);
            var coin = Instantiate(goldDrop, position, Quaternion.identity);
            coin.transform.DOMove(goldTarget.position, 1f)
                .SetEase(Ease.InOutBack)
                .OnComplete((() => EarnMoney(goldAmount)));


            //Add this to end of the animation later
        }
    }

    private void EarnMoney(int goldAmount)
    {
        GameManager.Money += goldAmount;
        gameUIManager.tempMoney += goldAmount;

    }
}