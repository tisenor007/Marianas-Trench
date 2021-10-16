using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    protected GameObject upgradeManager;
    protected UpgradeManager upgradeManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        upgradeManager = GameObject.FindWithTag("UpgradeManager");
        if (upgradeManager != null)
        {
            upgradeManagerScript = upgradeManager.GetComponent<UpgradeManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    public void upgradeFuel()
    {
        if (upgradeManagerScript != null && upgradeManager != null)
        {
            upgradeManagerScript.UpgradeFuel();
            Debug.Log("1");
        }
    }

    public void GoToUpgradeScreen()
    {
        //Debug.Log("THIS BUTTON WORKS");

        SceneManager.LoadScene(Global.upgradeScene, LoadSceneMode.Single);
    }
    public void Options()
    {
        Debug.Log("THIS BUTTON WORKS");
    }
    public void StartGamePlay()
    {
        SceneManager.LoadScene(Global.gameScene, LoadSceneMode.Single);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(Global.titleScene, LoadSceneMode.Single);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
