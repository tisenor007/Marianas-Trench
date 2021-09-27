using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToUpgradeScreen()
    {
        //Debug.Log("THIS BUTTON WORKS");

        SceneManager.LoadScene("UpgradeScreen", LoadSceneMode.Single);
    }
    public void Options()
    {
        Debug.Log("THIS BUTTON WORKS");
    }
    public void StartGamePlay()
    {
        SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }
}
