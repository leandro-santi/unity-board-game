using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private GameObject[] dicesPlayer1;
    [SerializeField] private GameObject[] dicesPlayer2;
    [SerializeField] private Transform diceSpawnPosition;
    [SerializeField] private float rollForce;
    [SerializeField] private float diceRollWaitTime;

    private GameObject[] _allDices;
    private List<int> _player1Results;
    private List<int> _player2Results;

    public void RollDice()
    {
        _allDices = new GameObject[6];
        _player1Results = new List<int>();
        _player2Results = new List<int>();

        ActivateDice(dicesPlayer1, 0);
        ActivateDice(dicesPlayer2, 3);

        StartCoroutine(RollAllDice());
    }

    private void ActivateDice(GameObject[] diceArray, int startIndex)
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject dice = diceArray[i];
            Vector3 offset = new Vector3(i * 1.5f, 0, 0);
            dice.transform.position = diceSpawnPosition.position + offset;
            dice.transform.rotation = Random.rotation;
            dice.SetActive(true);
            _allDices[startIndex + i] = dice;
        }
    }

    private IEnumerator RollAllDice()
    {
        foreach (var dice in _allDices)
        {
            RollSingleDice(dice);
        }

        yield return new WaitForSeconds(diceRollWaitTime);

        GetDiceResults();

        yield return new WaitForSeconds(diceRollWaitTime);

        DeactivateAllDice();
    }

    private void RollSingleDice(GameObject dice)
    {
        Rigidbody rb = dice.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * rollForce + Random.insideUnitSphere * rollForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * rollForce, ForceMode.Impulse);
    }

    private void DeactivateAllDice()
    {
        foreach (var dice in _allDices)
        {
            dice.SetActive(false);
        }
    }

    private void GetDiceResults()
    {
        for (int i = 0; i < _allDices.Length; i++)
        {
            DiceFaceReader faceReader = _allDices[i].GetComponent<DiceFaceReader>();
            int topValue = faceReader.GetTopFaceValue();

            if (i < 3)
            {
                _player1Results.Add(topValue);
            }
            else
            {
                _player2Results.Add(topValue);
            }
        }
    }

    public List<int> ReturnDiceResults(int playerIndex)
    {
        return playerIndex == 0 ? _player1Results : _player2Results;
    }
}