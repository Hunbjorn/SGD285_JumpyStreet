using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{
    public Text tutorialText;
    string[] dialogue = new string[] { "Hello!", "Welcome to the tutorial!", "Let's first start with moving around.", "Use the WASD keys to move.", "Good job!" };

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
            currentText++;
            if(currentText >= dialogue.Length)
            {
                currentText = dialogue.Length - 1;
            }
        }
    }
}
