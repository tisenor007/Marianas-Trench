using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   // public GameObject enemyManager;
    public GameObject lightFishPrefab;
    public GameObject octopusEnemyPrefab;
    public GameObject mediumFishPrefab;
    public GameObject heavyEnemyPrefab;
    
    private static int lightFishAmount = 15;
    private static int mediumFishAmount = 10;
    private static int octopusAmount = 4;
    private static int heavyEnemyAmount = 2;

    private GameObject[] lightFishes = new GameObject[lightFishAmount];
    private GameObject[] mediumFishes = new GameObject[mediumFishAmount];
    private GameObject[] octopusEnemies = new GameObject[octopusAmount];
    private GameObject[] heavyEnemies = new GameObject[heavyEnemyAmount];

    public float startX = -7.5f;

    void Awake()
    {
        for (int i = 0; i < lightFishAmount; i++)
        {
            lightFishes[i] = Instantiate(lightFishPrefab, new Vector2(0, 0), Quaternion.identity);
        }

        for (int i = 0; i < mediumFishAmount; i++)
        {
            mediumFishes[i] = Instantiate(mediumFishPrefab, new Vector2(0, 0), Quaternion.identity);
        }

        for (int i = 0; i < octopusAmount; i++)
        {
            octopusEnemies[i] = Instantiate(octopusEnemyPrefab, new Vector2(startX + (3 * i), -90), Quaternion.identity);
        }

        for (int i = 0; i < heavyEnemyAmount; i++)
        {
            heavyEnemies[i] = Instantiate(heavyEnemyPrefab, new Vector2(0, 0), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
