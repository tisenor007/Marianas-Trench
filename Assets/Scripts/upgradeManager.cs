using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public Submarine subStats;
   // public GameObject earnedMoneyCanvas;
    public GameObject moneyCanvas;
    //public GameObject insufficientFundsMessage;
    //public Text earnedMoneyTxt;
    public GameManager gameManager;
    public Color enabledColour;
    public Color disabledColour;

    protected static int _upgradeAmount = 8;
    public static int upgradeAmount
    {
        get { return _upgradeAmount; }
        set { _upgradeAmount = value; }
    }
    //[Header("Set these for game play")]

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

    protected int fuelCost;
    protected int engineCost;
    protected int hullCost;
    protected int propellerCost;
    protected int pressureResistanceCost;

    //protected CanvasGroup insufficientFundsCanvas;

    private void Awake()
    {
        SetGameplayStats();
    }
    // Start is called before the first frame update
    void Start()
    {
        //SetGameplayStats();
        //insufficientFundsCanvas = insufficientFundsMessage.GetComponent<CanvasGroup>();
        //StartCoroutine(FadeCanvas(true));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(fuelUpgrades.Length);
        fuelCost = pricePerUpgrade[subStats.currentFuelUpgrade];
        engineCost = pricePerUpgrade[subStats.currentEngineUpgrade];
        hullCost = pricePerUpgrade[subStats.currentHullUpgrade];
        propellerCost = pricePerUpgrade[subStats.currentPropellerUpgrade];
        pressureResistanceCost = pricePerUpgrade[subStats.currentPressureResistanceUpgrade];

        //if (insufficientFundsCanvas.alpha <= 0)
        //{
        //    StopCoroutine(FadeCanvas(false));
        //}
        //else if (insufficientFundsCanvas.alpha > 0)
        //{
        //    StartCoroutine(FadeCanvas(true));
        //}
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
        //else if (subStats.currentMoney < fuelCost)
        //{
        //    insufficientFundsCanvas.alpha = 1;
        //    StartCoroutine(FadeCanvas(true));
        //}
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
        //else if (subStats.currentMoney < fuelCost)
        //{
        //    insufficientFundsCanvas.alpha = 1;
        //    StartCoroutine(FadeCanvas(true));
        //}
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
        //else if (subStats.currentMoney < engineCost)
        //{
        //    insufficientFundsCanvas.alpha = 1;
        //    StartCoroutine(FadeCanvas(true));
        //}
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
        //else if (subStats.currentMoney < hullCost)
        //{
        //    insufficientFundsCanvas.alpha = 1;
        //    StartCoroutine(FadeCanvas(true));
        //}
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
        //else if (subStats.currentMoney < propellerCost)
        //{
        //    insufficientFundsCanvas.alpha = 1;
        //    StartCoroutine(FadeCanvas(true));
        //}
    }
    public void updateButtons(GameObject fuelButton, GameObject engineButton, GameObject hullButton, GameObject propellerButton, GameObject PRButton)
    {
        if (fuelButton != null)
        {
            fuelButton.transform.GetChild(1).GetComponent<Text>().text = (subStats.currentFuelUpgrade + 1).ToString();
            fuelButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $"+fuelCost;
            if (subStats.currentMoney < fuelCost)
            {
                fuelButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                fuelButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= fuelCost)
            {
                fuelButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                fuelButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
        }
        if (engineButton != null)
        {
            engineButton.transform.GetChild(1).GetComponent<Text>().text = (subStats.currentEngineUpgrade + 1).ToString();
            engineButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + engineCost;
            if (subStats.currentMoney < engineCost){
                engineButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                engineButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= engineCost)
            {
                engineButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                engineButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
        }
        if (hullButton != null)
        {
            hullButton.transform.GetChild(1).GetComponent<Text>().text = (subStats.currentHullUpgrade + 1).ToString();
            hullButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + hullCost;
            if (subStats.currentMoney < hullCost)
            {
                hullButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                hullButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= hullCost)
            {
                hullButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                hullButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
        }
        if (propellerButton != null)
        {
            propellerButton.transform.GetChild(1).GetComponent<Text>().text = (subStats.currentPropellerUpgrade + 1).ToString();
            propellerButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + propellerCost;
            if (subStats.currentMoney < propellerCost)
            {
                propellerButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                propellerButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= propellerCost)
            {
                propellerButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                propellerButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
        }
        if (PRButton != null)
        {
            PRButton.transform.GetChild(1).GetComponent<Text>().text = (subStats.currentPressureResistanceUpgrade + 1).ToString();
            PRButton.transform.GetChild(2).transform.GetChild(0).GetComponent<Text>().text = "Buy $" + pressureResistanceCost;
            if (subStats.currentMoney < pressureResistanceCost)
            {
                PRButton.transform.GetChild(2).GetComponent<Button>().enabled = false;
                PRButton.transform.GetChild(2).GetComponent<Image>().color = disabledColour;
            }
            else if (subStats.currentMoney >= pressureResistanceCost)
            {
                PRButton.transform.GetChild(2).GetComponent<Button>().enabled = true;
                PRButton.transform.GetChild(2).GetComponent<Image>().color = enabledColour;
            }
        }
    }

    //public IEnumerator FadeCanvas(bool fadeOut)
    //{
    //    if (fadeOut == true)
    //    {
    //        for (float i = 1; i >= 0; i -= Time.deltaTime)
    //        {
    //            insufficientFundsCanvas.alpha = i;
    //            yield return null;
    //        }
    //    }
    //}
    //public int GetUpgradeAmount()
    //{
    //    return upgradeAmount;
    //}

    public void SetGameplayStats()
    {
        fuelUpgrades[0] = 5;
        fuelUpgrades[1] = 10;
        fuelUpgrades[2] = 15;
        fuelUpgrades[3] = 20;
        fuelUpgrades[4] = 25;
        fuelUpgrades[5] = 30;
        fuelUpgrades[6] = 35;
        fuelUpgrades[7] = 40;

        engineUpgrades[0] = 4;
        engineUpgrades[1] = 8;
        engineUpgrades[2] = 12;
        engineUpgrades[3] = 16;
        engineUpgrades[4] = 20;
        engineUpgrades[5] = 24;
        engineUpgrades[6] = 28;
        engineUpgrades[7] = 32;

        hullUpgrades[0] = 10;
        hullUpgrades[1] = 15;
        hullUpgrades[2] = 25;
        hullUpgrades[3] = 40;
        hullUpgrades[4] = 55;
        hullUpgrades[5] = 70;
        hullUpgrades[6] = 85;
        hullUpgrades[7] = 100;

        propellerUpgrades[0] = 5;
        propellerUpgrades[1] = 10;
        propellerUpgrades[2] = 15;
        propellerUpgrades[3] = 20;
        propellerUpgrades[4] = 25;
        propellerUpgrades[5] = 30;
        propellerUpgrades[6] = 35;
        propellerUpgrades[7] = 40;

        pressureResistanceUpgrades[0] = 15;
        pressureResistanceUpgrades[1] = 20;
        pressureResistanceUpgrades[2] = 40;
        pressureResistanceUpgrades[3] = 60;
        pressureResistanceUpgrades[4] = 80;
        pressureResistanceUpgrades[5] = 100;
        pressureResistanceUpgrades[6] = 115;
        pressureResistanceUpgrades[7] = 135;

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
