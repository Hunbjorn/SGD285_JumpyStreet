using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Source for movement: https://docs.unity3d.com/ScriptReference/Rigidbody.MovePosition.html

    // Movement Properties
    Rigidbody rb;
    public float speed = 5f;

    // Scoring Properties
    public int score = 0;
    public Text scoreText;

    public GameObject scoreCollider;

    // Start is called before the first frame update
    void Start()
    {
        // Movement
        rb = GetComponent<Rigidbody>();

        // Score
        scoreText.text = "Score: " + score.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + input * Time.deltaTime * speed);
    }

    // Scoring implementation
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Score")
        {
            score++;
            scoreCollider.SetActive(false);
            scoreText.text = "Score: " + score.ToString();
        }
    }

    // This is the primary movement script. I implemented the potential scoring mechanics here as well in the case we wanted to use it.
}
