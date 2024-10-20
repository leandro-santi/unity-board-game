using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI player1HpText;
    [SerializeField] private TextMeshProUGUI player2HpText;
    [SerializeField] private TextMeshProUGUI player1PowerText;
    [SerializeField] private TextMeshProUGUI player2PowerText;
    [SerializeField] private TextMeshProUGUI currentPlayerText;
    [SerializeField] private TextMeshProUGUI remainingMovesText;
    [SerializeField] private TextMeshProUGUI changingPlayerText;
    [SerializeField] private TextMeshProUGUI battleFeedbackText;
    [SerializeField] private TextMeshProUGUI winnerBattleText;
    [SerializeField] private TextMeshProUGUI diceBattleText;
    [SerializeField] private TextMeshProUGUI winnerText;
    [SerializeField] private GameObject winnerPanel;
    [SerializeField] private GameObject battleStatsPanel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdatePlayerHp(int playerIndex, int hp)
    {
        if (playerIndex == 1)
        {
            player1HpText.text = $"HP: {hp}";
        }
        else if (playerIndex == 2)
        {
            player2HpText.text = $"HP: {hp}";
        }
    }

    public void UpdatePlayerPower(int playerIndex, int power)
    {
        if (playerIndex == 1)
        {
            player1PowerText.text = $"POWER: {power}";
        }
        else if (playerIndex == 2)
        {
            player2PowerText.text = $"POWER: {power}";
        }
    }

    public void UpdateCurrentPlayer(int playerIndex)
    {
        currentPlayerText.text = $"TURN: Player {playerIndex}";
    }

    public void UpdateRemainingMoves(int moves)
    {
        remainingMovesText.text = $"Moves: {moves}";
    }

    public void ShowChangingPlayerText(bool show)
    {
        changingPlayerText.gameObject.SetActive(show);
    }

    public void ShowBattleFeedbackText(bool show)
    {
        battleFeedbackText.gameObject.SetActive(show);
    }

    public void EndGame(int playerIndex)
    {
        winnerText.text = $"Player {playerIndex} Wins!";
        winnerPanel.SetActive(true);
    }

    public void UpdateBattleStats(List<int> player1Rolls, List<int> player2Rolls, string winnerBattle)
    {
        string player1RollsText = string.Join(", ", player1Rolls);
        string player2RollsText = string.Join(", ", player2Rolls);

        diceBattleText.text = $"Player 1: [{player1RollsText}] x Player 2: [{player2RollsText}]";

        winnerBattleText.text = $"{winnerBattle} Wins the Battle!";
    }

    public void ShowBattleStatsPanel(bool show)
    {
        battleStatsPanel.SetActive(show);
    }
}