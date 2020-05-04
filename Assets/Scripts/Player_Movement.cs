using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    CharacterController characterController;
    private float speed = 6.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;
    private Vector3 moveDirection = Vector3.zero;

    private bool moving;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {



        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        //else
        //{
        //    moveDirection = Vector3.zero;
        //}
        //the players always getting effected by gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Moves the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
