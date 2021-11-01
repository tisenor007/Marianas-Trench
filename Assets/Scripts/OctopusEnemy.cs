using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusEnemy : Enemy
{
    public GameObject submarine;
    public Vector2 visibleScreen;

    void Start()
    {
        submarine = GameObject.FindGameObjectWithTag("Player");
        speed = Random.Range(1f, 10f);
        attackDamage = 10;

        //Vector2 visibleScreen = new Vector3((Screen.width / 100), submarine.transform.position.y + ((Screen.height / 100) / 2) + 3);
    }

    
    void Update()
    {
        if (submarine.transform.position.y < -30 && submarine.transform.position.y > -90)
        {
            visibleScreen = new Vector3((Screen.width / 100), submarine.transform.position.y + ((Screen.height / 100) / 2) + 16);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            //Debug.Log(Screen.height / 100);

            if (gameObject.transform.position.y > visibleScreen.y && submarine.transform.position.y > -70)
            {
                Debug.Log("TRIGGERED");
                gameObject.transform.position = new Vector2(transform.position.x, (submarine.transform.position.y + -((Screen.height / 100) / 2) - 16));
            }
        }

        
    }
}
