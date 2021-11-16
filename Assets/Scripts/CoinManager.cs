using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;
    private float minX;
    private float maxX;
    private float minY;
    private float maxY;

    private static int coinAmount = 50;
    private GameObject[] coins = new GameObject[coinAmount];

    void Start()
    {
        minX = -9;
        maxX = 9;

        minY = -145;
        maxY = -1;

        for (int i = 0; i < coinAmount; i++)
        {
            coins[i] = Instantiate(coinPrefab, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
