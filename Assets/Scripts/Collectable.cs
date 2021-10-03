using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    public Text txtCoins;
    public int coins;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("trigger");
            //gameObject.GetComponent<SphereCollider>().enabled = false;
            txtCoins.text = "Coins: " + coins.ToString();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}

