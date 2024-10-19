using UnityEngine;

public class DiceController : MonoBehaviour
{
    public int Roll()
    {
        return Random.Range(1, 7);
    }
}