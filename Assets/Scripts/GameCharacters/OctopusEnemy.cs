using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusEnemy : Enemy
{
    //VARIABLES
    private Vector2 visibleScreen;

    void Start()
    {
        speed = Random.Range(1f, 5f);
        attackDamage = 10;
        attackDistance = 3;
        attackSpeed = 3;
        state = State.roam;
    }

    void Update()
    {
        SetGameManager();
        if (target != null)
        {
            targetDistance = Vector3.Distance(target.transform.position, this.transform.position);
            targetScript = target.GetComponent<Character>();
        }
        if (target == null)
        {
            targetDistance = 0;
            targetScript = null;
        }
        //STATE MACHINE
        switch (state) 
        {
            case State.roam:
                VerticalRoam();
                CheckToAttack();
                break;
            case State.attacking:
                Attack();
                CheckToRoam();
                break;
        }
    }

    //Alternate Roam Method specific to this enemy type
    public void VerticalRoam()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        if (target.transform.position.y < -30 && target.transform.position.y > -90)
        {
            visibleScreen = new Vector3((Screen.width / 100), target.transform.position.y + ((Screen.height / 100) / 2) + 16);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            if (gameObject.transform.position.y > visibleScreen.y && target.transform.position.y > -70)
            {
                Debug.Log("TRIGGERED");
                gameObject.transform.position = new Vector2(transform.position.x, (target.transform.position.y + -((Screen.height / 100) / 2) - 16));
            }
        }
    }

    //Collision/Trigger Checks
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Rock" || other.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), other.gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = null;
        }
    }
}
