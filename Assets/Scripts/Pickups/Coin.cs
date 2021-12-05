using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{

    void Update()
    {
        FindGameManager();
    }

    //collision/trigger checks
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.subStats.coinsCollected++;
            gameManager.soundManagerScript.PlayPickupSound(this.GetComponent<AudioSource>());
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
