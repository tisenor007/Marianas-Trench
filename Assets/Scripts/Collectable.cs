using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    //public Text txtCoins;
    //public int coins;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //txtCoins.text = "Coins: " + coins.ToString();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

