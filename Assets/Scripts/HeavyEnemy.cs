using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{   
    // Start is called before the first frame update
    protected static float viewDistance = 10f;
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
        attackDamage = 35;
        speed = Random.Range(1, 3);
        maxHealth = health;
        state = State.roam;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(state);
        if (target != null)
        {
            targetDistance = Vector3.Distance(target.transform.position, this.transform.position);
        }
        switch (state)
        {
            case State.roam:
                Roam();
                CheckPlayerInSight();
                break;
            case State.chasing:
                Chase();
                CheckToRetreat();
                break;
            case State.retreating:
                Retreat();
                CheckPlayerInSight();
                CheckToRoam();
                break;
        }
        
    }
    private void Chase()
    {
        //Vector3 diff = (target.transform.position - this.transform.position);
        //float angle = Mathf.Atan2(diff.y, diff.x);
        //transform.rotation = Quaternion.Euler(0f, 0f, angle * Mathf.Rad2Deg);

        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (speed * 10) * Time.deltaTime);
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

    private void Retreat()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(new Vector3(0, 0, 0)), (speed * 10) * Time.deltaTime);
        if (randomY > this.transform.position.y + 1)
        {
            this.transform.Translate(0, 0.001f * speed, 0);
        }
        else if (randomY < this.transform.position.y - 1)
        {
            this.transform.Translate(0, -0.001f * speed, 0);
        }
    }
    private void CheckToRoam()
    {
        if (this.transform.position.y <= randomY + 2f && this.transform.position.y >= randomY - 2f)
        {
            state = State.roam;
        }
    }

    private void CheckPlayerInSight()
    {
        if (target != null)
        {
            if (targetDistance <= viewDistance)
            {
                state = State.chasing;
            }
        }
    }

    private void CheckToRetreat()
    {
        if (target != null)
        {
            if (targetDistance > viewDistance)
            {
                state = State.retreating;
            }
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
