using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text tutorialText;
    public AudioSource SpacebarSFX;
    string[] dialogue = new string[] { "Hello!", "Welcome to the tutorial!", "Let's first start with moving around.", "Use the WASD keys to move.", "Good job!",
        "The end goal is to... well... reach the end goal!", "There will be obstacles in your path that need to be avoided.", "Hit them and you have to restart!", "Play around for as long as you would like!", "Once you are ready, return to the main menu." };

    private int currentText = 0;

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
}
