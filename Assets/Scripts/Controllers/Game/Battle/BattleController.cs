using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private DiceController diceController;
    [SerializeField] private DiceSpawnerPositionHandler diceSpawnerPositionHandler;
    [SerializeField] private Player[] players;

    public void StartBattle(PlayerMovement currentPlayer, PlayerMovement opponentPlayer)
    {
        Debug.Log("Battle!");

        diceSpawnerPositionHandler.SetDiceSpawnPositionBeforeBattle(currentPlayer.transform);

        diceController.RollDice();

        StartCoroutine(BattleResults(currentPlayer, opponentPlayer));
    }

    private IEnumerator BattleResults(PlayerMovement currentPlayer, PlayerMovement opponentPlayer)
    {
        yield return new WaitForSeconds(5f);

        List<int> player1Rolls = diceController.ReturnDiceResults(0);
        List<int> player2Rolls = diceController.ReturnDiceResults(1);

        player1Rolls = player1Rolls.OrderByDescending(x => x).ToList();
        player2Rolls = player2Rolls.OrderByDescending(x => x).ToList();

        Debug.Log($"Player 1 Rolls: {string.Join(", ", player1Rolls)}");
        Debug.Log($"Player 2 Rolls: {string.Join(", ", player2Rolls)}");

        int player1Score = 0;
        int player2Score = 0;
        string winner = "";

        for (int i = 0; i < 3; i++)
        {
            if (player1Rolls[i] > player2Rolls[i])
            {
                player1Score++;
            }
            else if (player1Rolls[i] < player2Rolls[i])
            {
                player2Score++;
            }
        }

        if (player1Score > player2Score)
        {
            // Player 1 wins battle
            ApplyDamage(players[1], players[0].GetCurrentAttackPower()); // Player2 suffers damage

            winner = "Player 1";
        }
        else if (player2Score > player1Score)
        {
            // Player 2 wins battle
            ApplyDamage(players[0], players[1].GetCurrentAttackPower()); // Player1 suffers damage

            winner = "Player 2";
        }
        else
        {
            // Draw -> The current player of the turn wins battle
            ApplyDamage(opponentPlayer.GetComponent<Player>(), // Opponent player of the turn suffers damage
                currentPlayer.GetComponent<Player>().GetCurrentAttackPower());

            winner = "Player " + currentPlayer.GetComponent<Player>().GetPlayerIndex();
        }

        UIController.Instance.UpdateBattleStats(player1Rolls, player2Rolls, winner);
        UIController.Instance.ShowBattleStatsPanel(true);
    }

    private void ApplyDamage(Player target, int damage)
    {
        target.TakeDamage(damage);
    }
}