using System.Collections;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private PlayerMovement[] playersMovementTurn;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private BattleController battleController;
    [SerializeField] private int maxMovesPerTurn = 3;

    private int _currentPlayerIndex; // Can be 0 or 1
    private int _currentMoves;
    private bool _onBattle;

    private void Start()
    {
        _currentPlayerIndex = 0;
        _currentMoves = maxMovesPerTurn;
        _onBattle = false;

        playersMovementTurn[_currentPlayerIndex].enabled = true;

        UpdateCurrentPlayerText();
        UpdateMoveText();
    }

    private void Update()
    {
        if (GameController.Instance.isGameOver || _onBattle) return;

        if (_currentMoves <= 0)
        {
            EndTurn();
        }
    }

    public void MovePlayer()
    {
        if (_currentMoves > 0)
        {
            _currentMoves--;

            UpdateMoveText();

            CheckForBattle();
        }
    }

    private void CheckForBattle()
    {
        PlayerMovement currentPlayer = playersMovementTurn[_currentPlayerIndex];
        PlayerMovement opponentPlayer = playersMovementTurn[(_currentPlayerIndex + 1) % 2];

        if (AreAdjacent(currentPlayer.transform.position, opponentPlayer.transform.position))
        {
            battleController.StartBattle(currentPlayer, opponentPlayer);

            StartCoroutine(OnBattleDelay(currentPlayer));
        }
    }

    private bool AreAdjacent(Vector3 pos1, Vector3 pos2)
    {
        return Vector3.Distance(pos1, pos2) < 2;
    }

    private void EndTurn()
    {
        _currentMoves = maxMovesPerTurn;

        playersMovementTurn[_currentPlayerIndex].enabled = false;

        // Updating current player
        _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;

        SetNextPlayer();
    }

    private void SetNextPlayer()
    {
        StartCoroutine(SetNextPlayerDelay());
    }

    private IEnumerator SetNextPlayerDelay()
    {
        yield return new WaitForSeconds(2f);

        UpdateCurrentPlayerText();
        UpdateMoveText();
        ShowChangingPlayerFeedback(true);

        cameraController.SwitchTarget();

        yield return new WaitForSeconds(5f);

        // Enable next currentPlayer movement after prepare delay
        playersMovementTurn[_currentPlayerIndex].enabled = true;

        ShowChangingPlayerFeedback(false);
    }

    private IEnumerator OnBattleDelay(PlayerMovement currentPlayerMovement)
    {
        _onBattle = true;
        currentPlayerMovement.enabled = false;

        UIController.Instance.ShowBattleFeedbackText(true);

        yield return new WaitForSeconds(7f);

        UIController.Instance.ShowBattleFeedbackText(false);
        UIController.Instance.ShowBattleStatsPanel(false);

        currentPlayerMovement.enabled = true;
        _onBattle = false;
    }

    private void UpdateCurrentPlayerText()
    {
        UIController.Instance.UpdateCurrentPlayer(_currentPlayerIndex + 1);
    }

    private void UpdateMoveText()
    {
        UIController.Instance.UpdateRemainingMoves(_currentMoves);
    }

    private void ShowChangingPlayerFeedback(bool show)
    {
        UIController.Instance.ShowChangingPlayerText(show);
    }
}