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
    private Vector3 originPos;
    private Scene currentScene;
    private string sceneName;

    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(submarine);
            DontDestroyOnLoad(subStatsCanvas);
            control = this;
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
        originPos = submarine.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName == "GamePlay")
        {
            subStats.inShop = false;
            submarine.SetActive(true);
            subStatsCanvas.SetActive(true);
            submarine.transform.localScale = new Vector3(1, 1, 1);
            submarine.transform.position = originPos;
        }
        else if (sceneName == "UpgradeScreen")
        {
            submarine.SetActive(true);
            subStatsCanvas.SetActive(false);
            subStats.inShop = true;
            submarine.transform.position = originPos;

            submarine.transform.localScale = new Vector3(4, 4, 4);
        }
        else if (sceneName == "Title" || sceneName == "GameOver")
        {
            submarine.SetActive(false);
            subStatsCanvas.SetActive(false);
            subStats.inShop = false;
        }

        //Debug.Log(sceneName);
        //Debug.Log(subStats.inShop);
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
