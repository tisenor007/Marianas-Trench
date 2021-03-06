using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    //VARIABLES
    public enum GameState
    {
        Title,
        GamePlay,
        GameOver,
        UpgradeScreen,
        WinScreen,
        CreditsScreen
    }
    public GameState gameState;
    public static GameManager gameManager;
    public GameObject subCam;
    public GameObject sub;
    public GameObject subStatsCanvas;
    public GameObject upgradeManager;
    public GameObject soundManager;
    public UIManager uiManager;
    public Submarine subStats;
    public int earnedMoney;
    public bool hideTutorial;
    public bool inTutorial;
    private UpgradeManager upgradeManagerScript;
    private Vector3 originSubPos;
    private Vector3 subCamOriginPos;
    private SoundManager _soundManagerScript;
    public SoundManager soundManagerScript
    {
        get { return _soundManagerScript; }
    }

    void Awake()
    {
        //Singleton
        if (gameManager == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(upgradeManager);
            DontDestroyOnLoad(sub);
            DontDestroyOnLoad(subCam);
            DontDestroyOnLoad(uiManager);
            DontDestroyOnLoad(soundManager);
            gameManager = this;
        }
        else if (gameManager != this)
        {
            Destroy(gameObject);
            Destroy(upgradeManager);
            Destroy(sub);
            Destroy(subCam);
            Destroy(uiManager);
            Destroy(soundManager);
        }
    }

    void Start()
    {
        subStats = sub.GetComponent<Submarine>();
        upgradeManagerScript = upgradeManager.GetComponent<UpgradeManager>();
        _soundManagerScript = soundManager.GetComponent<SoundManager>();
        originSubPos = sub.transform.position;
        subCamOriginPos = subCam.transform.position;
    }

    void Update()
    {
        earnedMoney = (int)(subStats.currentDepth * 1.5f) + (subStats.coinsCollected * subStats.coinWorth);
        if (subStats.isDead == false)
        {
            if (subStats.health <= 0)
            {
                subStats.hullIsBroken = true;
                subStats.addMoney(earnedMoney);
                //auto saving.....
                Save();
                subStats.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
            else if (subStats.fuel <= 0)
            {
                subStats.outOfFuel = true;
                subStats.addMoney(earnedMoney);
                //auto saving.....
                Save();
                subStats.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }
        }

        if (SceneManager.GetActiveScene().name == GameState.Title.ToString()) { gameState = GameState.Title; }
        if (SceneManager.GetActiveScene().name == GameState.GamePlay.ToString()) { gameState = GameState.GamePlay; }
        if (SceneManager.GetActiveScene().name == GameState.UpgradeScreen.ToString()) { gameState = GameState.UpgradeScreen; }
        if (SceneManager.GetActiveScene().name == GameState.GameOver.ToString()) { gameState = GameState.GameOver; }
        if (SceneManager.GetActiveScene().name == GameState.WinScreen.ToString()) { gameState = GameState.WinScreen; }
        if (SceneManager.GetActiveScene().name == GameState.CreditsScreen.ToString()) { gameState = GameState.CreditsScreen; }

        switch (gameState) 
        {
            case GameState.GamePlay:
                sub.SetActive(true);
                subStats.inShop = false;
                subCam.SetActive(true);
                subStatsCanvas.SetActive(true);
                upgradeManagerScript.moneyCanvas.SetActive(true);
                sub.GetComponent<SpriteRenderer>().enabled = true;
                sub.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                break;
            case GameState.UpgradeScreen:
                sub.SetActive(true);
                subCam.SetActive(false);
                subStatsCanvas.SetActive(false);
                upgradeManagerScript.moneyCanvas.SetActive(true);
                subStats.inShop = true;
                subCam.transform.position = subCamOriginPos;
                sub.GetComponent<SpriteRenderer>().enabled = false;
                break;
            case GameState.Title:
                sub.SetActive(false);
                subCam.SetActive(false);
                subStatsCanvas.SetActive(false);
                upgradeManagerScript.moneyCanvas.SetActive(false);
                subStats.inShop = true;
                break;
            case GameState.CreditsScreen:
                sub.SetActive(false);
                subCam.SetActive(false);
                subStatsCanvas.SetActive(false);
                upgradeManagerScript.moneyCanvas.SetActive(false);
                subStats.inShop = true;
                break;
            case GameState.WinScreen:
                //auto saving.....
                Save();
                sub.SetActive(false);
                subCam.SetActive(false);
                subStatsCanvas.SetActive(false);
                upgradeManagerScript.moneyCanvas.SetActive(false);
                subStats.inShop = true;
                break;
        }
    }

    public void NewGame()
    {
        subStats.clearGameStats();
    }

    //saves game stats
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.currentMoney = subStats.currentMoney;
        data.currentFuelUpgrade = subStats.currentFuelUpgrade;
        data.currentEngineUpgrade = subStats.currentEngineUpgrade;
        data.currentHullUpgrade = subStats.currentHullUpgrade;
        data.currentPropellerUpgrade = subStats.currentPropellerUpgrade;
        data.currentPressureResistanceUpgrade = subStats.currentPressureResistanceUpgrade;
        bf.Serialize(file, data);
        file.Close();
    }

    //saves game stats
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            subStats.currentMoney = data.currentMoney;
            subStats.currentFuelUpgrade = data.currentFuelUpgrade;
            subStats.currentEngineUpgrade = data.currentEngineUpgrade;
            subStats.currentHullUpgrade = data.currentHullUpgrade;
            subStats.currentPropellerUpgrade = data.currentPropellerUpgrade;
            subStats.currentPressureResistanceUpgrade = data.currentPressureResistanceUpgrade;
        }
        hideTutorial = true;
        inTutorial = false;
    }
}
[Serializable]
class PlayerData
{
    public int currentMoney;
    public int currentFuelUpgrade;
    public int currentEngineUpgrade;
    public int currentHullUpgrade;
    public int currentPropellerUpgrade;
    public int currentPressureResistanceUpgrade;
}
