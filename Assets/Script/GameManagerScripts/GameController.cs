using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject defeatCanvas;
    [SerializeField] private GameObject prizeCanvas;

    [SerializeField] private EarningsPanel earningsPanel;

    #region "Singleton"

    public static GameController Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

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
                StartCoroutine(PlayerWon());
                if (GameManager.currentLevel == GameManager.selectedLevel) GameManager.currentLevel++;
                break;
            case State.Lose:
                StartCoroutine(PlayerLost());
                break;
        }

        //todo Pause the game with this event
    }

    private void Start()
    {
        UpdateGameState(State.Play);
    }

    private IEnumerator PlayerLost()
    {
        SoundManager.PlaySound(SoundManager.Sound.Loss);
        Cursor.visible = true;
        PauseTheGame();
        yield return new WaitForSeconds(0.2f);
        defeatCanvas.SetActive(true);
    }

    private IEnumerator PlayerWon()
    {
        SoundManager.PlaySound(SoundManager.Sound.Win);
        Cursor.visible = true;
        PauseTheGame();
        yield return new WaitForSeconds(0.2f);
        victoryCanvas.SetActive(true);
    }

    private void PauseTheGame()
    {
        prizeCanvas.SetActive(true);
        BaseEnemyManager.isBaseAttackAllowed = false;
        BaseEnemyManager.isBaseMovementAllowed = false;
    }

    private void Update()
    {
        if (earningsPanel.isActiveAndEnabled)
        {
            earningsPanel.UpdatePanel();
        }
    }
}