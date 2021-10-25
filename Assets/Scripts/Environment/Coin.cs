using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private Collider boxCollider;
    private MeshRenderer rend;
    public float min, max;
    //public float offsetZ;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.GetComponent<BoxCollider>();
        rend = this.GetComponent<MeshRenderer>();
        //offsetZ = -10;
    }

    // Update is called once per frame
    void Update()
    {
    }

    // Upon collision with another GameObject
    private void OnTriggerEnter(Collider other)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (other.tag == "Boat")
        {
            print("Moving Prize");
            float x, y, z;
            x = UnityEngine.Random.Range(-4, 4);
            y = -5.47f;
            z = UnityEngine.Random.Range(min, max);
            this.transform.position = new Vector3(x, y, z);
        }
    }
}
