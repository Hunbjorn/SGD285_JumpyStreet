using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public SimpleCharacterController charController;

    void Awake()
    {
        charController = GetComponent<SimpleCharacterController>();
    }

    public void Update()
    {
        // Get input values
        int vertical = Mathf.RoundToInt(Input.GetAxis("Vertical"));
        int horizontal = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        bool jump = Input.GetKey(KeyCode.Space);

        charController.ForwardInput = vertical;
        charController.TurnInput = horizontal;
        charController.JumpInput = jump;
    }
}
