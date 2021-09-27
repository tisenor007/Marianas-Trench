using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        //Debug.Log("THIS BUTTON WORKS");

        SceneManager.LoadScene("UpgradeScreen", LoadSceneMode.Single);
    }
    public void Options()
    {
        Debug.Log("THIS BUTTON WORKS");
    }
}
