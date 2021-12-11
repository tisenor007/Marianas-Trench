using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    //VARIABLES
    [Header("Upgrade Buttons")]
    public GameObject fuelButton;
    public GameObject engineButtonTxt;
    public GameObject hullButton;
    public GameObject propellerButton;
    public GameObject pressureResistanceButton;
    protected GameObject upgradeManager;
    protected UpgradeManager upgradeManagerScript;
    protected GameObject gameManager;
    protected GameManager gameManagerScript;
    protected GameObject uiManager;
    protected UIManager uiManagerScript;
    protected int _pageNum = 0;
    public int pageNum {
        get { return _pageNum; }
        set { _pageNum = value; }
    }

    void Start()
    {
        pageNum = 0;
        uiManager = GameObject.Find("UIManager");
        upgradeManager = GameObject.FindWithTag("UpgradeManager");
        gameManager = GameObject.FindWithTag("GameManager");
        if (upgradeManager != null)
        {
            upgradeManagerScript = upgradeManager.GetComponent<UpgradeManager>();
        }
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }
        if (uiManager != null)
        {
            uiManagerScript = uiManager.GetComponent<UIManager>();
        }
    }

    void Update()
    {
        upgradeManagerScript.updateButtons(fuelButton, engineButtonTxt, hullButton, propellerButton, pressureResistanceButton);
    }

    //Saves game through a button that has this method assigned to it
    public void SaveGame()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.Save();
        }
    }

    //Loads game through a button that has this method assigned to it
    public void LoadGame()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.Load();
            GoToUpgradeScreen();
        }
    }

    //starts new game through a button that has this method assigned to it
    public void NewGame()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.NewGame();
            StartGamePlay();
        }
    }

    //Method used to get through tutorial pages
    public void FlipThroughPage()
    {
        if (pageNum == 0)
        {
            uiManagerScript.tutorialPages[0].SetActive(false);
            uiManagerScript.tutorialPages[1].SetActive(true);
            pageNum++;
        }
        else if (pageNum == 1)
        {
            uiManagerScript.tutorialPages[1].SetActive(false);
            uiManagerScript.tutorialPages[2].SetActive(true);
            pageNum++;
        }
        else if (pageNum == 2)
        {
            uiManagerScript.tutorialPages[2].SetActive(false);
            Time.timeScale = 1;
            gameManagerScript.inTutorial = false;
            gameManagerScript.hideTutorial = true;
        }
    }

    //Methods for upgrade buttons
    public void UpgradePressureResistance()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.IncreaseUpgrade(upgradeManagerScript.pressureResistanceCost, ref upgradeManagerScript.subStats.currentPressureResistanceUpgrade);
        }
    }

    public void UpgradeFuel()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.IncreaseUpgrade(upgradeManagerScript.fuelCost, ref upgradeManagerScript.subStats.currentFuelUpgrade);
        }
    }

    public void UpgradeEngine()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.IncreaseUpgrade(upgradeManagerScript.engineCost, ref upgradeManagerScript.subStats.currentEngineUpgrade);
        }
    }

    public void UpgradeHull()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.IncreaseUpgrade(upgradeManagerScript.hullCost, ref upgradeManagerScript.subStats.currentHullUpgrade);
        }
    }

    public void UpgradePropeller()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.IncreaseUpgrade(upgradeManagerScript.propellerCost, ref upgradeManagerScript.subStats.currentPropellerUpgrade);
        }
    }

    public void GoToUpgradeScreen()
    {
        gameManagerScript.subStats.ResetStats();
        SceneManager.LoadScene(Global.upgradeSceneName, LoadSceneMode.Single);
    }

    //Options Method for a possible future options Menu
    public void Options()
    {
        //Debug.Log("THIS BUTTON WORKS");
    }

    public void StartGamePlay()
    {
        gameManagerScript.subStats.ResetStats();
        SceneManager.LoadScene(Global.gameSceneName, LoadSceneMode.Single);
        gameManagerScript.inTutorial = true;
        gameManagerScript.hideTutorial = false;
    }

    public void DiveAgain()
    {
        gameManagerScript.subStats.ResetStats();
        SceneManager.LoadScene(Global.gameSceneName, LoadSceneMode.Single);
        gameManagerScript.inTutorial = false;
        gameManagerScript.hideTutorial = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(Global.titleSceneName, LoadSceneMode.Single);
    }

    public void GoToCreditsScreen()
    {
        SceneManager.LoadScene(Global.creditScreenName, LoadSceneMode.Single);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
