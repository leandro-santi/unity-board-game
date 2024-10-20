using System.Collections;
using UnityEngine;

public class DiceController : MonoBehaviour
{
    [SerializeField] private GameObject diceType1Prefab;
    [SerializeField] private GameObject diceType2Prefab;
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private float rollForce = 10f;
    [SerializeField] private float waitTime = 3f;
    
    private GameObject[] allDice;
    
    public void RollDice()
    {
        allDice = new GameObject[6];
        SpawnDice(diceType1Prefab, 0);
        SpawnDice(diceType2Prefab, 3);
        
        StartCoroutine(RollAllDice());
    }

    private void SpawnDice(GameObject dicePrefab, int startIndex)
    {
        for (int i = 0; i < 3; i++)
        {
            Vector3 offset = new Vector3(i * 1.5f, 0, 0);
            allDice[startIndex + i] = Instantiate(dicePrefab, spawnPosition.position + offset, Random.rotation);
        }
    }

    private IEnumerator RollAllDice()
    {
        foreach (var dice in allDice)
        {
            RollSingleDie(dice);
        }
        
        yield return new WaitForSeconds(waitTime);
        
        GetDiceResults();
    }

    private void RollSingleDie(GameObject die)
    {
        Rigidbody rb = die.GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * rollForce + Random.insideUnitSphere * rollForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * rollForce, ForceMode.Impulse);
    }

    private void GetDiceResults()
    {
        foreach (var die in allDice)
        {
            // DiceFaceReader faceReader = die.GetComponent<DiceFaceReader>();
            // int topValue = faceReader.GetTopFaceValue();
            // Debug.Log($"Dado {die.name} parou com a face {topValue} para cima.");
        }
    }
    
    public int Roll()
    {
        return Random.Range(1, 7);
    }
}