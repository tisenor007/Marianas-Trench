using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    //VARIABLES
    public Submarine subStats;
    public GameObject moneyCanvas;
    public GameManager gameManager;
    public Color enabledColour;
    public Color disabledColour;
    protected static int _upgradeAmount = 8;
    public static int upgradeAmount
    {
        get { return _upgradeAmount; }
        set { _upgradeAmount = value; }
    }

    [HideInInspector]
    public int[] fuelUpgrades = new int[upgradeAmount];
    [HideInInspector]
    public int[] engineUpgrades = new int[upgradeAmount];
    [HideInInspector]
    public int[] hullUpgrades = new int[upgradeAmount];
    [HideInInspector]
    public int[] propellerUpgrades = new int[upgradeAmount];
    [HideInInspector]
    public int[] pressureResistanceUpgrades = new int[upgradeAmount];
    [HideInInspector]
    public int[] pricePerUpgrade = new int[upgradeAmount];
    public Transform[] pips = new Transform[8];
    protected int fuelCost;
    protected int engineCost;
    protected int hullCost;
    protected int propellerCost;
    protected int pressureResistanceCost;

    private void Awake()
    {
        SetGameplayStats();
    }

    void Update()
    {
        fuelCost = pricePerUpgrade[subStats.currentFuelUpgrade];
        engineCost = pricePerUpgrade[subStats.currentEngineUpgrade];
        hullCost = pricePerUpgrade[subStats.currentHullUpgrade];
        propellerCost = pricePerUpgrade[subStats.currentPropellerUpgrade];
        pressureResistanceCost = pricePerUpgrade[subStats.currentPressureResistanceUpgrade];
    }

    public void UpgradePressureResistance()
    {
        if (subStats.currentMoney >= pressureResistanceCost)
        {
            if (subStats.currentPressureResistanceUpgrade >= upgradeAmount - 1)
            {
                subStats.currentPressureResistanceUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.removeMoney(pressureResistanceCost);
                subStats.currentPressureResistanceUpgrade = subStats.currentPressureResistanceUpgrade + 1;
                gameManager.soundManagerScript.PlayUpgradeSound(this.GetComponent<AudioSource>());
            }
        }
    }

    public void UpgradeFuel()
    {
        if (subStats.currentMoney >= fuelCost)
        {
            if (subStats.currentFuelUpgrade >= upgradeAmount - 1)
            {
                subStats.currentFuelUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.removeMoney(fuelCost);
                subStats.currentFuelUpgrade = subStats.currentFuelUpgrade + 1;
                gameManager.soundManagerScript.PlayUpgradeSound(this.GetComponent<AudioSource>());
            }
        }
    }

    public void UpgradeEngine()
    {
        if (subStats.currentMoney >= engineCost)
        {
            if (subStats.currentEngineUpgrade >= upgradeAmount - 1)
            {
                subStats.currentEngineUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.currentEngineUpgrade = subStats.currentEngineUpgrade + 1;
                subStats.removeMoney(engineCost);
                gameManager.soundManagerScript.PlayUpgradeSound(this.GetComponent<AudioSource>());
            }
        }
    }

    public void UpgradeHullArmour()
    {
        if (subStats.currentMoney >= hullCost)
        {
            if (subStats.currentHullUpgrade >= upgradeAmount - 1)
            {
                subStats.currentHullUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.currentHullUpgrade = subStats.currentHullUpgrade + 1;
                subStats.removeMoney(hullCost);
                gameManager.soundManagerScript.PlayUpgradeSound(this.GetComponent<AudioSource>());
            }
        }
    }

    public void UpgradePropeller()
    {
        if (subStats.currentMoney >= propellerCost)
        {
            if (subStats.currentPropellerUpgrade >= upgradeAmount - 1)
            {
                subStats.currentPropellerUpgrade = upgradeAmount - 1;
            }
            else
            {
                subStats.currentPropellerUpgrade = subStats.currentPropellerUpgrade + 1;
                subStats.removeMoney(propellerCost);
                gameManager.soundManagerScript.PlayUpgradeSound(this.GetComponent<AudioSource>());
            }
        }
    }

    public void updateButtons(GameObject fuelButton, GameObject engineButton, GameObject hullButton, GameObject propellerButton, GameObject PRButton)
    {
        if (fuelButton != null)
        {
            if (subStats.currentFuelUpgrade >= fuelUpgrades.Length - 1) { fuelButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "MAXED"; }
            else if (subStats.currentFuelUpgrade < fuelUpgrades.Length - 1) { fuelButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + fuelCost; }
            if (subStats.currentMoney < fuelCost || subStats.currentFuelUpgrade >= fuelUpgrades.Length - 1)
            {
                fuelButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                fuelButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= fuelCost)
            {
                fuelButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                fuelButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
            UpdatePips(fuelButton, subStats.currentFuelUpgrade);
        }
        if (engineButton != null)
        {
            if (subStats.currentEngineUpgrade >= engineUpgrades.Length - 1) { engineButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "MAXED"; }
            else if (subStats.currentEngineUpgrade < engineUpgrades.Length - 1) engineButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + engineCost;
            if (subStats.currentMoney < engineCost || subStats.currentEngineUpgrade >= engineUpgrades.Length - 1)
            {
                engineButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                engineButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= engineCost)
            {
                engineButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                engineButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
            UpdatePips(engineButton, subStats.currentEngineUpgrade);
        }
        if (hullButton != null)
        {
            if (subStats.currentHullUpgrade >= hullUpgrades.Length - 1) { hullButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "MAXED"; }
            else if (subStats.currentHullUpgrade < hullUpgrades.Length - 1) { hullButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + hullCost; }
            if (subStats.currentMoney < hullCost || subStats.currentHullUpgrade >= hullUpgrades.Length - 1)
            {
                hullButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                hullButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= hullCost)
            {
                hullButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                hullButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
            UpdatePips(hullButton, subStats.currentHullUpgrade);
        }
        if (propellerButton != null)
        {
            if (subStats.currentPropellerUpgrade >= propellerUpgrades.Length - 1) { propellerButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "MAXED"; }
            else if (subStats.currentPropellerUpgrade < propellerUpgrades.Length - 1) { propellerButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + propellerCost; }
            if (subStats.currentMoney < propellerCost || subStats.currentPropellerUpgrade >= propellerUpgrades.Length - 1)
            {
                propellerButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                propellerButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= propellerCost)
            {
                propellerButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                propellerButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
            UpdatePips(propellerButton, subStats.currentPropellerUpgrade);
        }
        if (PRButton != null)
        {
            if (subStats.currentPressureResistanceUpgrade >= pressureResistanceUpgrades.Length - 1) { PRButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "MAXED"; }
            else if (subStats.currentPressureResistanceUpgrade < pressureResistanceUpgrades.Length - 1) { PRButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + pressureResistanceCost; }
            if (subStats.currentMoney < pressureResistanceCost || subStats.currentPressureResistanceUpgrade >= pressureResistanceUpgrades.Length - 1)
            {
                PRButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                PRButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= pressureResistanceCost)
            {
                PRButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                PRButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
            UpdatePips(PRButton, subStats.currentPressureResistanceUpgrade);
        }
    }

    public void UpdatePips(GameObject currentUpgrade, int upgradeAmount)
    {
        for (int i = 0; i < pips.Length; i++)
        {
            pips[i] = currentUpgrade.transform.GetChild(4).GetChild(i);
            if (i <= upgradeAmount)
            {
                pips[i].GetComponent<Image>().color = new Color32(0, 220, 5, 225);
            }
            else
            {
                pips[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            }
        }
    }

    //sets all array stats to prevent doing it in hierarchy 
    public void SetGameplayStats()
    {
        //fuel upgrades
        fuelUpgrades[0] = 5;
        fuelUpgrades[1] = 10;
        fuelUpgrades[2] = 15;
        fuelUpgrades[3] = 20;
        fuelUpgrades[4] = 25;
        fuelUpgrades[5] = 30;
        fuelUpgrades[6] = 35;
        fuelUpgrades[7] = 40;

        //engine Upgrades
        engineUpgrades[0] = 4;
        engineUpgrades[1] = 8;
        engineUpgrades[2] = 12;
        engineUpgrades[3] = 16;
        engineUpgrades[4] = 20;
        engineUpgrades[5] = 24;
        engineUpgrades[6] = 28;
        engineUpgrades[7] = 32;

        //hull upgrades
        hullUpgrades[0] = 10;
        hullUpgrades[1] = 15;
        hullUpgrades[2] = 25;
        hullUpgrades[3] = 40;
        hullUpgrades[4] = 55;
        hullUpgrades[5] = 70;
        hullUpgrades[6] = 85;
        hullUpgrades[7] = 100;

        //propeller upgrades
        propellerUpgrades[0] = 5;
        propellerUpgrades[1] = 10;
        propellerUpgrades[2] = 15;
        propellerUpgrades[3] = 20;
        propellerUpgrades[4] = 25;
        propellerUpgrades[5] = 30;
        propellerUpgrades[6] = 35;
        propellerUpgrades[7] = 40;

        //pressure resistance upgrades
        pressureResistanceUpgrades[0] = 15;
        pressureResistanceUpgrades[1] = 20;
        pressureResistanceUpgrades[2] = 40;
        pressureResistanceUpgrades[3] = 60;
        pressureResistanceUpgrades[4] = 80;
        pressureResistanceUpgrades[5] = 100;
        pressureResistanceUpgrades[6] = 115;
        pressureResistanceUpgrades[7] = 135;

        //price per upgrades
        //prices for every upgrade are the same
        pricePerUpgrade[0] = 100;
        pricePerUpgrade[1] = 200;
        pricePerUpgrade[2] = 300;
        pricePerUpgrade[3] = 400;
        pricePerUpgrade[4] = 500;
        pricePerUpgrade[5] = 600;
        pricePerUpgrade[6] = 700;
        pricePerUpgrade[7] = 800;
    }
}