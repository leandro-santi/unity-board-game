using System.Collections;
using UnityEngine;

public class TurnController : MonoBehaviour
{
    [SerializeField] private PlayerMovement[] players;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private int maxMovesPerTurn = 3;
    private int _currentPlayerIndex = 0;
    private int _currentMoves = 0;

    private void Start()
    {
        players[_currentPlayerIndex].enabled = true;
    }

    void Update()
    {
        if (_currentMoves >= maxMovesPerTurn)
        {
            EndTurn();
        }
    }

    public void MovePlayer()
    {
        _currentMoves++;

        // Debug.Log("Current: " + _currentMoves);

        if (_currentMoves >= maxMovesPerTurn)
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        _currentMoves = 0;

        players[_currentPlayerIndex].enabled = false;

        _currentPlayerIndex = (_currentPlayerIndex + 1) % players.Length;

        SetNextPlayer();
    }

    private void SetNextPlayer()
    {
        Debug.Log("SetCurrentPlayer: " + _currentPlayerIndex);

        players[_currentPlayerIndex].enabled = true;

        StartCoroutine(SetNextPlayerCoroutine());
    }

    private IEnumerator SetNextPlayerCoroutine()
    {
        yield return new WaitForSeconds(2f);

        cameraController.SwitchTarget();
    }
}