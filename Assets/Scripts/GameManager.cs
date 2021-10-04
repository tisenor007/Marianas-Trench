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
    public GameObject submarine;
    public GameObject subStatsCanvas;

    private Scene currentScene;
    private string sceneName;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(submarine);
            DontDestroyOnLoad(subStatsCanvas);
        }
        else if (control != this)
        {
            Destroy(gameObject);
            Destroy(submarine);
            Destroy(subStatsCanvas);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "GamePlay") 
        { 
            submarine.SetActive(true);
            subStatsCanvas.SetActive(true);
            subStats.inShop = false;
        }
        if (sceneName == "UpgradeScreen") 
        { 
            submarine.SetActive(true);
            subStatsCanvas.SetActive(true);
        }
        else { subStats.inShop = true; }

        Debug.Log(sceneName);
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
