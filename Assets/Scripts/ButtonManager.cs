using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Upgrade Buttons")]
    public GameObject fuelButton;
    public GameObject engineButtonTxt;
    public GameObject hullButton;
    public GameObject propellerButton;
    public GameObject pressureResistanceButton;

   // public GameObject upgradeButton;
    //public GameObject diveAgainButton;
    //public GameObject dismissButton;
    //public GameObject dismissButton2;
    //public GameObject dismissButton3;

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

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        upgradeManagerScript.updateButtons(fuelButton, engineButtonTxt, hullButton, propellerButton, pressureResistanceButton);
    }
    public void SaveGame()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.Save();
        }
    }
    public void LoadGame()
    {
        if (gameManagerScript != null)
        {
            gameManagerScript.Load();
            GoToUpgradeScreen();
        }
    }
    public void NewGame()
    {
        if (gameManagerScript != null)
        {
            //uiManagerScript.inTutorial = true;
            //uiManagerScript.hideTutorial = false;
            
            gameManagerScript.NewGame();
            StartGamePlay();
        }
    }

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
    public void upgradePressureResistance()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.UpgradePressureResistance();
        }
    }
    public void upgradeFuel()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.UpgradeFuel();
        }
    }
    public void upgradeEngine()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.UpgradeEngine();
        }
    }
    public void upgradeHull()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.UpgradeHullArmour();
        }
    }
    public void upgradePropeller()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.UpgradePropeller();
        }
    }

    public void GoToUpgradeScreen()
    {
        //Debug.Log("THIS BUTTON WORKS");
        gameManagerScript.subStats.ResetStats();
        SceneManager.LoadScene(Global.upgradeSceneName, LoadSceneMode.Single);
    }
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
