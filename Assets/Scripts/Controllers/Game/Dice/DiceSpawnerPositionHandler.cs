using UnityEngine;

public class DiceSpawnerPositionHandler : MonoBehaviour
{
    [SerializeField] private float spawnHeightOffset;

    public void SetDiceSpawnPositionBeforeBattle(Transform attackerTransform)
    {
        Vector3 newPosition = attackerTransform.position;
        newPosition.y += spawnHeightOffset;
        transform.position = newPosition;
    }
}