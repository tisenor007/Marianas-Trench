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
    public RectTransform results;
    public Vector2 resultsMoveToPosition;
    public bool running = true;
    public bool deathCheck = true;

    void Awake()
    {

    }

    void Start()
    {
        
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
            if (results == null)
            {
                results = GameObject.Find("panel").GetComponent<RectTransform>();
                resultsMoveToPosition = new Vector2(results.localPosition.x, results.localPosition.y);
                results.transform.position = new Vector2(Screen.width / 2, Screen.height + 300);
            }

            if (outOfFuelObject == null)
            {
                outOfFuelObject = GameObject.Find("Out of Fuel");
                outOfFuelObject.SetActive(false);
                Debug.Log("FOUND");
            }

            if (deathCheck)
            {
                if (gameManager.subStats.GetIsDead() == true)
                {
                    deathCheck = false;
                    outOfFuelObject.SetActive(true);
                    LeanTween.scale(outOfFuelObject, Vector2.zero, 0f);
                    LeanTween.scale(outOfFuelObject, Vector2.one, 2f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
                    {
                        LeanTween.scale(outOfFuelObject, Vector2.zero, 0f).setDelay(2f);
                        LeanTween.move(results, resultsMoveToPosition, 1.0f);
                    });
                }
            }
            
            
        }
    }
}
