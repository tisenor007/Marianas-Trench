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
    //public Text depthTxt;
    public GameObject subCamera;
    public UpgradeManager upgradeManager;

    [Header("Can set these for testing")]
    public int currentFuelUpgrade = 0;
    public int currentEngineUpgrade = 0;
    public int currentHullUpgrade = 0;
    public int currentPropellerUpgrade = 0;
    public int currentPressureResistanceUpgrade = 0;

    public int coinsCollected;
    public int coinWorth = 15;
    protected bool _treasureFound = false;
    public bool treasureFound
    {
        get { return _treasureFound; }
        set { _treasureFound = value; }
    }
    protected int _fuel;
    public int fuel
    {
        get { return _fuel; }
        set { _fuel = value; }
    }
    protected int engineEfficiency;
    protected bool _inShop;
    public bool inShop
    {
        get { return _inShop; }
        set { _inShop = value; }
    }
    protected int _currentDepth;
    public int currentDepth
    {
        get { return _currentDepth; }
        set { _currentDepth = value; }
    }
    protected int _currentMoney = 0;
    public int currentMoney
    {
        get { return _currentMoney; }
        set { _currentMoney = value; }
    }
    protected int _pressureHitTime = 5;
    public int pressureHitTime
    {
        get { return _pressureHitTime; }
        set { _pressureHitTime = value; }
    }
    private int pressureTimer = 0;
    private int originPressureHitTime;
    private int pressureDamage = 5;
    private int slowTime = 1;



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
        if (transform.position.y >= -148 + ((Screen.height / 100)/2))
        {
            subCamera.transform.position = new Vector3(subCamera.transform.position.x, transform.position.y, subCamera.transform.position.z);
        }
        if (transform.position.x < -((Screen.width / 100) / 2))
        {
            transform.position = new Vector2(-((Screen.width / 100) / 2), transform.position.y);
        }
        else if (transform.position.x > ((Screen.width / 100) / 2))
        {
            transform.position = new Vector2(((Screen.width / 100) / 2), transform.position.y);
        }
        healthTxt.text = "Sub Hull Armour: " + health.ToString();
        MoneyTxt.text = "Your Money: $" + currentMoney.ToString();
        fuelTxt.text = "Sub Fuel: " + fuel.ToString() + "L";
        //depthTxt.text = "Current Depth: " + currentDepth.ToString() + "ft";

        if (inShop == true)
        {
            ResetStats();
            rb.velocity = Vector3.zero;
        }

        else if (inShop == false)
        {
            //All ways game could end
            
            if (treasureFound == true && inShop == false) { treasureFound = false; SceneManager.LoadScene(Global.winSceneName, LoadSceneMode.Single); }
            
            if (isDead == false)
            {
                //GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                currentDepth = Mathf.RoundToInt(-this.gameObject.transform.position.y);
                CheckForPressure();
                //if (Input.GetKey(KeyCode.W))
                //rb.AddForce(Vector3.up * speed * Time.deltaTime);
                if (fuel > 0)
                {
                    if (Input.GetKey(KeyCode.W))
                    {
                        rb.AddForce(Vector3.up * speed * Time.deltaTime);
                        useFuel();
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        rb.AddForce(Vector3.left * (speed * 5f) * Time.deltaTime);
                        useFuel();
                    }
                    if (Input.GetKey(KeyCode.S))
                    {
                        rb.AddForce(Vector3.down * speed * Time.deltaTime);
                        useFuel();
                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        rb.AddForce(Vector3.right * (speed * 5f) * Time.deltaTime);
                        useFuel();
                    }
                    else if (!Input.anyKey)
                    {
                        if (rb.velocity.y < -0.15f)
                        {
                            if (Time.time > slowTime)
                            {
                                //Debug.Log("WORKS");
                                rb.velocity = new Vector2(rb.velocity.x * 0.5f, rb.velocity.y * 0.5f);
                                slowTime = Mathf.RoundToInt(Time.time) + 1;
                            }
                        }
                        else if (rb.velocity.y >= -0.15f) { rb.velocity = new Vector2(rb.velocity.x, -0.15f); }
                    }
                }

            }
            if (isDead == true)
            {
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            }
        }
    }
    public void ResetStats()
    {
        coinsCollected = 0;
        isDead = false;
        hullIsBroken = false;
        outOfFuel = false;
        currentDepth = 0;
        fuel = upgradeManager.fuelUpgrades[currentFuelUpgrade];
        engineEfficiency = upgradeManager.engineUpgrades[currentEngineUpgrade];
        health = upgradeManager.hullUpgrades[currentHullUpgrade];
        maxHealth = health;
        speed = upgradeManager.propellerUpgrades[currentPropellerUpgrade];
        pressureTimer = Mathf.RoundToInt(Time.time) + pressureHitTime;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    public void clearGameStats()
    {
        ResetStats();
        currentMoney = 0;
        currentHullUpgrade = 0;
        currentFuelUpgrade = 0;
        currentEngineUpgrade = 0;
        currentPropellerUpgrade = 0;
        currentPressureResistanceUpgrade = 0;
    }
    public void CheckForPressure()
    {
        Debug.Log(pressureHitTime);
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
    public void addFuel(int value)
    {
        fuel = fuel + value;
        if (fuel >= upgradeManager.fuelUpgrades[currentFuelUpgrade])
        {
            fuel = upgradeManager.fuelUpgrades[currentFuelUpgrade];
        }
    }
    public void Heal(int hp)
    {
        health = health + hp;
        if (health >= upgradeManager.hullUpgrades[currentHullUpgrade])
        {
            health = upgradeManager.hullUpgrades[currentHullUpgrade];
        }
    }

    //public bool isTeasureFound() { return treasureFound; }
    //money
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
        //if (other.gameObject.tag == "LightEnemy")
        //{
        //    TakeDamage(5);
        //}
        //if (other.gameObject.tag == "MediumFish")
        //{
        //    TakeDamage(25);
        //}
        //if (other.gameObject.tag == "HeavyFish")
        //{
            //TakeDamage(15);
        //}
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.gameObject.tag == "Coin")
        {
            addMoney(25);
        }*/
        if (other.gameObject.tag == "Treasure")
        {
            treasureFound = true;
        }
    }
    
}
