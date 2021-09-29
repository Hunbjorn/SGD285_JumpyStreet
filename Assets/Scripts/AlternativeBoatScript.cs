using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeBoatScript : MonoBehaviour
{

    public float speed = 2f;
    public Vector3 originalPos;

    public float thrust = 30f;

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

    // Upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "LeftWall")
        {
            print("Hit left wall");
            transform.position = originalPos;
        }

    }
}
