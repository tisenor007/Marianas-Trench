using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    //public GameObject fish;

    public bool right;
    public float speed;
    public int damage;


    void Start()
    {
        isDead = false;
        transform.position = new Vector2(-(Screen.width / 100) - 2, -1);
        rb = GetComponent<Rigidbody2D>();
        right = true;
        if (this.gameObject.tag == "LightEnemy")
        {
            health = 20;
            damage = 10;
            speed = 2;
        }
        maxHealth = health;
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
        if (other.gameObject.tag == "Player")
        {
            health = health - damage;
        }
    }
}
