using UnityEngine;

public class CollectibleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] collectiblePrefabs;
    [SerializeField] private int rows = 16;
    [SerializeField] private int columns = 16;
    [SerializeField] private float tileSize = 1f;

    private void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        int tileLayer = LayerMask.NameToLayer("Tile");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        float boardOffsetX = -(columns / 2f) * tileSize + tileSize / 2f;
        float boardOffsetZ = -(rows / 2f) * tileSize + tileSize / 2f;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == tileLayer)
            {
                int row = Mathf.RoundToInt((obj.transform.position.z - boardOffsetZ) / tileSize);
                int column = Mathf.RoundToInt((obj.transform.position.x - boardOffsetX) / tileSize);

                if (row > 0 && row < rows - 1)
                {
                    GameObject selectedPrefab = GetRandomCollectiblePrefab();

                    Vector3 spawnPosition = obj.transform.position + new Vector3(0, 0.75f, 0);

                    Instantiate(selectedPrefab, spawnPosition, Quaternion.identity,
                        obj.transform);
                }
            }
        }
    }

    private GameObject GetRandomCollectiblePrefab()
    {
        int randomIndex = Random.Range(0, collectiblePrefabs.Length);
        return collectiblePrefabs[randomIndex];
    }
}