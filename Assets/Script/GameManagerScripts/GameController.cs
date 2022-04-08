using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject victoryCanvas;
    [SerializeField] private GameObject defeatCanvas;

    [SerializeField] private BaseEnemyManager enemyManager;

    private State currentState;

    public static event Action<State> OnGameStateChanged;

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
        currentState = gameState;
        switch (gameState)
        {
            case State.Play:
                break;
            case State.Win:
                PlayerWon();
                break;
            case State.Lose:
                PlayerLost();
                break;
        }

        OnGameStateChanged?.Invoke(gameState);
        //todo Pause the game with this event
    }

    private void Start()
    {
        UpdateGameState(State.Play);
    }

    private void PlayerLost()
    {
        defeatCanvas.SetActive(true);
        PauseTheGame();
    }

    private void PlayerWon()
    {
        victoryCanvas.SetActive(true);
        PauseTheGame();
    }

    private void PauseTheGame()
    {
        BaseEnemyManager.isBaseAttackAllowed = false;
        BaseEnemyManager.isBaseMovementAllowed = false;
        ArrowManager.canAttack = false;
    }
}