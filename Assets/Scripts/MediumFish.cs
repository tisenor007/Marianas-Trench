using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumFish : Enemy
{
    // Start is called before the first frame update

    void Start()
    {
        minY = -110;
        maxY = -85;
        randomY = Random.Range(minY, maxY);

        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        //right = true;

        health = 50;
        attackDamage = 20;
        attackDistance = 3;
        attackSpeed = 4;
        speed = Random.Range(4, 8);

        maxHealth = health;

        transform.position = new Vector2(-(Screen.width / 100) - 2, randomY);

        state = State.roam;
    }

    // Update is called once per frame
    void Update()
    {
        FindGameManager();
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
