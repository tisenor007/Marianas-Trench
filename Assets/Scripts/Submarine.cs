using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submarine : Character
{
    public int speed;
    public Text healthTxt;
    public Text MoneyTxt;
    public Text fuelTxt;
    public int[] fuelUpgrades = new int[8];
    public int[] engineUpgrades = new int[8];
    protected int currentEngineUpgrade;
    protected int fuel;
    protected int currentFuelUpgrade;
    protected int fuelUsage;
    protected int fuelEfficiency;
    protected bool inShop;
    protected float currentDepth;
    protected float currentMoney = 0;

    

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxShield = shield;
         rb = GetComponent<Rigidbody2D>();
        currentFuelUpgrade = 0;
        currentEngineUpgrade = 0;
        fuelEfficiency = fuelUsage;
    }

    // Update is called once per frame
    void Update()
    {
        fuel = fuelUpgrades[currentFuelUpgrade];
        fuelUsage = engineUpgrades[currentEngineUpgrade];
        healthTxt.text = "Health: " + health.ToString();
        MoneyTxt.text = "Your Money: " + currentMoney.ToString();
        fuelTxt.text = "Your Fuel: " + fuel.ToString() + "L";
        currentDepth = +this.gameObject.transform.position.y;

        if (inShop == false)
        {
            if (health <= 0)
            {
                isDead = true;
            }
            else
            {
                isDead = false;
                //if (Input.GetKey(KeyCode.W))
                //rb.AddForce(Vector3.up * speed * Time.deltaTime);
                if (Input.GetKey(KeyCode.A))
                {
                    rb.AddForce(Vector3.left * speed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    rb.AddForce(Vector3.down * speed * Time.deltaTime);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    rb.AddForce(Vector3.right * speed * Time.deltaTime);
                    useFuel();
                }
            }
        }
        if (inShop == true)
        {
            health = 100;
            rb.velocity = Vector3.zero;
        }
    }
    public void useFuel()
    {
        engineUpgrades[currentEngineUpgrade]--;
        Debug.Log("YEE");
        if (engineUpgrades[currentEngineUpgrade] <= 0)
        {
            fuel = fuel - 1;
            engineUpgrades[currentEngineUpgrade] = fuelEfficiency;
        }
    }
    public void upgradeFuel()
    {
        currentFuelUpgrade = currentFuelUpgrade + 1;
        fuelEfficiency = fuelUsage;
    }
    public int getCurrFuelUpdrage()
    {
        return currentFuelUpgrade;
    }
    //shop status
    public bool getInShopStatus()
    {
        return inShop;
    }
    public void setInShopStatus(bool status)
    {
        inShop = status;
    }
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
        if (isDead == true) { SceneManager.LoadScene("GameOver", LoadSceneMode.Single); }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            addMoney(100);
        }
    }
}
