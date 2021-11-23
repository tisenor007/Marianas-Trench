using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCan : Collectable
{
    public int refillAmount = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FindGameManager();
    }

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
