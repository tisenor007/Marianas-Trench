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
    public static GameManager control;

    public GameObject subCam;
    public GameObject sub;
    public GameObject subStatsCanvas;
    public GameObject upgradeManager;
    private UpgradeManager upgradeManagerScript;
    private Submarine subStats;
    private Vector3 originSubPos;
    private Vector3 subCamOriginPos;
    private Scene currentScene;
    private string sceneName;
    private int earnedMoney;

    private GameObject deathUI;
    private GameObject deathMessage;
    private Text deathMessageTxt;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(upgradeManager);
            DontDestroyOnLoad(sub);
            DontDestroyOnLoad(subCam);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
            Destroy(upgradeManager);
            Destroy(sub);
            Destroy(subCam);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        subStats = sub.GetComponent<Submarine>();
        upgradeManagerScript = upgradeManager.GetComponent<UpgradeManager>();
        originSubPos = sub.transform.position;
        subCamOriginPos = subCam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        deathUI = GameObject.FindGameObjectWithTag("deathUI");
        deathMessage = GameObject.Find("deathUI/deathMessage");
        if (deathMessage != null) { deathMessageTxt = deathMessage.GetComponent<Text>(); }
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        if (subStats.GetIsDead() == false)
        {
            if (subStats.getHealth() <= 0) {subStats.addMoney(subStats.GetCurrentDepth()); deathMessageTxt.text = "Hull Armour broke!"; subStats.SetIsDead(true); }
            else if (subStats.getFuel() <= 0) { subStats.addMoney(subStats.GetCurrentDepth()); deathMessageTxt.text = "No Fuel Left!"; subStats.SetIsDead(true); }
            if (deathUI != null) { deathUI.SetActive(false); }
        }
        else if (subStats.GetIsDead() == true)
        {
            if (deathUI != null) { deathUI.SetActive(true); }
        }

        if (sceneName == Global.gameSceneName)
        {
            sub.SetActive(true);
            upgradeManagerScript.earnedMoneyCanvas.SetActive(false);
            subStats.setInShopStatus(false);
            subCam.SetActive(true);
            subStatsCanvas.SetActive(true);
            upgradeManagerScript.insufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(true);
            sub.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        if (sceneName == Global.upgradeSceneName)
        {
            upgradeManagerScript.earnedMoneyCanvas.SetActive(false);
            sub.SetActive(true);
            subCam.SetActive(true);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(true);
            upgradeManagerScript.insufficientFundsMessage.SetActive(true);
            subStats.setInShopStatus(true);
            subCam.transform.position = subCamOriginPos;
            sub.transform.position = originSubPos;
            sub.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (sceneName == Global.titleSceneName)
        {
            upgradeManagerScript.earnedMoneyCanvas.SetActive(false);
            sub.SetActive(false);
            subCam.SetActive(false);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.insufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(false);
            subStats.setInShopStatus(true);
        }
        if (sceneName == Global.gameOverSceneName)
        {
            Save();
            upgradeManagerScript.earnedMoneyCanvas.SetActive(true);
            earnedMoney = subStats.GetCurrentDepth();
            upgradeManagerScript.earnedMoneyTxt.text = "Money Earned: $" + earnedMoney;
            sub.SetActive(false);
            subCam.SetActive(false);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.insufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(false);
            subStats.setInShopStatus(true);
        }
        if (sceneName == Global.winSceneName)
        {
            Save();
            upgradeManagerScript.earnedMoneyCanvas.SetActive(true);
            earnedMoney = subStats.GetCurrentDepth();
            upgradeManagerScript.earnedMoneyTxt.text = "Money Earned: $" + earnedMoney;
            sub.SetActive(false);
            subCam.SetActive(false);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.insufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(false);
            subStats.setInShopStatus(true);
        }
    }
    public void NewGame()
    {
        subStats.clearGameStats();
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.currentMoney = subStats.GetMoney();
        data.currentFuelUpgrade = subStats.currentFuelUpgrade;
        data.currentEngineUpgrade = subStats.currentEngineUpgrade;
        data.currentHullUpgrade = subStats.currentHullUpgrade;
        data.currentPropellerUpgrade = subStats.currentPropellerUpgrade;

        bf.Serialize(file, data);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            subStats.SetMoney(data.currentMoney);
            subStats.currentFuelUpgrade = data.currentFuelUpgrade;
            subStats.currentEngineUpgrade = data.currentEngineUpgrade;
            subStats.currentHullUpgrade = data.currentHullUpgrade;
            subStats.currentPropellerUpgrade = data.currentPropellerUpgrade;

        }
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
}
