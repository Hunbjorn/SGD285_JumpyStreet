//////////////////////////////////////////////////////
// Assignment/Lab/Project: SGD285-JumpyStreet
// Name: Julian Davis
// Section: 2021FA.SGD.285
// Instructor: Aurore Wold
// Date: 10/25/2021
//////////////////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SimpleCharacterController : MonoBehaviour
{
    private AudioSource audioSource;
    private AudioSource[] allAudioSources;
    private Transform[] allChildren;
    public Text scoreText;
    public Text highScoreText;

    public GameObject prize;
    public MeshRenderer coin;
    public GameObject player;
    public GameObject character;
    private bool godModeOn;
    private bool gameOver;
    public static int score;
    public static int highScore;
    private int lives;

    // --
    public GameObject gameOverPanel;
    public GameObject completedPanel;

    [Tooltip("Maximum slope the character can jump on")]
    [Range(5f, 60f)]
    public float slopeLimit = 45f;
    [Tooltip("Move speed in meters/second")]
    public float moveSpeed = 2f;
    [Tooltip("Turn speed in degrees/second, left (+) or right (-)")]
    public float turnSpeed = 300f;
    [Tooltip("Whether the character can jump")]
    public bool allowJump = false;
    [Tooltip("Upward speed to apply when jumping in meters/second")]
    public float jumpSpeed = 1f;

    public bool onBoat = false;

    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    public Text godMode;
    public Text info;

    public float jumpForce = 4f;
    public float jumpAmount = 2f;

    new private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Vector3 originalPos;

    float velocity;

    private void Start()
    {
        //StartAllAudio();
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalPos = this.transform.position;
        godMode.text = " ";
        godModeOn = false;
        gameOver = false;
        lives = 5;
        info.text = "Lives: " + lives;

        //--
        gameOverPanel.SetActive(false);
        completedPanel.SetActive(false);
    }

    void Update()
    {
        CheckGrounded();
        Move();
        Jump();
        GodMode();
    }

    private void FixedUpdate()
    {
        Turn();
    }

    public void GodMode()
    {
        if (Input.GetKey(KeyCode.P))
        {
            godMode.text = "GOD MODE";
            //Debug.Log("God mode enabled");
            godModeOn = true;

            player.tag = "Untagged";
            character.tag = "Untagged";
        }

        else
        {
            godMode.text = "";
            //Debug.Log("God mode disabled");
            godModeOn = false;

            player.tag = "Player";
            character.tag = "Player";
        }
    }

    // Checks whether the character is on the ground and updates <see cref="IsGrounded"/>
    private void CheckGrounded()
    {
        IsGrounded = false;
        float capsuleHeight = Mathf.Max(capsuleCollider.radius * 2f, capsuleCollider.height);
        Vector3 capsuleBottom = transform.TransformPoint(capsuleCollider.center - Vector3.up * capsuleHeight / 2f);
        float radius = transform.TransformVector(capsuleCollider.radius, 0f, 0f).magnitude;

        Ray ray = new Ray(capsuleBottom + transform.up * .016f, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, radius * 5f))
        {
            float normalAngle = Vector3.Angle(hit.normal, transform.up);
            if (normalAngle < slopeLimit)
            {
                float maxDist = radius / Mathf.Cos(Mathf.Deg2Rad * normalAngle) - radius + .016f;
                if (hit.distance < maxDist)
                {
                    IsGrounded = true;
                }
            }
        }
    }

    // Processes input actions and converts them into movement

    void Turn()
    {
        // Turning
        if (TurnInput != 0f)
        {
            float angle = Mathf.Clamp(TurnInput, -1f, 1f) * turnSpeed;
            transform.Rotate(Vector3.up, Time.fixedDeltaTime * angle);
        }
    }

    void Move()
    {
        // Movement
        Vector3 move = transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) *
        moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(transform.position + move);
    }

    void Jump()
    {
        // Jump
        if (JumpInput && allowJump && IsGrounded)
        {
            GetComponent<AudioSource>().Play();
            rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<AudioSource>().Play();
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            }
        }
    }

    // Upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "wall")
        {
            transform.position = originalPos;
        }

        if (other.tag == "Boat")
        {
            onBoat = true;
        }

        if(other.tag == "Vehicle") 
        {
            other.GetComponent<AudioSource>().Play();
            if (lives > 0)
            {
                lives--;
                transform.position = originalPos;
            }
            else
            {
                gameOver = true;
                //info.text = "GAME OVER: YOU LOST";
                info.text = "";
                StartCoroutine(DelayedEnd());

                //--
                gameOverPanel.SetActive(true);
            }
        }

        if (other.tag == "Water")
        {
            if (onBoat == false && godModeOn == false)
            {
                CheckLives();
            }
        }

        if (other.tag == "Prize")
        {
            other.GetComponent<AudioSource>().Play();
            score++;
            scoreText.text = score.ToString();
            if (highScore < score)
            {
                highScore = score;
                highScoreText.text = highScore.ToString();
            }
            allChildren = other.GetComponentsInChildren<Transform>();
            foreach (Transform child in allChildren)
            {
                child.gameObject.SetActive(false);
            }
            Destroy(other.GetComponent<Collider>());
            Destroy(other.GetComponent<MeshRenderer>());
        }

        if (other.tag == "Goal")
        {
            completedPanel.SetActive(true);
            //info.text = "YAY! YOU REACHED THE EXIT!";
            info.text = "";
            StartCoroutine(DelayedEnd());

            //--
            completedPanel.SetActive(true);
        }

    }

    public void CheckLives()
    {
        if (lives > 0)
        {
            lives--;
            transform.position = originalPos;
            info.text = "Lives: " + lives;
        }
        else if (lives <= 0)
        {
            gameOver = true;
            //info.text = "GAME OVER: YOU LOST";
            info.text = "";
            StartCoroutine(DelayedEnd());

            //--
            gameOverPanel.SetActive(true);
        }
        else 
        {
            print("lives counter error");
            StartCoroutine(DelayedEnd());
        }
    
    }

    private void OnTriggerExit(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "Boat")
        {
            onBoat = false;
        }
    }

    void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.enabled = false;
        }
    }


    void StartAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.enabled = true;
        }
    }

    IEnumerator DelayedEnd()
    {
        StopAllAudio();
        yield return new WaitForSeconds(5.0f);
        int y = SceneManager.GetActiveScene().buildIndex;
        if (y == 4 || gameOver == true)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
