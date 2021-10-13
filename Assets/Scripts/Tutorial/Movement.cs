using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    // Source for movement: https://docs.unity3d.com/ScriptReference/Rigidbody.MovePosition.html

    Rigidbody rb;
    public float speed = 5f;
    //public Transform player;
    private TutorialScript tutorialManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tutorialManager = GameObject.FindGameObjectWithTag("TutorialManager").GetComponent<TutorialScript>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        rb.MovePosition(transform.position + input * Time.deltaTime * speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            // Instantiate(player, new Vector3(0, 1, -4), Quaternion.identity);
            tutorialManager.InstantiateNewPlayer();
            tutorialManager.EnemyHit();
            Destroy(gameObject);
        }

        if (other.tag == "Prize")
        {
            other.gameObject.SetActive(false);
            tutorialManager.CoinCollected();
        }

        if(other.tag == "Respawn")
        {
            tutorialManager.InstantiateNewPlayer();
            Destroy(gameObject);
        }
    }
}
