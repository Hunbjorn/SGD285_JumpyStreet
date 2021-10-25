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

public class AlternativeBoatScript : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject player;
    public GameObject character;
    public Transform oldParent;
    public float thrust = 30f;
    public float speed = 1f;
    public Vector3 originalPos;
    public float jumpSpeed = 2f;

    [SerializeField]
    private Vector3 velocity;

    private bool moving;

    void Awake()
    {
        character = GameObject.Find("Character");
        player = GameObject.Find("ToxicFrog");
    }

    void Start()
    {
        originalPos = this.transform.position;
        gameObject.SetActive(true);
    }

    //Moves this GameObject 2 units a second in the forward direction
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void FixedUpdate()
    {
        if (moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }

    // Upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "LeftWall")
        {
            //print("Boat hit left wall");
            this.transform.position = originalPos;
        }

        if (other.tag == "Player")
        {
            oldParent = other.transform.parent; //store the original parent
            other.GetComponent<Collider>().transform.SetParent(transform);
            // print("Boat hit player");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        //Statement for handling getting off boats
        if (Input.GetKeyDown(KeyCode.J))
        {
            other.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "Player")
        {
            other.GetComponent<Collider>().transform.SetParent(oldParent);
            // print("Boat hit player");
        }
    }
}
