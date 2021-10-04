using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    //public GameObject fish;

    public bool right;
    public float speed;
    public int health = 20;
    public int damage = 10;

    public Text healthTxt;


    void Start()
    {
        speed = 2;
        transform.position = new Vector2(-(Screen.width / 100) - 2, -1);
        right = true;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        health = health - damage;
        healthTxt.text = "Health: " + health.ToString();
        if (health >= 0)
        {

        }
    }
}
