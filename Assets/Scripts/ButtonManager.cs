using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Upgrade Buttons")]
    public Text fuelButtonTxt;
    public Text engineButtonTxt;
    public Text hullButtonTxt;
    public Text propellerButtonTxt;
    public Text pressureResistanceTxt;

    protected GameObject upgradeManager;
    protected UpgradeManager upgradeManagerScript;

    protected GameObject gameManager;
    protected GameManager gameManagerScript;

    protected GameObject uiManager;
    protected UIManager uiManagerScript;

    public GameObject upgradeButton;
    public GameObject diveAgainButton;
    public GameObject dismissButton;

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
        upgradeManagerScript.updateButtons(fuelButtonTxt, engineButtonTxt, hullButtonTxt, propellerButtonTxt, pressureResistanceTxt);

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
            Debug.Log("FOUND dismiss BUTTON");
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
        uiManagerScript.tutorialPages[pageNum].SetActive(false);
        pageNum++;
        if (pageNum < uiManagerScript.tutorialPages.Length) { uiManagerScript.tutorialPages[pageNum].SetActive(true);  }
        //else { uiManagerScript.hideTutorial = true; }
        dismissButton = null;

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
        gameManagerScript.UImanager.inTutorial = true;
        gameManagerScript.UImanager.hideTutorial = false;
    }
    public void DiveAgain()
    {
        gameManagerScript.subStats.ResetStats();
        
        SceneManager.LoadScene(Global.gameSceneName, LoadSceneMode.Single);
        gameManagerScript.UImanager.inTutorial = false;
        gameManagerScript.UImanager.hideTutorial = true;
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
