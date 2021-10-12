using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submarine : Character
{
    public int speed = 50;
    public Text healthTxt;
    public Text coinTxt;
    public bool inShop;
    private int coinCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        healthTxt.text = "Health: " + health.ToString();
        coinTxt.text = "Coins: " + coinCounter.ToString();

        if (inShop == false)
        {
            if (health <= 0)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
                if (Input.GetKey(KeyCode.W))
                    rb.AddForce(Vector3.up * speed * Time.deltaTime);
                if (Input.GetKey(KeyCode.A))
                    rb.AddForce(Vector3.left * speed * Time.deltaTime);
                if (Input.GetKey(KeyCode.S))
                    rb.AddForce(Vector3.down * speed * Time.deltaTime);
                if (Input.GetKey(KeyCode.D))
                    rb.AddForce(Vector3.right * speed * Time.deltaTime);
            }
        }
        if (inShop == true)
        {
            health = 100;
            rb.velocity = Vector3.zero;
        }
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "LightEnemy")
        {
            TakeDamage(50);
        }
        if (isDead == true) { SceneManager.LoadScene("GameOver", LoadSceneMode.Single); }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coinCounter++;
        }
    }
}
