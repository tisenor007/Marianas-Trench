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

    public GameObject upgradeButton;
    public GameObject diveAgainButton;
    public GameObject dismissButton;
    public GameObject dismissButton2;
    public GameObject dismissButton3;

    protected GameObject upgradeManager;
    protected UpgradeManager upgradeManagerScript;

    protected GameObject gameManager;
    protected GameManager gameManagerScript;

    protected GameObject uiManager;
    protected UIManager uiManagerScript;


    public int pageNum = 0;
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

        if (upgradeButton == null)
        {
            //resultsScreen = GameObject.Find("panel");
            
            upgradeButton = GameObject.Find("UpgradesMenuButton");
            if (upgradeButton != null)
            {
                upgradeButton.GetComponent<Button>().onClick.AddListener(GoToUpgradeScreen);
            }
            //Debug.Log("FOUND BUTTON");
        }
        if (diveAgainButton == null)
        {
            diveAgainButton = GameObject.Find("DiveAgainButton");
            if (diveAgainButton != null)
            {
                diveAgainButton.GetComponent<Button>().onClick.AddListener(DiveAgain);
            }
            //Debug.Log("FOUND dive BUTTON");
        }
        if (dismissButton == null)
        {
            dismissButton = GameObject.Find("DismissButton");
            if (dismissButton != null)
            {
                dismissButton.GetComponent<Button>().onClick.AddListener(OnDismissClicked1);
            }
            //Debug.Log("FOUND dismiss BUTTON");
        }
        if (dismissButton2 == null)
        {
            dismissButton = GameObject.Find("DismissButton2");
            if (dismissButton != null)
            {
                dismissButton.GetComponent<Button>().onClick.AddListener(OnDismissClicked2);
            }
            //Debug.Log("FOUND dismiss BUTTON 2");
        }
        if (dismissButton3 == null)
        {
            dismissButton = GameObject.Find("DismissButton3");
            if (dismissButton != null)
            {
                dismissButton.GetComponent<Button>().onClick.AddListener(OnDismissClicked3);
            }
            //Debug.Log("FOUND dismiss BUTTON 2");
        }
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

    public void OnDismissClicked1()
    {
        uiManagerScript.tutorialPages[0].SetActive(false);
        uiManagerScript.tutorialPages[1].SetActive(true);
        //uiManagerScript.tutorialPages[pageNum].SetActive(false);
        //pageNum++;
        //if (pageNum < uiManagerScript.tutorialPages.Length) { uiManagerScript.tutorialPages[pageNum].SetActive(true);  }
        //dismissButton = null;

    }
    public void OnDismissClicked2()
    {
        uiManagerScript.tutorialPages[1].SetActive(false);
        uiManagerScript.tutorialPages[2].SetActive(true);
        gameManagerScript.inTutorial = false;
        gameManagerScript.hideTutorial = true;
        Time.timeScale = 1;
        //uiManagerScript.tutorialPages[pageNum].SetActive(false);
        //pageNum++;
        //if (pageNum < uiManagerScript.tutorialPages.Length) { uiManagerScript.tutorialPages[pageNum].SetActive(true);  }
        //dismissButton = null;

    }
    public void OnDismissClicked3()
    {
        uiManagerScript.tutorialPages[2].SetActive(false);
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
        SceneManager.LoadScene(Global.upgradeSceneName, LoadSceneMode.Single);
    }
    public void Options()
    {
        Debug.Log("THIS BUTTON WORKS");
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
    public void ExitGame()
    {
        Application.Quit();
    }
}
