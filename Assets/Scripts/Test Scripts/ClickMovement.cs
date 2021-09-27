using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMovement : MonoBehaviour
{
    // Source: https://answers.unity.com/questions/1357724/how-do-i-move-an-object-by-1-unit-at-the-press-of.html

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            transform.position += Vector3.forward;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            transform.position += Vector3.back;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.position += Vector3.left;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.position += Vector3.right;
        }
    }

    // This is the alternative movement script, and isn't ideal because the objects in the scene move freely and not one singular space at a time.
}
