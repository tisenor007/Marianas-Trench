using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusEnemy : Enemy
{
    private Vector2 visibleScreen;

    void Start()
    {
        //submarine = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(1f, 5f);
        attackDamage = 10;

        //Vector2 visibleScreen = new Vector3((Screen.width / 100), submarine.transform.position.y + ((Screen.height / 100) / 2) + 3);

        state = State.roam;
    }


    void Update()
    {
        switch (state) 
        {
            case State.roam:
                target = GameObject.FindGameObjectWithTag("Player");
                if (target.transform.position.y < -30 && target.transform.position.y > -90)
                {
                    visibleScreen = new Vector3((Screen.width / 100), target.transform.position.y + ((Screen.height / 100) / 2) + 16);
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                    //Debug.Log(Screen.height / 100);

                    if (gameObject.transform.position.y > visibleScreen.y && target.transform.position.y > -70)
                    {
                        Debug.Log("TRIGGERED");
                        gameObject.transform.position = new Vector2(transform.position.x, (target.transform.position.y + -((Screen.height / 100) / 2) - 16));
                    }
                }
                break;
        }

        
    }
}
