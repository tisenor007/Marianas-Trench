using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    //VARIABLES
    protected GameObject gameManager;
    protected GameManager gameManagerScript;
    public GameObject music;
   
    void Start()
    {
        //defines game manager
        gameManager = GameObject.FindWithTag("GameManager");
        if (gameManager != null)
        {
            gameManagerScript = gameManager.GetComponent<GameManager>();
        }
    }

    void Update()
    {
        //if any scene has and audiosource object called "BackgroundMusic" that music will play
        if (music == null)
        {
            music = GameObject.Find("BackgroundMusic");
            if (music != null)
            {
                music.GetComponent<AudioSource>().Play(0);
            }
        }
        //music loop
        if (music != null && music.GetComponent<AudioSource>().isPlaying == false)
        {
            music.GetComponent<AudioSource>().Play(0);
        }
    }

    //methods for playing pickup sounds, damage sounds, and upgrade sounds
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
