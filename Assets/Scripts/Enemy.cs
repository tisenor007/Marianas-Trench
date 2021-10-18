using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public int minY;
    public int maxY;

    public int randomY;

    void Start()
    {
        //randomY = Random.Range(minY, maxY);
    }

    // Update is called once per frame
    void Update()
    {

        



    }

    public void OnCollisionEnter2D(Collision2D other)
    {

    }
}
