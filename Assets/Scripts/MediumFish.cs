using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MediumFish : Enemy
{
    // Start is called before the first frame update
    public bool right;

    void Start()
    {

        minY = -110;
        maxY = -85;
        randomY = Random.Range(minY, maxY);

        isDead = false;
        rb = GetComponent<Rigidbody2D>();
        right = true;

        health = 50;
        attackDamage = 20;
        speed = Random.Range(4, 8);

        maxHealth = health;

        transform.position = new Vector2(-(Screen.width / 100) - 2, randomY);
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x > (Screen.width / 100) + 2)
            {
                right = false;
            }
        }


        if (right == false)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x < -(Screen.width / 100) - 2)
            {
                right = true;
            }
        }

    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "LightEnemy")
        {
            if (right == true)
            {
                right = false;
            }
            else if (right == false)
            {
                right = true;
            }
        }

    }
}