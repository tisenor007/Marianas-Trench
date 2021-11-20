using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public GameObject coinPrefab;
    public GameObject repairKitPrefab;
    public GameObject fuelCanPrefab;
    private static int coinAmount = 50;
    private static int repairKitAmount = 20;
    private static int fuelCanAmount = 20;
    private GameObject[] coins = new GameObject[coinAmount];
    private GameObject[] repairKits = new GameObject[repairKitAmount];
    private GameObject[] fuelCans = new GameObject[fuelCanAmount];
    private float xRangeMin;
    private float xRangeMax;
    private float yRangeMin;
    private float yRangeMax;

    void Start()
    {
        xRangeMin = -9;
        xRangeMax = 9;

        yRangeMin = -145;
        yRangeMax = -1;

        for (int i = 0; i < coinAmount; i++)
        {
            coins[i] = Instantiate(coinPrefab, new Vector2(Random.Range(xRangeMin, xRangeMax), Random.Range(yRangeMin, yRangeMax)), Quaternion.identity);

        }
        for (int i = 0; i < repairKitAmount; i++)
        {
            repairKits[i] = Instantiate(repairKitPrefab, new Vector2(Random.Range(xRangeMin, xRangeMax), Random.Range(yRangeMin - 20, yRangeMax)), Quaternion.identity);

        }
        for (int i = 0; i < fuelCanAmount; i++)
        {
            fuelCans[i] = Instantiate(fuelCanPrefab, new Vector2(Random.Range(xRangeMin, xRangeMax), Random.Range(yRangeMin - 20, yRangeMax)), Quaternion.identity);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
