using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   // public GameObject enemyManager;
    public GameObject lightFishPrefab;
    public GameObject[] lightFishes = new GameObject[20];

    public GameObject octopusEnemyPrefab;
    public GameObject[] octopusEnemies = new GameObject[6];

    public float startX = -7.5f;

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            lightFishes[i] = Instantiate(lightFishPrefab, new Vector2(0, 0), Quaternion.identity);

        }

        for (int i = 0; i < 6; i++)
        {
            octopusEnemies[i] = Instantiate(octopusEnemyPrefab, new Vector2(startX + (3 * i), -90), Quaternion.identity);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
