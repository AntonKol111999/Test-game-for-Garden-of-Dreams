using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{ 
    public GameObject enemyPrefab; // ������ �����
    public int numberOfEnemies = 3; // ���������� ������
    public Vector2 arenaSize = new Vector2(10, 20); // ������ �����

   
    void Start()
    {
        SpawnEnemies();

        
    }

   

    void SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Vector2 randomPosition = new Vector2(
                Random.Range(-arenaSize.x / 2, arenaSize.x / 2),
                Random.Range(-arenaSize.y / 2, arenaSize.y / 2)
            );

            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
    }

    

  
}
