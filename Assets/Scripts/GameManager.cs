using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameManager : MonoBehaviour
{
    public static GameManager control;

    public Submarine subStats;
    public GameObject subCam;
    public GameObject sub;
    public GameObject subStatsCanvas;
    private Vector3 originSubPos;
    private Vector3 subCamOriginPos;
    private Scene currentScene;
    private string sceneName;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(sub);
            DontDestroyOnLoad(subCam);
            DontDestroyOnLoad(subStatsCanvas);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
            Destroy(sub);
            Destroy(subCam);
            Destroy(subStatsCanvas);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        originSubPos = sub.transform.position;
        subCamOriginPos = subCam.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "GamePlay")
        {
            subStats.setInShopStatus(false);
            sub.SetActive(true);
            subCam.SetActive(true);
            subStatsCanvas.SetActive(true);
            sub.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
            //subCam.transform.position = subCamOriginPos;
            //sub.transform.position = originSubPos;
        }
        if (sceneName == "UpgradeScreen")
        {
            sub.SetActive(true);
            subCam.SetActive(true);
            subStatsCanvas.SetActive(false);
            subStats.setInShopStatus(true);
            subCam.transform.position = subCamOriginPos;
            sub.transform.position = originSubPos;
            sub.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (sceneName == "Title" || sceneName == "GameOver")
        {
            sub.SetActive(false);
            subCam.SetActive(false);
            subStatsCanvas.SetActive(false);
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
}
