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
    public Button tutorialButton;
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

    public void OnTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }


    public void OnQuit()
    {
        Application.Quit();
    }
}
