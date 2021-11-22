using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class UIManager : MonoBehaviour
{
    public GameManager gameManager;

    public GameObject outOfFuelObject;
    public GameObject hullBrokenObject;
    public RectTransform diveAgainButton;
    public RectTransform results;
    public Vector2 resultsMoveToPosition;
    public Vector2 diveAgainButtonMoveToPosition;
    public bool running = true;
    public bool deathCheck = true;
    //public bool hideTutorial;
    //public bool inTutorial;

    public Text depthText;
    public Text coinsText;
    public Text totalText;

    public GameObject[] tutorialPages;

    void Awake()
    {

    }

    void Start()
    {
        gameManager.inTutorial = true;
        tutorialPages = new GameObject[3];
    }
    
    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        if (sceneName == Global.titleSceneName)
        {
            if (running)
            {
                RectTransform title = GameObject.Find("Title").GetComponent<RectTransform>();
                Vector2 titleMoveToPosition = new Vector2(title.localPosition.x, title.localPosition.y);

                RectTransform newGame = GameObject.Find("NewgameButton").GetComponent<RectTransform>();
                Vector2 newGameMoveToPosition = new Vector2(newGame.localPosition.x, newGame.localPosition.y);
                RectTransform loadGame = GameObject.Find("LoadgameButton").GetComponent<RectTransform>();
                Vector2 loadGameMoveToPosition = new Vector2(loadGame.localPosition.x, loadGame.localPosition.y);
                RectTransform exitGame = GameObject.Find("ExitButton").GetComponent<RectTransform>();
                Vector2 exitGameMoveToPosition = new Vector2(exitGame.localPosition.x, exitGame.localPosition.y);

                Vector2 allStartPosition = new Vector2(Screen.width / 2, Screen.height + 300);

                title.transform.position = allStartPosition;
                newGame.transform.position = allStartPosition;
                loadGame.transform.position = allStartPosition;
                exitGame.transform.position = allStartPosition;

                LeanTween.move(title, titleMoveToPosition, 2.0f).setEase(LeanTweenType.easeSpring);
                LeanTween.move(newGame, newGameMoveToPosition, 2.0f).setEase(LeanTweenType.easeSpring).setDelay(2f);
                LeanTween.move(loadGame, loadGameMoveToPosition, 2.0f).setEase(LeanTweenType.easeSpring).setDelay(2f);
                LeanTween.move(exitGame, exitGameMoveToPosition, 2.0f).setEase(LeanTweenType.easeSpring).setDelay(2f);

                running = false;
            }

        }

        if (sceneName == Global.gameSceneName)
        {

            if (gameManager.inTutorial)
            {
                //tutorialPages[0] = GameObject.Find("welcomePanel");
                //tutorialPages[1] = GameObject.Find("tutorialPanel 1");
                //tutorialPages[2] = GameObject.Find("tutorialPanel 2");

                tutorialPages[0] = GameObject.Find("welcomePanel");
                tutorialPages[1] = GameObject.Find("tutorialPanel 1");
                tutorialPages[2] = GameObject.Find("tutorialPanel 2");


                if (tutorialPages[0] != null || tutorialPages[0] != null && tutorialPages[0].activeSelf == false) tutorialPages[0].SetActive(true);
                if (tutorialPages[1] != null || tutorialPages[1] != null && tutorialPages[1].activeSelf == true) tutorialPages[1].SetActive(false);
                if (tutorialPages[2] != null || tutorialPages[2] != null && tutorialPages[2].activeSelf == true) tutorialPages[2].SetActive(false);

                gameManager.inTutorial = false;

                if (tutorialPages[0] != null && tutorialPages[1] != null && tutorialPages[2] != null) { Time.timeScale = 0; }

                //tutorialPages[1].SetActive(false);
                //tutorialPages[2].SetActive(false);

                
            }

            if (gameManager.hideTutorial)
            {
                tutorialPages[0] = GameObject.Find("welcomePanel");
                tutorialPages[1] = GameObject.Find("tutorialPanel 1");
                tutorialPages[2] = GameObject.Find("tutorialPanel 2");

                //if (tutorialPages[0] == null) { tutorialPages[0] = GameObject.Find("welcomePanel"); }
                //if (tutorialPages[1] == null) { tutorialPages[1] = GameObject.Find("tutorialPanel 1"); }
                //if (tutorialPages[2] == null) { tutorialPages[2] = GameObject.Find("tutorialPanel 2"); }

                if (tutorialPages[0] != null || tutorialPages[0] != null && tutorialPages[0].activeSelf == true) tutorialPages[0].SetActive(false);
                if (tutorialPages[1] != null || tutorialPages[1] != null && tutorialPages[1].activeSelf == true) tutorialPages[1].SetActive(false);
                if (tutorialPages[2] != null || tutorialPages[2] != null && tutorialPages[2].activeSelf == true) tutorialPages[2].SetActive(false);

                Time.timeScale = 1;
                //tutorialPages[0].SetActive(false);
                //tutorialPages[1].SetActive(false);
                //tutorialPages[2].SetActive(false);

                //hideTutorial = false;
            }


            if (results == null)
            {
                results = GameObject.Find("resultsPanel").GetComponent<RectTransform>();
                resultsMoveToPosition = new Vector2(results.localPosition.x, results.localPosition.y);
                results.transform.position = new Vector2(Screen.width / 2, Screen.height * 1.5f);
                deathCheck = true;
            }

            if (diveAgainButton == null)
            {
                diveAgainButton = GameObject.Find("DiveAgainButton").GetComponent<RectTransform>();
                diveAgainButtonMoveToPosition = new Vector2(diveAgainButton.position.x, diveAgainButton.position.y);
                diveAgainButton.transform.position = new Vector2(Screen.width / 2, -100);
            }

            if (outOfFuelObject == null)
            {
                outOfFuelObject = GameObject.Find("Out of Fuel");
                outOfFuelObject.SetActive(false);
                Debug.Log("FOUND");
            }

            if (hullBrokenObject == null)
            {
                hullBrokenObject = GameObject.Find("Hull Broken");
                hullBrokenObject.SetActive(false);
                Debug.Log("FOUND");
            }


            if (deathCheck)
            {
                if (gameManager.subStats.isDead == true)
                {
                    
                    depthText = results.GetChild(2).GetChild(4).GetChild(0).GetComponent<Text>();
                    depthText.text = gameManager.subStats.currentDepth.ToString() + "ft = $" + (int)(gameManager.subStats.currentDepth * 1.5f);

                    coinsText = results.GetChild(2).GetChild(4).GetChild(1).GetComponent<Text>();
                    coinsText.text = gameManager.subStats.coinsCollected.ToString() + " = $" + (gameManager.subStats.coinsCollected * gameManager.subStats.coinWorth).ToString();

                    totalText = results.GetChild(2).GetChild(4).GetChild(3).GetComponent<Text>();
                    totalText.text = "$" + gameManager.earnedMoney.ToString(); //((gameManager.subStats.coinsCollected * gameManager.subStats.coinWorth) + (int)(gameManager.subStats.GetCurrentDepth() * 1.5f)).ToString();

                    if (gameManager.subStats.hullIsBroken == true)
                    {
                        hullBrokenObject.SetActive(true);
                        LeanTween.scale(hullBrokenObject, Vector2.zero, 0f);
                        LeanTween.scale(hullBrokenObject, Vector2.one, 2f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
                        {
                            LeanTween.scale(hullBrokenObject, Vector2.zero, 0f).setDelay(3f);
                            LeanTween.move(results, resultsMoveToPosition, 1.0f).setOnComplete(() => LeanTween.moveY(diveAgainButton, diveAgainButtonMoveToPosition.y, 0.5f));

                        });
                    }

                    if (gameManager.subStats.outOfFuel == true)
                    {
                        outOfFuelObject.SetActive(true);
                        LeanTween.scale(outOfFuelObject, Vector2.zero, 0f);
                        LeanTween.scale(outOfFuelObject, Vector2.one, 2f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
                        {
                            LeanTween.scale(outOfFuelObject, Vector2.zero, 0f).setDelay(3f);
                            LeanTween.move(results, resultsMoveToPosition, 1.0f).setOnComplete(() => LeanTween.moveY(diveAgainButton, diveAgainButtonMoveToPosition.y, 0.5f));

                        });
                    }
                    deathCheck = false;
                }
            }
            
            
            
        }
    }

    public void LoadTutorial()
    {
        tutorialPages[0].SetActive(true);
    }
}
