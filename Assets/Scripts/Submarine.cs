using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submarine : MonoBehaviour
{
    public int speed = 50;
    public int health = 100;
    public Text healthTxt;
    public Text coinTxt;
    public bool inShop;
    private int coinCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthTxt.text = "Health: " + health.ToString();
        coinTxt.text = "Coins: " + coinCounter.ToString();

        if (inShop == false)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();

            if (Input.GetKey(KeyCode.W))
                rb.AddForce(Vector3.up * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.A))
                rb.AddForce(Vector3.left * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.S))
                rb.AddForce(Vector3.down * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.D))
                rb.AddForce(Vector3.right * speed * Time.deltaTime);

        }
        if (inShop == true)
        {
            //reset stats
            health = 100;
        }

        if (health <= 0) { health = 0; SceneManager.LoadScene("GameOver", LoadSceneMode.Single); }
        //Debug.Log(health);
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "LightEnemy")
        {
            health = health - 50;
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coinCounter++;
        }
    }
}
