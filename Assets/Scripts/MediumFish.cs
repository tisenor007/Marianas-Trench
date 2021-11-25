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
        attackDistance = 5;
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
