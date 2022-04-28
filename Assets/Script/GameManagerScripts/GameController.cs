using System.Collections;
using Script.GameSceneScripts;
using Script.Utils;
using UnityEngine;

namespace Script.GameManagerScripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject victoryCanvas;
        [SerializeField] private GameObject defeatCanvas;
        [SerializeField] private GameObject prizeCanvas;

        [SerializeField] private EarningsPanel earningsPanel;

        public enum State
        {
            Play,
            Win,
            Lose,
        }

        public void UpdateGameState(State gameState)
        {
            switch (gameState)
            {
                case State.Play:
                    break;
                case State.Win:
                    StartCoroutine(ShowWinScreen());
                    if (GameManager.currentLevel == GameManager.selectedLevel) GameManager.currentLevel++;
                    break;
                case State.Lose:
                    StartCoroutine(ShowLoseScreen());
                    break;
            }
        }

        private void Start()
        {
            UpdateGameState(State.Play);
        }

        private IEnumerator ShowLoseScreen()
        {
            if (!victoryCanvas.activeInHierarchy)
            {
                SoundManager.PlaySound(SoundManager.Sound.Loss);
                Cursor.visible = true;
                PauseTheGame();
                yield return new WaitForSeconds(1f);
                prizeCanvas.SetActive(true);
                defeatCanvas.SetActive(true);
            }
        }

        private IEnumerator ShowWinScreen()
        {
            if (!defeatCanvas.activeInHierarchy)
            {
                SoundManager.PlaySound(SoundManager.Sound.Win);
                Cursor.visible = true;
                PauseTheGame();
                yield return new WaitForSeconds(1f);
                prizeCanvas.SetActive(true);
                victoryCanvas.SetActive(true);
            }
        }

        private void PauseTheGame()
        {
            BaseEnemyManager.isBaseAttackAllowed = false;
            BaseEnemyManager.isBaseMovementAllowed = false;
        }

        private void Update()
        {
            SetEarningPanel();
        }

        private void SetEarningPanel()
        {
            if (earningsPanel.isActiveAndEnabled)
            {
                earningsPanel.UpdatePanel();
            }
        }
    }
}