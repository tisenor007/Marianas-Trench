using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Submarine subStats;
    public GameObject earnedMoneyCanvas;
    public GameObject moneyCanvas;
    public GameObject insufficientFundsMessage;
    public CanvasGroup insufficientFundsCanvas;
    public Text earnedMoneyTxt;

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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeCanvas(true));
    }

    // Update is called once per frame
    void Update()
    {
        fuelCost = pricePerUpgrade[subStats.currentFuelUpgrade];
        engineCost = pricePerUpgrade[subStats.currentEngineUpgrade];
        hullCost = pricePerUpgrade[subStats.currentHullUpgrade];
        propellerCost = pricePerUpgrade[subStats.currentPropellerUpgrade];

        if (insufficientFundsCanvas.alpha <= 0)
        {
            StopCoroutine(FadeCanvas(false));
        }
    }
    public void UpgradeFuel()
    {
        if (subStats.GetMoney() >= fuelCost)
        {
            if (subStats.currentFuelUpgrade >= upgradeAmount - 1)
            {
                subStats.currentFuelUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.removeMoney(fuelCost);
                subStats.currentFuelUpgrade = subStats.currentFuelUpgrade + 1;
            }
        }
        else if (subStats.GetMoney() < fuelCost)
        {
            insufficientFundsCanvas.alpha = 1;
            StartCoroutine(FadeCanvas(true));
        }
    }

    public void UpgradeEngine()
    {
        if (subStats.GetMoney() >= engineCost)
        {
            if (subStats.currentEngineUpgrade >= upgradeAmount - 1)
            {
                subStats.currentEngineUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.currentEngineUpgrade = subStats.currentEngineUpgrade + 1;
                subStats.removeMoney(engineCost);
            }
        }
        else if (subStats.GetMoney() < engineCost)
        {
            insufficientFundsCanvas.alpha = 1;
            StartCoroutine(FadeCanvas(true));
        }
    }
    public void UpgradeHullArmour()
    {
        if (subStats.GetMoney() >= hullCost)
        {
            if (subStats.currentHullUpgrade >= upgradeAmount - 1)
            {
                subStats.currentHullUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.currentHullUpgrade = subStats.currentHullUpgrade + 1;
                subStats.removeMoney(hullCost);
            }
        }
        else if (subStats.GetMoney() < hullCost)
        {
            insufficientFundsCanvas.alpha = 1;
            StartCoroutine(FadeCanvas(true));
        }
    }
    public void UpgradePropeller()
    {
        if (subStats.GetMoney() >= propellerCost)
        {
            if (subStats.currentPropellerUpgrade >= upgradeAmount - 1)
            {
                subStats.currentPropellerUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.currentPropellerUpgrade = subStats.currentPropellerUpgrade + 1;
                subStats.removeMoney(propellerCost);
            }
        }
        else if (subStats.GetMoney() < propellerCost)
        {
            insufficientFundsCanvas.alpha = 1;
            StartCoroutine(FadeCanvas(true));
        }
    }
    public void updateButtons(Text fuelButtonTxt, Text engineButtonTxt, Text hullButtonTxt, Text propellerButtonTxt)
    {
        if (fuelButtonTxt != null)
        {
            if (subStats.currentFuelUpgrade >= upgradeAmount - 1)
            {
                fuelButtonTxt.text = "Fuel MAXED";
            }
            else if (subStats.currentFuelUpgrade < upgradeAmount - 1)
            {
                fuelButtonTxt.text = "$" + fuelCost + " - Upgrade to Fuel Tank Level " + (subStats.currentFuelUpgrade + 1);
            }
        }
        if (engineButtonTxt != null)
        {
            if (subStats.currentEngineUpgrade >= upgradeAmount - 1)
            {
                engineButtonTxt.text = "Engine MAXED";
            }
            else if (subStats.currentEngineUpgrade < upgradeAmount - 1)
            {
                engineButtonTxt.text = "$" + engineCost + " - Upgrade to Engine Level " + (subStats.currentEngineUpgrade + 1);
            }
        }
        if (hullButtonTxt != null)
        {
            if (subStats.currentHullUpgrade >= upgradeAmount - 1)
            {
                hullButtonTxt.text = "Hull Armour MAXED";
            }
            else if (subStats.currentHullUpgrade < upgradeAmount - 1)
            {
                hullButtonTxt.text = "$" + hullCost + " - Upgrade to Hull Armour Level " + (subStats.currentHullUpgrade + 1);
            }
        }
        if (propellerButtonTxt != null)
        {
            if (subStats.currentPropellerUpgrade >= upgradeAmount - 1)
            {
                propellerButtonTxt.text = "Propeller MAXED";
            }
            else if (subStats.currentPropellerUpgrade < upgradeAmount - 1)
            {
                propellerButtonTxt.text = "$" + propellerCost + " - Upgrade to Propeller Level " + (subStats.currentPropellerUpgrade + 1);
            }
        }
    }

    public IEnumerator FadeCanvas(bool fadeOut)
    {
        if (fadeOut == true)
        {
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                insufficientFundsCanvas.alpha = i;
                yield return null;
            }
        }
    }

}
