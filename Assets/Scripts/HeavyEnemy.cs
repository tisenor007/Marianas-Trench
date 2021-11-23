using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyEnemy : Enemy
{   
    // Start is called before the first frame update
    protected static float viewDistance = 16f;
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
        attackDistance = 6;
        attackSpeed = 6;
        speed = Random.Range(1, 3);
        maxHealth = health;
        state = State.roam;

        

    }

    // Update is called once per frame
    void Update()
    {
        FindGameManager();
        //Debug.Log(state);
        //Debug.Log(targetDistance);
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
                CheckPlayerInSight();
                break;
            case State.chasing:
                Chase();
                CheckToRetreat();
                CheckToAttack();
                break;
            case State.retreating:
                Retreat();
                CheckPlayerInSight();
                CheckToRoam();
                CheckToAttack();
                break;
            case State.attacking:
                Attack();
                CheckPlayerInSight();
                break;
        }
        
    }
    private void Chase()
    {
       
        if (target != null)
        {
            float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, (speed * 10) * Time.deltaTime);
            
            this.transform.Translate(0.001f * speed, 0, 0);
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

    private void CheckPlayerInSight()
    {
        if (target != null)
        {
            if (targetDistance <= viewDistance && targetDistance > attackDistance)
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
        else if (target == null)
        {
            state = State.retreating;
        }
    }
    
}
