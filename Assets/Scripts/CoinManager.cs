using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    public GameObject coinPrefab;
    public GameObject[] coins = new GameObject[75];

    void Start()
    {
        minX = -9;
        maxX = 9;

        minY = -145;
        maxY = -1;

        for (int i = 0; i < 150; i++)
        {
            coins[i] = Instantiate(coinPrefab, new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY)), Quaternion.identity);

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
