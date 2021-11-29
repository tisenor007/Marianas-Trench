using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

    public GameObject shadePanel;
    public GameObject[] tutorialPages;
    public Text subHull;
    public Image pressureGaugeImage;
    public Sprite[] pressureGauge = new Sprite[5];

    public GameObject fuelTank;

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

                tutorialPages[0] = GameObject.Find("welcomePanel");
                tutorialPages[1] = GameObject.Find("tutorialPanel 1");
                tutorialPages[2] = GameObject.Find("tutorialPanel 2");

                if (tutorialPages[0] != null || tutorialPages[0] != null && tutorialPages[0].activeSelf == false) tutorialPages[0].SetActive(true);
                if (tutorialPages[1] != null || tutorialPages[1] != null && tutorialPages[1].activeSelf == true) tutorialPages[1].SetActive(false);
                if (tutorialPages[2] != null || tutorialPages[2] != null && tutorialPages[2].activeSelf == true) tutorialPages[2].SetActive(false);

                gameManager.inTutorial = false;

                if (tutorialPages[0] != null && tutorialPages[1] != null && tutorialPages[2] != null) { Time.timeScale = 0; }
                
            }

            if (gameManager.hideTutorial)
            {
                tutorialPages[0] = GameObject.Find("welcomePanel");
                tutorialPages[1] = GameObject.Find("tutorialPanel 1");
                tutorialPages[2] = GameObject.Find("tutorialPanel 2");

                if (tutorialPages[0] != null || tutorialPages[0] != null && tutorialPages[0].activeSelf == true) tutorialPages[0].SetActive(false);
                if (tutorialPages[1] != null || tutorialPages[1] != null && tutorialPages[1].activeSelf == true) tutorialPages[1].SetActive(false);
                if (tutorialPages[2] != null || tutorialPages[2] != null && tutorialPages[2].activeSelf == true) tutorialPages[2].SetActive(false);
                
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

            }

            if (hullBrokenObject == null)
            {
                hullBrokenObject = GameObject.Find("Hull Broken");
                hullBrokenObject.SetActive(false);
                //Debug.Log("FOUND");
            }

            if (shadePanel == null)
            {
                shadePanel = GameObject.Find("ShadePanel");
                StartCoroutine(FadeImage(false));
                shadePanel.SetActive(false);
            }

            UpdateSubHUD();
            UpdatePressureGauge();
            UpdateFuelTank();

            if (deathCheck)
            {
                if (gameManager.subStats.isDead == true)
                {
                    
                    depthText = results.GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>();
                    depthText.text = gameManager.subStats.currentDepth.ToString() + "ft = $" + (int)(gameManager.subStats.currentDepth * 1.5f);

                    coinsText = results.GetChild(1).GetChild(3).GetChild(1).GetComponent<Text>();
                    coinsText.text = gameManager.subStats.coinsCollected.ToString() + " = $" + (gameManager.subStats.coinsCollected * gameManager.subStats.coinWorth).ToString();

                    totalText = results.GetChild(1).GetChild(3).GetChild(2).GetComponent<Text>();
                    totalText.text = "$" + gameManager.earnedMoney.ToString(); //((gameManager.subStats.coinsCollected * gameManager.subStats.coinWorth) + (int)(gameManager.subStats.GetCurrentDepth() * 1.5f)).ToString();

                    if (gameManager.subStats.hullIsBroken == true)
                    {
                        hullBrokenObject.SetActive(true);
                        LeanTween.scale(hullBrokenObject, Vector2.zero, 0f);
                        LeanTween.scale(hullBrokenObject, Vector2.one, 2f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
                        {
                            shadePanel.SetActive(true);
                            StartCoroutine(FadeImage(true));
                            LeanTween.scale(hullBrokenObject, Vector2.zero, 0f).setDelay(3f);
                            LeanTween.move(results, resultsMoveToPosition, 1.0f).setOnComplete(() => LeanTween.moveY(diveAgainButton, diveAgainButtonMoveToPosition.y, 0.5f).setOnComplete(() => Time.timeScale = 0));

                        });
                    }

                    if (gameManager.subStats.outOfFuel == true)
                    {
                        outOfFuelObject.SetActive(true);
                        LeanTween.scale(outOfFuelObject, Vector2.zero, 0f);
                        LeanTween.scale(outOfFuelObject, Vector2.one, 2f).setEase(LeanTweenType.easeSpring).setOnComplete(() =>
                        {
                            shadePanel.SetActive(true);
                            StartCoroutine(FadeImage(true));
                            LeanTween.scale(outOfFuelObject, Vector2.zero, 0f).setDelay(3f);
                            LeanTween.move(results, resultsMoveToPosition, 1.0f).setOnComplete(() => LeanTween.moveY(diveAgainButton, diveAgainButtonMoveToPosition.y, 0.5f).setOnComplete(() => Time.timeScale = 0));

                        });
                    }

                    deathCheck = false;
                }
                else if (gameManager.subStats.isDead == false && gameManager.hideTutorial == true) { Time.timeScale = 1; }
            }
           
            
            
        }
    }

    public void LoadTutorial()
    {
        tutorialPages[0].SetActive(true);
    }

    public void UpdateSubHUD()
    {
        fuelTank.GetComponent<Slider>().value = gameManager.subStats.fuel;
    }

    IEnumerator FadeImage(bool fadeAway)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 0; i <= 0.75f; i += Time.deltaTime)
            {
                // set color with i as alpha
                shadePanel.GetComponent<Image>().color = new Color(0, 0, 0, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            shadePanel.GetComponent<Image>().color = new Color(0, 0, 0, 0.0f);


        }
    }
    public void UpdatePressureGauge()
    {
        //pressureGaugeImage.sprite = pressureGauge[gameManager.subStats.pressureHitTime - 1];
        subHull.text = gameManager.subStats.health.ToString();

        if (gameManager.sub.transform.position.y < -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 4) && gameManager.sub.transform.position.y >= -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 3))
        {
            pressureGaugeImage.sprite = pressureGauge[3];
        }
        else if (gameManager.sub.transform.position.y < -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 3) && gameManager.sub.transform.position.y >= -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 2))
        {
            pressureGaugeImage.sprite = pressureGauge[2];
        }
        else if (gameManager.sub.transform.position.y < -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 2) && gameManager.sub.transform.position.y >= -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 1))
        {
            pressureGaugeImage.sprite = pressureGauge[1];
        }
        else if (gameManager.sub.transform.position.y < -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade]))
        {
            pressureGaugeImage.sprite = pressureGauge[0];
        }
        else if (gameManager.sub.transform.position.y > -(gameManager.upgradeManager.GetComponent<UpgradeManager>().pressureResistanceUpgrades[gameManager.subStats.currentPressureResistanceUpgrade] / 4))
        {
            pressureGaugeImage.sprite = pressureGauge[4];
        }
    }
}
