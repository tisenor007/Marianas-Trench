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
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == Global.gameScene)
        {
            upgradeManagerScript.earnedMoneyCanvas.SetActive(false);
            subStats.setInShopStatus(false);
            sub.SetActive(true);
            subCam.SetActive(true);
            subStatsCanvas.SetActive(true);
            upgradeManagerScript.InsufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(true);
            sub.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
        }
        if (sceneName == Global.upgradeScene)
        {
            upgradeManagerScript.earnedMoneyCanvas.SetActive(false);
            sub.SetActive(true);
            subCam.SetActive(true);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(true);
            upgradeManagerScript.InsufficientFundsMessage.SetActive(true);
            subStats.setInShopStatus(true);
            subCam.transform.position = subCamOriginPos;
            sub.transform.position = originSubPos;
            sub.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (sceneName == Global.titleScene)
        {
            upgradeManagerScript.earnedMoneyCanvas.SetActive(false);
            sub.SetActive(false);
            subCam.SetActive(false);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.InsufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(false);
            subStats.setInShopStatus(true);
        }
        if (sceneName == Global.gameOverScene)
        {
            upgradeManagerScript.earnedMoneyCanvas.SetActive(true);
            earnedMoney = subStats.GetCurrentDepth();
            upgradeManagerScript.earnedMoneyTxt.text = "Money Earned: $" + earnedMoney;
            sub.SetActive(false);
            subCam.SetActive(false);
            subStatsCanvas.SetActive(false);
            upgradeManagerScript.InsufficientFundsMessage.SetActive(false);
            upgradeManagerScript.moneyCanvas.SetActive(false);
            subStats.setInShopStatus(true);
        }
    }
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.health = subStats.health;

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

            subStats.health = data.health;

        }
    }
}
[Serializable]
class PlayerData
{
    public int health;
    public int currentFuelUpgrade;
    public int currentEngineUpgrade;
}
