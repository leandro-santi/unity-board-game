using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] hexTilePrefabs;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float hexRadius;

    void Start()
    {
        GenerateHexBoard();
    }

    void GenerateHexBoard()
    {
        float hexWidth = hexRadius * 2f;
        float hexHeight = Mathf.Sqrt(3f) * hexRadius;

        float boardWidth = columns * hexWidth * 0.75f;
        float boardHeight = rows * hexHeight;

        float offsetX = -boardWidth / 2f + (hexWidth * 0.75f) / 2f;
        float offsetZ = -boardHeight / 2f + hexHeight / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                float xOffset = col * hexWidth * 0.75f;
                float zOffset = row * hexHeight;

                if (col % 2 == 1)
                {
                    zOffset += hexHeight / 2f;
                }

                Vector3 position = new Vector3(xOffset + offsetX, 0, zOffset + offsetZ);

                int regionIndex = GetRegionIndex(row, rows);
                GameObject selectedPrefab = hexTilePrefabs[regionIndex];

                Instantiate(selectedPrefab, position, Quaternion.identity, transform);
            }
        }
    }

    int GetRegionIndex(int row, int totalRows)
    {
        int regionSize = totalRows / 3;

        if (row < regionSize)
        {
            return 0;
        }
        else if (row < 2 * regionSize)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}