using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public Submarine subStats;
    public int currentFuelUpgrade = 0;
    public int currentEngineUpgrade = 0;
    public int currentHullUpgrade = 0;
    public int currentPropellerUpgrade = 0;
    public int[] fuelUpgrades = new int[upgradeAmount];
    public int[] engineUpgrades = new int[upgradeAmount];
    public int[] hullUpgrades = new int[upgradeAmount];
    public int[] propellerUpgrades = new int[upgradeAmount];

    public int[] pricePerUpgrade = new int[upgradeAmount];
    protected static int upgradeAmount = 8;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpgradeFuel()
    {
        //if (subStats.)
        currentFuelUpgrade = currentFuelUpgrade + 1;
        if (currentFuelUpgrade >= upgradeAmount -1)
        {
            currentFuelUpgrade = upgradeAmount-1;
        }
    }

    public void UpgradeEngine()
    {
        currentEngineUpgrade = currentEngineUpgrade + 1;
        if (currentEngineUpgrade >= upgradeAmount -1)
        {
            currentEngineUpgrade = upgradeAmount-1;
        }
    }
    public void UpgradeHullArmour()
    {
        currentHullUpgrade = currentHullUpgrade + 1;
        if (currentHullUpgrade >= upgradeAmount -1)
        {
            currentHullUpgrade = upgradeAmount-1;
        }
    }
    public void UpgradePropeller()
    {
        currentPropellerUpgrade = currentPropellerUpgrade + 1;
        if (currentPropellerUpgrade >= upgradeAmount -1)
        {
            currentPropellerUpgrade = upgradeAmount -1;
        }
    }

}
