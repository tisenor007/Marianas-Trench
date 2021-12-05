using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairKit : Collectable
{
    //VARIABLES
    //Item context (how much will the item effect the recipient)
    public int healAmount = 10;
   
    void Update()
    {
        FindGameManager();
    }

    //collision/trigger checks
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gameManager.subStats.Heal(healAmount);
            gameManager.soundManagerScript.PlayPickupSound(this.GetComponent<AudioSource>());
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
