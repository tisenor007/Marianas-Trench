using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Submarine : Character
{
    public Text healthTxt;
    public Text MoneyTxt;
    public Text fuelTxt;
    public Text depthTxt;
    public UpgradeManager upgradeManager;

    [Header("Can set these for testing")]
    public int currentFuelUpgrade = 0;
    public int currentEngineUpgrade = 0;
    public int currentHullUpgrade = 0;
    public int currentPropellerUpgrade = 0;
    public int currentPressureResistanceUpgrade = 0;

    protected int speed;
    protected int fuel;
    protected int engineEfficiency;
    protected bool inShop;
    protected int currentDepth;
    protected int currentMoney = 10000000;
    private int pressureTimer = 0;
    private int pressureHitTime = 5;
    private int originPressureHitTime;
    private int pressureDamage = 5;



    // Start is called before the first frame update
    void Start()
    {
        ResetStats();
         rb = GetComponent<Rigidbody2D>();
        originPressureHitTime = pressureHitTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -(Screen.width / 100) + 2)
        {
            transform.position = new Vector2(-(Screen.width / 100) + 2, transform.position.y);
        }
        else if (transform.position.x > (Screen.width / 100) - 2)
        {
            transform.position = new Vector2(Screen.width / 100 - 2, transform.position.y);
        }
        healthTxt.text = "Sub Hull Armour: " + health.ToString();
        MoneyTxt.text = "Your Money: $" + currentMoney.ToString();
        fuelTxt.text = "Sub Fuel: " + fuel.ToString() + "L";
        depthTxt.text = "Current Depth: " + currentDepth.ToString() + "ft";

        if (inShop == true)
        {
            ResetStats();
            rb.velocity = Vector3.zero;
        }

        else if (inShop == false)
        {
            //All ways game could end
            if (fuel <= 0 || GetIsDead() == true) { addMoney(currentDepth); SceneManager.LoadScene(Global.gameOverSceneName, LoadSceneMode.Single); }
            if (health <= 0)
            {
                isDead = true;
            }
            else
            {
                currentDepth = Mathf.RoundToInt(-this.gameObject.transform.position.y);
                CheckForPressure();
                if (Input.GetKey(KeyCode.W))
                rb.AddForce(Vector3.up * speed * Time.deltaTime);
                if (fuel > 0)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        rb.AddForce(Vector3.up * speed * Time.deltaTime);
                        useFuel();
                    }
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
                    else if (!Input.anyKey)
                    { 
                        rb.velocity = new Vector3(0, -0.15f, 0);
                    }
                }

            }
        }
    }
    public void ResetStats()
    {
        isDead = false;
        currentDepth = 0;
        fuel = upgradeManager.fuelUpgrades[currentFuelUpgrade];
        engineEfficiency = upgradeManager.engineUpgrades[currentEngineUpgrade];
        health = upgradeManager.hullUpgrades[currentHullUpgrade];
        maxHealth = health;
        speed = upgradeManager.propellerUpgrades[currentPropellerUpgrade];
    }
    public void clearGameStats()
    {
        ResetStats();
        SetMoney(0);
        currentHullUpgrade = 0;
        currentFuelUpgrade = 0;
        currentEngineUpgrade = 0;
        currentPropellerUpgrade = 0;
    }
    public void CheckForPressure()
    {
        if (currentDepth > upgradeManager.pressureResistanceUpgrades[currentPressureResistanceUpgrade] && isDead == false)
        {
            if (Time.time > pressureTimer)
            {
                TakeDamage(pressureDamage);
                pressureHitTime = pressureHitTime - 1;
                if (pressureHitTime <= 1)
                {
                    pressureHitTime = 1;
                }
                pressureTimer = Mathf.RoundToInt(Time.time) + pressureHitTime;
                
            }
        }
        else if (currentDepth < upgradeManager.pressureResistanceUpgrades[currentPressureResistanceUpgrade])
        {
            pressureHitTime = originPressureHitTime;
        }

    }
    public void useFuel()
    {
       
        if (Time.time > engineEfficiency)
        {
            fuel = fuel - 1;
            engineEfficiency = Mathf.RoundToInt(Time.time) + upgradeManager.engineUpgrades[currentEngineUpgrade];
        }
        if (fuel <= 0)
        {
            fuel = 0;
        }
    }
    public int GetCurrentDepth()
    {
        return currentDepth;
    }
    //shop status
    public bool getInShopStatus(){return inShop;}
    public void setInShopStatus(bool status){inShop = status;}
    //money
    public int GetMoney()
    {
        return currentMoney;
    }
    public void SetMoney(int amount)
    {
        currentMoney = amount;
    }
    public void addMoney(int amount)
    {
        currentMoney = currentMoney + amount;
    }
    public void removeMoney(int amount)
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
            TakeDamage(10);
        }
        if (other.gameObject.tag == "LightEnemy")
        {
            TakeDamage(15);
        }
        if (other.gameObject.tag == "LightEnemy")
        {
            TakeDamage(30);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Coin")
        {
            addMoney(10);
        }
    }
}
