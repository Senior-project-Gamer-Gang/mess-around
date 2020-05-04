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
    public GameObject[] DeathObjs = new GameObject[20];
    //just a temp obj
    public GameObject obj;
    

    void Start()
    {
        //gets the CharacterController 
        characterController = GetComponent<CharacterController>();
        DeathObjs = GameObject.FindGameObjectsWithTag("Death");
    }

    void Update()
    {


        //you can move if your characters grounded 
        if (characterController.isGrounded)
        {
            //moves the player 
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection *= speed;
            //jump if you press space 
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        //the players always getting effected by gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Moves the controller
        characterController.Move(moveDirection * Time.deltaTime);







        //keeps running through the for loop to see if the player collides with the DeathObjs
        for (int i = 0; i < DeathObjs.Length; i++)
        {
            if (DeathObjs[i].GetComponent<Death>().Dead == true)
            {
                this.gameObject.transform.position = obj.GetComponent<CheckPoints>().checkpointpos[
                    obj.GetComponent<CheckPoints>().currentcheckpoint];

                DeathObjs[i].GetComponent<Death>().Dead = false;
            }
            //resets after it loops through the all the checkpoints 
            if (i >= DeathObjs.Length)
            {
                i = 0;
            }
        }



    }
}
