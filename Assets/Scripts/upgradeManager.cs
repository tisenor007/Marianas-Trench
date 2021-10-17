using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Submarine subStats;
    public GameObject earnedMoneyCanvas;
    public GameObject moneyCanvas;
    public GameObject InsufficientFundsMessage;
    public CanvasGroup InsufficientFundsCanvas;
    public Text earnedMoneyTxt;

    [Header("Can set these for testing")]
    public int currentFuelUpgrade = 0;
    public int currentEngineUpgrade = 0;
    public int currentHullUpgrade = 0;
    public int currentPropellerUpgrade = 0;

    [Header("Set these for game play")]
    public int[] fuelUpgrades = new int[upgradeAmount];
    public int[] engineUpgrades = new int[upgradeAmount];
    public int[] hullUpgrades = new int[upgradeAmount];
    public int[] propellerUpgrades = new int[upgradeAmount];

    public int[] pricePerUpgrade = new int[upgradeAmount];
    protected int fuelCost;
    protected int engineCost;
    protected int hullCost;
    protected int propellerCost;

    protected static int upgradeAmount = 8;

    protected bool fadingOut;
    // Start is called before the first frame update
    void Start()
    {
        fadingOut = true;
    }

    // Update is called once per frame
    void Update()
    {
        fuelCost = pricePerUpgrade[currentFuelUpgrade];
        engineCost = pricePerUpgrade[currentEngineUpgrade];
        hullCost = pricePerUpgrade[currentHullUpgrade];
        propellerCost = pricePerUpgrade[currentPropellerUpgrade];

        if (fadingOut == true)
        {
            if (InsufficientFundsCanvas.alpha >= 0)
            {
                InsufficientFundsCanvas.alpha -= Time.deltaTime;
                if (InsufficientFundsCanvas.alpha == 0)
                {
                }
            }
        }
        else if (fadingOut == false)
        {
            InsufficientFundsCanvas.alpha = 1;
            fadingOut = true;
        }
        Debug.Log(fadingOut);
    }
    public void UpgradeFuel()
    {
        if (subStats.GetMoney() >= fuelCost)
        {
            if (currentFuelUpgrade >= upgradeAmount - 1)
            {
                currentFuelUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.removeMoney(fuelCost);
                currentFuelUpgrade = currentFuelUpgrade + 1;
            }
        }
        else if (subStats.GetMoney() < fuelCost)
        {
            fadingOut = false;
        }
    }

    public void UpgradeEngine()
    {
        if (subStats.GetMoney() >= engineCost)
        {
            if (currentEngineUpgrade >= upgradeAmount - 1)
            {
                currentEngineUpgrade = upgradeAmount - 1;
            }
            else
            {
                currentEngineUpgrade = currentEngineUpgrade + 1;
                subStats.removeMoney(engineCost);
            }
        }
        else if (subStats.GetMoney() < engineCost)
        {
            fadingOut = false;
        }
    }
    public void UpgradeHullArmour()
    {
        if (subStats.GetMoney() >= hullCost)
        {
            if (currentHullUpgrade >= upgradeAmount - 1)
            {
                currentHullUpgrade = upgradeAmount - 1;
            }
            else
            {
                currentHullUpgrade = currentHullUpgrade + 1;
                subStats.removeMoney(hullCost);
            }
        }
        else if (subStats.GetMoney() < hullCost)
        {
            fadingOut = false;
        }
    }
    public void UpgradePropeller()
    {
        if (subStats.GetMoney() >= propellerCost)
        {
            if (currentPropellerUpgrade >= upgradeAmount - 1)
            {
                currentPropellerUpgrade = upgradeAmount - 1;
            }
            else if (subStats.GetMoney() < propellerCost)
            {
                currentPropellerUpgrade = currentPropellerUpgrade + 1;
                subStats.removeMoney(propellerCost);
            }
        }
        else
        {
            fadingOut = false;
        }
    }

}
