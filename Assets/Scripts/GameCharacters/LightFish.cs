using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightFish : Enemy
{

    void Start()
    {
        //Start Stats
        minY = -30;
        maxY = -1;
        randomY = Random.Range(minY, maxY);
        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        health = 20;
        health = 50;
        attackDamage = 2;
        attackDistance = 4;
        attackSpeed = 1;
        speed = Random.Range(2, 6);
        maxHealth = health;
        transform.position = new Vector2(-(Screen.width / 100) - 2, randomY);
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
        switch (state)
        {
            case State.roam:
                Roam();
                CheckToAttack();
                break;
            case State.attacking:
                Attack();
                CheckToRoam();
                break;
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
