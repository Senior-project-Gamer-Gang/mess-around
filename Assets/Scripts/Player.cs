using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    
    private Vector3 moveDirection = Vector3.zero;
    private bool moving;
    public GameObject[] DeathObjs = new GameObject[20];
    //just a temp obj
     GameObject obj;
    //these values are just test values, the values in the start
    //are for the playable characters
    private int hp = 6;
    private float speed = 6.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;

    public Text text;
    

    void Start()
    {
        //gets the CharacterController 
        characterController = GetComponent<CharacterController>();
        DeathObjs = GameObject.FindGameObjectsWithTag("Death");
        #region diffrentPlayerTypes
        if (this.gameObject.name == "Jeff")
        {
            speed = 6.0f;
            jumpSpeed = 4;
            gravity = 20;
            hp = 3;
        }
        if (this.gameObject.name == "Shooter")
        {
            speed = 4.0f;
            jumpSpeed = 6;
            gravity = 20;
            hp = 4;
        }
        if (this.gameObject.name == "HandMan")
        {
            speed = 2.0f;
            jumpSpeed = 2;
            gravity = 20;
            hp = 6;
        }
        #endregion
    }

    void Update()
    {
        text.text = hp.ToString();

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
                //sets the position of the player to the most recent checkpoint touched
                this.gameObject.transform.position = obj.GetComponent<CheckPoints>().checkpointpos[
                    obj.GetComponent<CheckPoints>().currentcheckpoint];

                //player dies
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
