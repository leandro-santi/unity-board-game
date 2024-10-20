using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private DiceController diceController;
    [SerializeField] private DiceSpawnerPositionHandler diceSpawnerPositionHandler;

    public void StartBattle(PlayerMovement currentPlayer)
    {
        Debug.Log("Battle!");

        diceSpawnerPositionHandler.SetDiceSpawnPositionBeforeBattle(currentPlayer.transform);

        GetComponent<DiceController>().RollDice();

        StartCoroutine(BattleResults());
    }

    private IEnumerator BattleResults()
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
            Debug.Log("Player 1 Wins the Battle!");
        }
        else if (player2Score > player1Score)
        {
            Debug.Log("Player 2 Wins the Battle!");
        }
        else
        {
            Debug.Log("It's a Tie!");
        }
    }
}