using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{   
    // Start is called before the first frame update
    void Start()
    {
        minY = -115;
        maxY = -135;
        randomY = Random.Range(minY, maxY);

        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        //right = true;

        transform.position = new Vector2(-(Screen.width / 100) - 2, randomY);

        health = 40;
        attackDamage = 25;
        speed = Random.Range(1, 3);
        maxHealth = health;
        state = State.roam;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(targetDistance);
        if (target != null)
        {
            targetDistance = Vector3.Distance(target.transform.position, this.transform.position);
        }
        switch (state)
        {
            case State.roam:
                Roam();
                CheckPlayerInRange();
                break;
            case State.chasing:
                Chase();
                break;
        }
        
    }
    private void Chase()
    {
        if (target.transform.position.y > this.transform.position.y + 1)
        {
            this.transform.Translate(0, 0.001f * speed, 0);
        }
        else if (target.transform.position.y < this.transform.position.y - 1)
        {
            this.transform.Translate(0, -0.001f * speed, 0);
        }

        if (target.transform.position.x > this.transform.position.x + 1)
        {
            this.transform.Translate(0.001f * speed, 0, 0);
        }
        else if (target.transform.position.x < this.transform.position.x - 1)
        {
            this.transform.Translate(-0.001f * speed, 0, 0);
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
        }
    }
}