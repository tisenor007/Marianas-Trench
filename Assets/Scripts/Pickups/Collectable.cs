using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectable : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    
    //all collectables spawn after the game starts, and the collectable manager is not in the same scene as the gameManager--->
    //---> So each collectable has to find the game manager after spawing.....
    protected void FindGameManager()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }
}

