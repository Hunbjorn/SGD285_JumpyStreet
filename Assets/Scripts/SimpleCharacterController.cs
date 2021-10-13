using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SimpleCharacterController : MonoBehaviour
{
    private AudioSource audioSource;
    private int score;
    private int highScore;
    public Text scoreText;
    public Text highScoreText;
    public GameObject prize;
    public MeshRenderer coin;

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

    public bool IsGrounded { get; private set; }
    public float ForwardInput { get; set; }
    public float TurnInput { get; set; }
    public bool JumpInput { get; set; }

    public float jumpForce = 4f;
    public float jumpAmount = 2f;

    new private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Vector3 originalPos;

    float velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalPos = this.transform.position;
    }

    void Update()
    {
        CheckGrounded();
        Move();
        Jump();
    }

    private void FixedUpdate()
    {
        //CheckGrounded();
        Turn();
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
            if (Input.GetKeyDown(KeyCode.J))
            {
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
            }

            //velocity += (gravity * gravityScale) * Time.fixedDeltaTime;
            //velocity += gravity * gravityScale * Time.deltaTime;
            //if (Input.GetKeyDown(KeyCode.J))
            //{
            //    velocity = jumpForce;
            //}

            //transform.Translate(new Vector3(0, velocity, 0) * Time.deltaTime);


            //rigidbody.AddForce(transform.forward * jumpSpeed, ForceMode.VelocityChange);
            //float vPos = rb.position.y + velocity * Time.fixedDeltaTime;
            //rb.MovePosition(new Vector3(rb.position.x, vPos, rb.position.z));
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

        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "Prize")
        {
            other.GetComponent<AudioSource>().Play();
            score++;
            scoreText.text = score.ToString();
            if(highScore < score) 
            {
                highScore = score;
            }
            Destroy(other.GetComponent<Collider>());
            Destroy(other.GetComponent<MeshRenderer>());

        }

        if (other.tag == "Goal") 
        {
            SceneManager.LoadScene("MainMenu");
        }

    }
}
