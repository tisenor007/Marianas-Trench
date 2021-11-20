using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private static int obstacleSpacing = 12;
    private GameObject[] obstacles = new GameObject[-(Global.mapLengthY)];
    private float xLocation;
    // Start is called before the first frame update
    void Start()
    {
        int choice;
        
        for (int i = obstacleSpacing; i < obstacles.Length; i = i + obstacleSpacing)
        {
            choice = Random.Range(0, 3);
            if (choice == 1) { xLocation = ((Screen.width / 100) / 2) -obstaclePrefab.transform.localScale.x / 2; }
            if (choice == 2) { xLocation = -((Screen.width / 100) / 2) + obstaclePrefab.transform.localScale.x / 2; }
            obstacles[i] = Instantiate(obstaclePrefab, new Vector2(xLocation, -(i)), Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
