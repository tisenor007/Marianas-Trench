using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    protected GameObject gameManager;
    protected GameManager gameManagerScript;

    public GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager");

        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        //if (sceneName == Global.titleSceneName)
        //{
        if (music == null)
        {
            music = GameObject.Find("BackgroundMusic");
            if (music != null)
            {
                music.GetComponent<AudioSource>().Play(0);
            }
        }
        if (music != null && music.GetComponent<AudioSource>().isPlaying == false)
        {
            music.GetComponent<AudioSource>().Play(0);
        }
        
        //}
    }

    public void PlayPickupSound(AudioSource pickup)
    {
        pickup.Play();
    }

    public void PlayDamageSound(AudioSource damage)
    {
        damage.Play();
    }

    public void PlayUpgradeSound(AudioSource upgrade)
    {
        upgrade.Play();
    }
}
