using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeVehicle : MonoBehaviour
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

        else if (other.tag == "Vehicle")
        {
            int random = Random.Range(0, 1);
            if (random == 0)
            {
                Destroy(other);
            }
            else 
            {
                other.transform.position = originalPos;
            }
            print("Hit vehicle");
        }

        // Otherwise, it's hit the player, which kills the player
        else if (other.tag == "Player")
        {
            print("Vehicle hit player");
            other.transform.position = new Vector3(0f, -5.469f, -7.237f);
        }

        else
        {
            // print("Hit something else");
        }

    }

}
