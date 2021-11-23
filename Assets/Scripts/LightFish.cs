using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightFish : Enemy
{

    void Start()
    {
        minY = -30;
        maxY = -1;
        randomY = Random.Range(minY, maxY);

        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        //right = true;
        
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

    //public void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.tag == "Player" || other.gameObject.tag == "LightEnemy")
    //    {
    //        if (right == true)
    //        {
    //            right = false;
    //        }
    //        else if (right == false)
    //        {
    //            right = true;
    //        }
    //    }
        
    //}
}
