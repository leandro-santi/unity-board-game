using UnityEngine;

public class BoardModifier : MonoBehaviour
{
    [SerializeField] private GameObject[] additionalPrefabs;
    [SerializeField] private float probability;
    [SerializeField] private int rows = 16; // Quantidade de linhas do tabuleiro
    [SerializeField] private int columns = 16; // Quantidade de colunas do tabuleiro
    [SerializeField] private float tileSize = 1f; // Tamanho de cada tile

    private void Start()
    {
        ModifyBoard();
    }

    void ModifyBoard()
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
                    if (Random.value < probability)
                    {
                        GameObject selectedPrefab = GetRandomPrefab();
                        GameObject newObject = Instantiate(selectedPrefab, obj.transform.position, Quaternion.identity,
                            obj.transform);

                        if (newObject.GetComponent<Collider>() == null)
                        {
                            newObject.AddComponent<BoxCollider>();
                        }

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