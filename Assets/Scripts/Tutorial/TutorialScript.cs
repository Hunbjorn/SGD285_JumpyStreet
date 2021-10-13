using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{
    // Text
    [Header("UI")]
    public Text tutorialText;
    public Button backButton;

    // Prefabs
    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;

    // String Properties
    private int currentText = 0;

    string[] dialogue = new string[] { "Hello!", "Welcome to the tutorial!", "Let's first start with moving around.", "Use the WASD keys to move.", "Good job!",
    "The end goal is to... well... reach the end goal!", "There will be obstacles in your path that need to be avoided.", "Hit the enemy to see what happens!", 
    "Don't want that to happen again.", "See that shiny coin over there?", "Try collecting it!", "Now that you understand the basics...", "Are you ready for the real deal?", 
    "For now, play around for as long as you would like!", "Once you are ready, return to the main menu." };

    // Audio
    [Header("Audio")]
    public AudioSource SpacebarSFX;

    // Start is called before the first frame update
    void Start()
    {
        tutorialText.text = dialogue[0];
    }
    
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            tutorialText.text = dialogue[currentText];
            SpacebarSFX.Play();
            currentText++;
            if(currentText >= dialogue.Length)
            {
                currentText = dialogue.Length - 1;
            }
        }
    }

    // Conditional Collisions
    public void InstantiateNewPlayer()
    {
        Instantiate(playerPrefab, new Vector3(0, 1, -4), Quaternion.identity);
    }

    public void EnemyHit()
    {
        tutorialText.text = "Ow!";
    }

    public void CoinCollected()
    {
        tutorialText.text = "Nice!";
    }

    // Buttons
    public void BackButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
