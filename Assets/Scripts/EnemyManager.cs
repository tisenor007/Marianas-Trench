using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
   // public GameObject enemyManager;
    public GameObject lightFishPrefab;
    public GameObject[] lightFishes = new GameObject[20];

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            lightFishes[i] = Instantiate(lightFishPrefab, new Vector2(0, 0), Quaternion.identity);

        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
