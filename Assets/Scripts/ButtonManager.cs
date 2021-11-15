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

    public GameObject upgradeButton;
    public GameObject diveAgainButton;
    // Start is called before the first frame update
    void Start()
    {
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
            Debug.Log("FOUND BUTTON");
        }
        if (diveAgainButton == null)
        {
            diveAgainButton = GameObject.Find("DiveAgainButton");
            if (diveAgainButton != null)
            {
                diveAgainButton.GetComponent<Button>().onClick.AddListener(StartGamePlay);
            }
            Debug.Log("FOUND dive BUTTON");
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
            gameManagerScript.NewGame();
            StartGamePlay();
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
