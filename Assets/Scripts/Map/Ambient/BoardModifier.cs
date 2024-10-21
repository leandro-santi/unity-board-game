using UnityEngine;

public class BoardModifier : MonoBehaviour
{
    [SerializeField] private GameObject[] additionalPrefabs;
    [SerializeField] private float probability;
    [SerializeField] private int rows = 16;
    [SerializeField] private int columns = 16;
    [SerializeField] private float tileSize = 1f;

    private void Start()
    {
        // This functions aim to be a dynamic board creator
        // Dynamic cause the additional prefabs does not allow movement to the tile and block the way
        ModifyBoard();
    }

    void ModifyBoard()
    {
        int tileLayer = LayerMask.NameToLayer("Tile");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Not able to create additional prefabs in the limits of the map
        float boardOffsetZ = -(rows / 2f) * tileSize + tileSize / 2f;

        foreach (GameObject obj in allObjects)
        {
            if (obj.layer == tileLayer)
            {
                int row = Mathf.RoundToInt((obj.transform.position.z - boardOffsetZ) / tileSize);

                if (row > 0 && row < rows - 1)
                {
                    if (Random.value < probability)
                    {
                        GameObject selectedPrefab = GetRandomPrefab();
                        GameObject newObject = Instantiate(selectedPrefab, obj.transform.position, Quaternion.identity,
                            obj.transform);

                        if (newObject.GetComponent<Collider>() == null)
                        {
                            newObject.AddComponent<BoxCollider>();
                        }

                        // Changing layer of the map to disable movement capacity to there
                        obj.layer = LayerMask.NameToLayer("Default");
                    }
                }
            }
        }
    }

    GameObject GetRandomPrefab()
    {
        int randomIndex = Random.Range(0, additionalPrefabs.Length);
        return additionalPrefabs[randomIndex];
    }
}