using System.Collections;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private PlayerMovement[] players;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private BattleController battleController;
    [SerializeField] private int maxMovesPerTurn = 3;

    private int _currentPlayerIndex = 0; // Can be 0 or 1
    private int _currentMoves = 0;
    private bool _onBattle = false;

    private void Start()
    {
        players[_currentPlayerIndex].enabled = true;
    }

    void Update()
    {
        if (_onBattle) return;

        if (_currentMoves >= maxMovesPerTurn)
        {
            EndTurn();
        }
    }

    public void MovePlayer()
    {
        _currentMoves++;

        // Debug.Log("Current: " + _currentMoves);

        CheckForBattle();
    }

    private void CheckForBattle()
    {
        PlayerMovement currentPlayer = players[_currentPlayerIndex];
        PlayerMovement opponentPlayer = players[(_currentPlayerIndex + 1) % 2];

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
        _currentMoves = 0;

        players[_currentPlayerIndex].enabled = false;

        _currentPlayerIndex = (_currentPlayerIndex + 1) % 2;

        SetNextPlayer();
    }

    private void SetNextPlayer()
    {
        Debug.Log("SetCurrentPlayer: " + _currentPlayerIndex);

        players[_currentPlayerIndex].enabled = true;

        StartCoroutine(SetNextPlayerDelay());
    }

    private IEnumerator SetNextPlayerDelay()
    {
        yield return new WaitForSeconds(2f);

        cameraController.SwitchTarget();
    }

    private IEnumerator OnBattleDelay(PlayerMovement player)
    {
        _onBattle = true;
        player.enabled = false;

        yield return new WaitForSeconds(2f);

        player.enabled = true;
        _onBattle = false;
    }
}