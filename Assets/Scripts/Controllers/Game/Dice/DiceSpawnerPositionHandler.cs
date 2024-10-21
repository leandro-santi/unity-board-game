using UnityEngine;

public class DiceSpawnerPositionHandler : MonoBehaviour
{
    [SerializeField] private float spawnHeightOffset;

    // Setting the dice spawn point to above the current player of the turn
    public void SetDiceSpawnPositionBeforeBattle(Transform attackerTransform)
    {
        Vector3 newPosition = attackerTransform.position;
        newPosition.y += spawnHeightOffset;
        transform.position = newPosition;
    }
}