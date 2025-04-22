using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [Header("Buildings Prefabs")]
    public List<GameObject> buildings = new List<GameObject>();

    [Header("Road Prefabs")]
    public GameObject roadPrefab;
    public GameObject turnPrefab;
    public GameObject intersectionPrefab;
    public GameObject crossRoadPrefab;

    [Header("Prefab Settings")]
    public float prefabWidth;
    public float prefabHeight;

    [Range(10, 61)]
    public int rows;
    [Range(10, 61)]
    public int cols;
    private int[,] matrix;

    void Awake()
    {
        // rows = Random.Range(30, 61);
        // cols = Random.Range(30, 61);

        GenerateRoads();
    }

    public void GenerateRoads()
    {
        matrix = new int[rows, cols];
        InitializeMatrix(matrix);
        GenerateBorder(matrix);
        for (int i = 4; i < rows - 2; i += 3)
        {
            GenerateHorizontalRoads(i, 1, cols - 2, matrix);
        }

        for (int i = 0; i < cols - 2; i++)
        {
            if (matrix[rows - 3, i] == 1)
            {
                matrix[rows - 3, i] = 0;
            }
        }

        int currentRowIndex = 1;
        while (currentRowIndex < rows - 2)
        {
            if (matrix[currentRowIndex, 2] == 1)
            {
                ConnectRoads(currentRowIndex + 1, matrix);
            }

            currentRowIndex++;
        }

        for (int i = 1; i < rows - 2; i += 3)
        {
            ConnectRoads(i + 1, matrix);
        }

        GenerateIntersection(matrix);

        InstantitePrefabs(matrix);
    }

    // Initialize the matrix with 0's (empty space)
    private void InitializeMatrix(int[,] matrix)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                matrix[i, j] = 0;
            }
        }
    }

    // Display the matrix with colored output
    private void InstantitePrefabs(int[,] matrix)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Vector3 position = new Vector3(j * prefabWidth, 0, i * prefabHeight);
                switch (matrix[i, j])
                {
                    case 0:
                        Instantiate(RandomBuilding(), position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(roadPrefab, position, Quaternion.Euler(0, 90, 0));
                        break;
                    case 2:
                        Instantiate(roadPrefab, position, Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(turnPrefab, position, Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(turnPrefab, position, Quaternion.Euler(0, -90, 0));
                        break;
                    case 5:
                        Instantiate(turnPrefab, position, Quaternion.Euler(0, 180, 0));
                        break;
                    case 6:
                        Instantiate(turnPrefab, position, Quaternion.Euler(0, 90, 0));
                        break;
                    case 7:
                        Instantiate(crossRoadPrefab, position, Quaternion.identity);
                        break;
                    case 8:
                        Instantiate(crossRoadPrefab, position, Quaternion.Euler(0, 180, 0));
                        break;
                    case 9:
                        Instantiate(intersectionPrefab, position, Quaternion.identity);
                        break;
                    case 10:
                        Instantiate(crossRoadPrefab, position, Quaternion.Euler(0, 90, 0));
                        break;
                    case 11:
                        Instantiate(crossRoadPrefab, position, Quaternion.Euler(0, -90, 0));
                        break;
                }
            }

        }
    }

    GameObject RandomBuilding()
    {
        int buildingCount = buildings.Count;

        return buildings[RandomNumber(0, buildingCount - 1)];
    }

    private int RandomNumber(int minValue, int maxValue)
    {
        return Random.Range(minValue, maxValue);
    }

    private void GenerateBorder(int[,] matrix)
    {
        GenerateHorizontalRoads(1, 1, cols - 2, matrix);
        GenerateHorizontalRoads(rows - 2, 1, cols - 2, matrix);

        GenerateFixVerticalRoads(1, 1, rows - 2, matrix);
        GenerateFixVerticalRoads(cols - 2, 1, rows - 2, matrix);

        // Corners
        matrix[1, 1] = 3;
        matrix[1, cols - 2] = 4;
        matrix[rows - 2, cols - 2] = 5;
        matrix[rows - 2, 1] = 6;
    }

    private void GenerateHorizontalRoads(int rowIndex, int startColIndex, int length, int[,] matrix)
    {
        for (int i = 0; i < length; i++)
        {
            if (startColIndex + length < cols && matrix[rowIndex, startColIndex + i] == 0)
            {
                matrix[rowIndex, startColIndex + i] = 1;
            }
        }
    }

    private void ConnectRoads(int rowIndex, int[,] matrix)
    {
        int numOfRoads = RandomNumber(6, 10);
        for (int i = 0; i < numOfRoads; i++)
        {
            GenerateVerticalRoads(RandomNumber(3, cols - 4), rowIndex, matrix);
        }
    }

    private void GenerateVerticalRoads(int colIndex, int startRowIndex, int[,] matrix)
    {
        int i = 0;
        while (matrix[startRowIndex + i, colIndex] != 1)
        {
            // Check if there is a vertical road (2) on the left or right
            bool hasVerticalRoadLeft = colIndex - 1 >= 0 && matrix[startRowIndex + i, colIndex - 1] == 2;
            bool hasVerticalRoadRight = colIndex + 1 < cols && matrix[startRowIndex + i, colIndex + 1] == 2;

            // Only set the vertical road if there's no adjacent vertical road
            if (startRowIndex + i < rows && !hasVerticalRoadLeft && !hasVerticalRoadRight)
            {
                matrix[startRowIndex + i, colIndex] = 2;
            }

            i++;

            // Stop if we reach the end of the grid
            if (startRowIndex + i >= rows - 1)
                break;
        }
    }

    private void GenerateFixVerticalRoads(int colIndex, int startRowIndex, int length, int[,] matrix)
    {
        for (int i = 0; i < length; i++)
        {
            if (startRowIndex + length < rows && matrix[startRowIndex + i, colIndex] == 0)
            {
                matrix[startRowIndex + i, colIndex] = 2;
            }
        }
    }

    private void FindIntersection(int row, int col, int[,] matrix)
    {
        // Checking for intersection horizontal meets vertical downwards and upwards
        if (matrix[row, col] == 1)
        {
            if (matrix[row + 1, col] == 2)
            {
                matrix[row, col] = 7;
            }

            if (matrix[row - 1, col] == 2)
            {
                matrix[row, col] = 8;
            }

            if (matrix[row + 1, col] == 2 && matrix[row - 1, col] == 2)
            {
                matrix[row, col] = 9;
                // matrix[row + 1, col] = 12;
                // matrix[row - 1, col] = 13;
                // matrix[row, col - 1] = 14;
                // matrix[row, col + 1] = 15;
            }
        }
        else if (matrix[row, col] == 2)
        {
            if (matrix[row, col + 1] == 1)
            {
                matrix[row, col] = 10;
            }
            else if (matrix[row, col - 1] == 1)
            {
                matrix[row, col] = 11;
            }
        }
    }

    private void GenerateIntersection(int[,] matrix)
    {
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                FindIntersection(i, j, matrix);
            }
        }
    }
}