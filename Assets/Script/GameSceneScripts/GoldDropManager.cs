using DG.Tweening;
using Script.GameManagerScripts;
using Script.Utils;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.GameSceneScripts
{
    public class GoldDropManager : MonoBehaviour
    {
        public GameObject goldDrop;
        public GameObject diamondDrop;
        public Transform goldTarget;


        [SerializeField] private GameUIManager gameUIManager;

        #region "Singleton"

        public static GoldDropManager Instance;

        private void Awake()
        {
            Instance = this;
        }

        #endregion

        public void CheckLoot(int goldAmount, Vector3 position)
        {
            Debug.Log("Checking Gold");
            CheckGoldDrop(goldAmount, position);
            CheckDiamondDrop(position);
        }

        private void CheckDiamondDrop(Vector3 position)
        {
            var diamondDropChange = Random.Range(0, 10);
            if (diamondDropChange >= 8)
            {
                SoundManager.PlaySound(SoundManager.Sound.DiamondDrop);
                Debug.Log("Diamond earned");
                var diamond = Instantiate(diamondDrop, position + new Vector3(1, 0, 0), Quaternion.identity);
                diamond.transform.DOMove(goldTarget.position, 1f)
                    .SetEase(Ease.InOutBack)
                    .OnComplete((() => EarnDiamond(1)));
            }
        }

        private void CheckGoldDrop(int goldAmount, Vector3 position)
        {
            var goldDropChange = Random.Range(0, 5);
            if (goldDropChange >= 3)
            {
                SoundManager.PlaySound(SoundManager.Sound.GoldDrop);
                Debug.Log("Gold earned " + goldAmount);
                var coin = Instantiate(goldDrop, position, Quaternion.identity);
                int earnedGoldAmount = (int) (goldAmount * (Random.Range(0.9f, 1.2f)));
                coin.transform.DOMove(goldTarget.position, 1f)
                    .SetEase(Ease.InOutBack)
                    .OnComplete((() => EarnMoney(earnedGoldAmount)));
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
}