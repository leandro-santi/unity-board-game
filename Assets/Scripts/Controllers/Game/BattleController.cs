using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    [SerializeField] private DiceController diceController;

    public void StartBattle(PlayerMovement player1, PlayerMovement player2)
    {
        Debug.Log("Battle!");

        List<int> player1Rolls = new List<int>();
        List<int> player2Rolls = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            player1Rolls.Add(diceController.Roll());
            player2Rolls.Add(diceController.Roll());
        }

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