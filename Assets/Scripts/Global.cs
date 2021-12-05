using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a class used to access variables that will not change and is known throughout the entire game
public class Global : MonoBehaviour
{
    //VARIABLES
    public static string gameOverSceneName = "GameOver";
    public static string titleSceneName = "Title";
    public static string gameSceneName = "GamePlay";
    public static string upgradeSceneName = "UpgradeScreen";
    public static string winSceneName = "WinScreen";
    public static string creditScreenName = "CreditsScreen";
    public static int mapLengthY = -148;
}
