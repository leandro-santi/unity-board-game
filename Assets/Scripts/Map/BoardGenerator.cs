using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardGenerator : MonoBehaviour
{
    public GameObject hexTilePrefab; // Prefab do tile hexagonal
    public int rows = 16; // Número de linhas
    public int columns = 16; // Número de colunas
    public float hexRadius = 1f; // Raio do hexágono

    void Start()
    {
        GenerateHexBoard();
    }

    void GenerateHexBoard()
    {
        float hexWidth = hexRadius * 2f;
        float hexHeight = Mathf.Sqrt(3f) * hexRadius; // Altura do hexágono

        // Calcular a posição inicial para centralizar o tabuleiro
        float boardWidth = columns * hexWidth * 0.75f;
        float boardHeight = rows * hexHeight;

        float offsetX = -boardWidth / 2f + (hexWidth * 0.75f) / 2f;
        float offsetZ = -boardHeight / 2f + hexHeight / 2f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calcular a posição do tile com base no deslocamento
                float xOffset = col * hexWidth * 0.75f; // 75% do diâmetro horizontal para o deslocamento
                float zOffset = row * hexHeight;

                // Deslocar as linhas ímpares para criar o padrão
                if (col % 2 == 1)
                {
                    zOffset += hexHeight / 2f;
                }

                // Adicionar os offsets para centralizar o tabuleiro
                Vector3 position = new Vector3(xOffset + offsetX, 0, zOffset + offsetZ);
                Instantiate(hexTilePrefab, position, Quaternion.identity, transform);
            }
        }
    }
}