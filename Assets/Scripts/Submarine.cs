using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submarine : Character
{
    public int speed;
    public int currentFuelUpgrade = 0;
    public int currentEngineUpgrade = 0;
    public Text healthTxt;
    public Text MoneyTxt;
    public Text fuelTxt;
    public int[] fuelUpgrades = new int[8];
    public int[] engineUpgrades = new int[8];
    protected int fuel;
    protected int engineEfficiency;
    protected bool inShop;
    protected float currentDepth;
    protected float currentMoney = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        ResetStats();
         rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       
        healthTxt.text = "Sub Hull: " + health.ToString();
        MoneyTxt.text = "Your Money: " + currentMoney.ToString();
        fuelTxt.text = "Sub Fuel: " + fuel.ToString() + "L";
        currentDepth = +this.gameObject.transform.position.y;

        if (inShop == false)
        {
            if (getFuel() <= 0 || GetIsDead() == true) { SceneManager.LoadScene(Global.gameOverScene, LoadSceneMode.Single); }
            if (health <= 0)
            {
                isDead = true;
            }
            else
            {
                ResetStats();
                //if (Input.GetKey(KeyCode.W))
                //rb.AddForce(Vector3.up * speed * Time.deltaTime);
                if (fuel > 0)
                {
                    if (Input.GetKey(KeyCode.A))
                    {
                        rb.AddForce(Vector3.left * speed * Time.deltaTime);
                        useFuel();
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        rb.AddForce(Vector3.down * speed * Time.deltaTime);
                        useFuel();
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        rb.AddForce(Vector3.right * speed * Time.deltaTime);
                        useFuel();
                    }
                }

            }
        }
        if (inShop == true)
        {
            ResetStats();
            rb.velocity = Vector3.zero;
        }
    }
    public void ResetStats()
    {
        isDead = false;
        fuel = fuelUpgrades[currentFuelUpgrade];
        engineEfficiency = engineUpgrades[currentEngineUpgrade];
        health = 100;
        maxHealth = health;
    }
    public void useFuel()
    {
        engineEfficiency--;
        Debug.Log(engineEfficiency);
        if (engineEfficiency <= 0)
        {
            fuel = fuel - 1;
            engineEfficiency = engineUpgrades[currentEngineUpgrade];
        }
        if (fuel <= 0)
        {
            fuel = 0;
        }
    }
    public void upgradeFuel()
    {
        currentFuelUpgrade = currentFuelUpgrade + 1;
        Debug.Log("3");
    }
    public int getFuel(){return fuel;}
    public int getCurrFuelUpdrage(){return currentFuelUpgrade;}
    //shop status
    public bool getInShopStatus(){return inShop;}
    public void setInShopStatus(bool status){inShop = status;}
    //money
    public void addMoney(float amount)
    {
        currentMoney = currentMoney + amount;
    }
    public void removeMoney(float amount)
    {
        currentMoney = currentMoney - amount;
        if (currentMoney <= 0)
        {
            currentMoney = 0;
        }
    }
    //collison
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "LightEnemy")
        {
            TakeDamage(50);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            addMoney(100);
        }
    }
}
