using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

	[Serializable]
	public class Count {
		public int minimum;
		public int maximum;

		public Count(int min, int max) {
			minimum = min;
			maximum = max;
		}
	}

	public int columns = 8;
	public int rows = 8;
	public Count wallCount = new Count(5,9);
	public Count foodCount = new Count(5,9);
	public GameObject exit;
	public GameObject[] floorTiles;
	public GameObject[] wallTiles;
	public GameObject[] outerWallTiles;
	public GameObject[] enemyTiles;
	public GameObject[] foodTiles;

	private Transform boardHolder;
	private List<Vector3> gridPosition = new List<Vector3>();

	void initialiseList(){
		gridPosition.Clear();

		for (int x = 1; x < columns - 1; x++)
        {
			for (int y = 1; y < rows - 1; y++)
            {
				gridPosition.Add (new Vector3 (x, y, 0.0f));
			}
		}
	}

	void boardSetup() {
		boardHolder = new GameObject("board").transform;

        for (int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = floorTiles[Random.Range(0, floorTiles.Length)];
                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = outerWallTiles[Random.Range(0, outerWallTiles.Length)];

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0.0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 randomPosition()
    {
        int randomIndex = Random.Range(0, gridPosition.Count);
        Vector3 randomPosition = gridPosition[randomIndex];
        gridPosition.RemoveAt(randomIndex);
        return randomPosition;
    }
    
    void layoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
    {
        int objectCount = Random.Range(minimum, maximum + 1);

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 position = randomPosition();
            GameObject tileChoice = tileArray[Random.RandomRange(0, tileArray.Length)];
            Instantiate(tileChoice, position, Quaternion.identity);
        }
    }

    public void setupScene(int level)
    {
        boardSetup();
        initialiseList();
        layoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
        layoutObjectAtRandom(foodTiles, foodCount.minimum, foodCount.maximum);

        int enemyCount = (int)Mathf.Log(level, 2f);
        layoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0.0f), Quaternion.identity);
    }
}
