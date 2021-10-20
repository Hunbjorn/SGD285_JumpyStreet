using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // Buttons
    [Header("Buttons")]
    public Button startButton;
    public Button helpButton;
    public Button backButton;
    public Button quitButton;

    [Header("Audio")]
    public AudioSource mySFX;
    public AudioSource titleBGM;
    public AudioClip hoverSFX;
    public AudioClip selectSFX;

    // Game Objects
    [Header("Game Objects")]
    public GameObject helpPanel;
    public GameObject titlePanel;
    public GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        helpPanel.SetActive(false);
    }

    void Update()
    {
        if(Input.anyKey)
        {
            titlePanel.SetActive(false);
            titleBGM.Stop();
        }

        //Pause();
        //Resume();
    }

    //////////////////////////////////////////////////

    // Button SFX
    public void Hover()
    {
        mySFX.PlayOneShot(hoverSFX);
    }

    public void Select()
    {
        mySFX.PlayOneShot(selectSFX);
    }

    //////////////////////////////////////////////////

    // Pause Menu
    /*void Pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            isPaused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    void Resume()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            isPaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }*/

    //////////////////////////////////////////////////

    // Button Functions
    public void OnStart()
    {
        SceneManager.LoadScene("One");
    }

    public void OnHelp()
    {
        helpPanel.SetActive(true);
    }

    public void OnBack()
    {
        helpPanel.SetActive(false);
    }

    public void onTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
