using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : Collectable
{
    //VARIABLES
    //Item context (how much will the item effect the recipient)
    public int refillAmount = 15;

    void Update()
    {
        FindGameManager();
    }

    //collision/trigger checks
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.subStats.addFuel(refillAmount);
            gameManager.soundManagerScript.PlayPickupSound(this.GetComponent<AudioSource>());
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
