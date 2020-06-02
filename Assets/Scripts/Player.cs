﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region topValues
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private bool moving;
    GameObject[] DeathObjs = new GameObject[50];

    public Text text;
    float hit_timer;
    bool playerDead;
    public GameObject[] players = new GameObject[2];
    Vector3[] playerpos = new Vector3[3];
    public float[] distbetweenobj = new float[2];
    public bool activeplayer;
    public float switchtime;

    //this is just a temp 
    public GameObject[] fireobj = new GameObject[2];
    //this is just for the fired gameobjs
    GameObject[] firedobj = new GameObject[2];

    //each player will ahev these varibles 
    public int hp;
    public float speed; //I made this variable public so the camera can see it -Jon
    public float jumpSpeed;
    public float gravityScale; //I changed gravity to gravity scale cause Physics.Gravity is Unity's built in gravity. We should use that instead.
    public float rotationSpeed; //How fast the player rotate towards the direction they're moving
    private Animator anim;
    public CheckPoints Cp;
    //*------------------------------*

    //big handmans varibles
    public float punch_time = .5f;
    float handSpeed = 8;
    GameObject hand;
    bool handinmotion;
    public bool handattack;
    //*-------------------------------------*

    //peashooters values --------
    float rof = .5f; //rof = rate of fire
    GameObject bullet;
    //*--------------------------*


    private GameObject gameManager; //The manager of course -Jon
    private GameObject camera; //The camera of course -Jon
    private Transform pivot; //what the player uses to determine camera's rotation -Jon
    #endregion

    void Start()
    {
        //gets the CharacterController 
        characterController = GetComponent<CharacterController>();

        anim = gameObject.GetComponent<Animator>();

        //gets the game manager + camera + pivot -Jon
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        pivot = GetComponentInChildren<PivotScript>().transform;


        List<GameObject> PlayerList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        PlayerList.RemoveAll(delegate (GameObject player)
        {
            return (Vector3.Distance(player.transform.position, transform.position) == 0);
        });
        players = PlayerList.ToArray();


        #region diffrentPlayerTypes
        if (this.gameObject.name == "Jeff")
        {
            speed = 12.0f;
            jumpSpeed = 10;
            hp = 3;
        }
        if (this.gameObject.name == "Shooter")
        {
            anim.Play("idle");
            speed = 8.0f;
            jumpSpeed = 12;
            hp = 4;
        }
        if (this.gameObject.name == "HandMan")
        {
            anim.Play("ide");
            speed = 4.0f;
            jumpSpeed = 8;
            hp = 6;
        }
        #endregion
    }

    //This is what the character controller uses for its collision detection.
    //It only runs this function if the controller is moving
    //It does not detect collisions when standing still -Jon
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "ComicPage")
        { //The player collects a comic page, it should add to the pages number displayed on screen along with removing the page object. -Jon
            gameManager.GetComponent<GameManagerScript>().pagesCollected++;
            Destroy(hit.gameObject);
        }
    }

    void Update()
    {
        DeathObjs = GameObject.FindGameObjectsWithTag("Death");

        distbetweenobj[0] = Vector3.Distance(players[0].transform.position, transform.position);
        distbetweenobj[1] = Vector3.Distance(players[1].transform.position, transform.position);

        if (this.hp <= 0)
        {
            Dead();
        }

        //switchs player
        if (activeplayer == true)
        {
            
            if (Input.GetKeyDown(KeyCode.E) && distbetweenobj[0] < 3 && switchtime < 0)
            {
                players[0].GetComponent<Player>().activeplayer = true;
                players[0].GetComponent<Player>().switchtime = 2;
                switchtime = 2;

                activeplayer = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && distbetweenobj[1] < 3 && switchtime < 0)
            {
                players[1].GetComponent<Player>().activeplayer = true;
                players[1].GetComponent<Player>().switchtime = 2;

                switchtime = 2;
                activeplayer = false;
            }
            //if(text.gameObject != null)
            // text.text = this.hp.ToString();

            //this.gameObject.GetComponentInChildren<Camera>().enabled = true; (Commented out for now -Jon)
            //Tells the camera to now focus on this active player -Jon
            camera.GetComponent<CameraScript>().player = gameObject;


            if (this.gameObject.name == "Jeff")
            {
                if (Input.GetMouseButtonDown(0) && gameManager.GetComponent<GameManagerScript>().pagesCollected >= 1)
                {
                    jeffscale(this.gameObject.transform.localScale);
                }
                if (Input.GetMouseButtonUp(0) && gameManager.GetComponent<GameManagerScript>().pagesCollected >= 1)
                {
                    jefforiganal(this.gameObject.transform.localScale);
                }

            }
            if (this.gameObject.name == "Shooter")
            {
                if (gameManager.GetComponent<GameManagerScript>().pagesCollected >= 1)
                {
                    #region Dontlook@this
                    if (players[0].gameObject.name == "HandMan" && distbetweenobj[0] <= 3)
                        jumpSpeed = 20;
                    if (players[1].gameObject.name == "HandMan" && distbetweenobj[1] <= 3)
                        jumpSpeed = 20;
                    if (distbetweenobj[1] >= 3)
                        jumpSpeed = 12;
                    #endregion
                }

                if (Input.GetMouseButtonDown(0) && rof <= 0)
                {
                    anim.Play("attack");
                    bullet = Instantiate(fireobj[1], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1,
                        this.gameObject.transform.position.z + 2), Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                    rof = .5f;
                }
            }
            if (this.gameObject.name == "HandMan")
            {

                if (Input.GetMouseButtonDown(0) && punch_time < 0)
                {
                    //plays attack
                    anim.Play("attack");
                    hand = Instantiate(fireobj[0], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1,
                        this.gameObject.transform.position.z), Quaternion.identity);
                    punch_time = .5f;
                    handinmotion = true;
                }
                if (handinmotion == true && hand.gameObject != null)
                {
                    hand.transform.position += transform.forward * Time.deltaTime * handSpeed;
                    Destroy(hand, punch_time);
                }
                if (punch_time <= 0)
                    handinmotion = false;
                //if this is true then big handman can break through walls 
                if (gameManager.GetComponent<GameManagerScript>().pagesCollected >= 1)
                    handattack = true;
            }
            #region playerHit
            //keeps running through the for loop to see if the player collides with the DeathObjs
            for (int i = 0; i < DeathObjs.Length; i++)
            {
                if (DeathObjs[i].GetComponent<Death>().lose_Hp == true)
                {

                    //this is for if you fall off the map
                    if (DeathObjs[i].GetComponent<Death>().IsFloor == true)
                    {
                        //repositions you at the most recent checkpoint
                        Dead();

                        hp -= 1;
                        //player loses hp
                        DeathObjs[i].GetComponent<Death>().lose_Hp = false;
                    }
                    //this is for every other type of damage 
                    if (DeathObjs[i].GetComponent<Death>().IsFloor == false && hit_timer < 0)
                    {
                        hp -= 1;
                        // so you cant keep taking damage from getting hit or iframes for the youngsters 
                        hit_timer = 2;

                        DeathObjs[i].GetComponent<Death>().lose_Hp = false;
                    }
                }
                //resets after it loops through the all the deathobjs
                if (i >= DeathObjs.Length)
                {
                    i = 0;
                }
            }

            #endregion
        }
        //this is so the handman wont keep walking after he's deactivated
        if (activeplayer == false && this.gameObject.name == "HandMan")
            anim.SetInteger("Walking", 0);
        if (activeplayer == false && this.gameObject.name == "Shooter")
            anim.SetBool("run", false);
        //this.gameObject.GetComponentInChildren<Camera>().enabled = false; (Commentated out for now -Jon)
        #region tiemrs
        if (switchtime >= -1)
            switchtime -= Time.deltaTime;
        if (punch_time >= -1)
            punch_time -= Time.deltaTime;
        if (rof >= -1)
            rof -= Time.deltaTime;
        if (hit_timer >= -1)
            hit_timer -= Time.deltaTime;
        #endregion
        #region PlayerMovement
        if (activeplayer == true)
        {
            //moves the player 
            float yStore = moveDirection.y; //Saving the y data before it gets manipulated -Jon
            moveDirection = (pivot.forward * Input.GetAxis("Vertical")) + (pivot.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * speed; //This is so moving diagonally is not faster than moving...well not diagonally -Jon
            moveDirection.y = yStore; //Applying the y data after moveDirection is manipulated -Jon
            //moveDirection *= speed;

            //you can jump if your characters grounded 
            if (characterController.isGrounded)
            {
                //jump if you press space 
                if (Input.GetButton("Jump"))
                {
                    moveDirection.y = jumpSpeed;
                    if (this.gameObject.name == "HandMan")
                    {
                        anim.Play("jump");
                    }
                    if (this.gameObject.name == "Shooter")
                    {
                        anim.Play("Jump");
                    }
                }
            }
            //Player's rotation
            Quaternion desiredRotation;
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                desiredRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z)); //Uses moveDirection to determine where the player would want to rotate towards -Jon
                if (this.gameObject.name == "HandMan")
                {
                    anim.SetInteger("Walking", 1);
                }
                if (this.gameObject.name == "Shooter")
                {
                    anim.SetBool("run", true);
                }
            }
            else { desiredRotation = transform.rotation; } //It will not try to rotate if player is not moving -Jon
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime); //Gradually rotates towards desiredRotation -Jon
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && this.gameObject.name == "HandMan")
            {
                anim.SetInteger("Walking", 0);
            }
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && this.gameObject.name == "Shooter")
            {
                anim.SetBool("run", false);
            }
            // Moves the controller
            characterController.Move(moveDirection * Time.deltaTime);


        }
        if (!characterController.isGrounded)
        {
            //the players always getting effected by gravity when off the ground
            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        }
        #endregion

        if (hp <= 0)
        {
            Dead();
        }
    }
    //this scales jeff down 
    void jeffscale(Vector3 scale)
    {
        scale -= new Vector3(0.5f, 0.5f, 0.5f);
        this.gameObject.transform.localScale = scale;
    }
    //this scales jeff back to the origanal scale 
    void jefforiganal(Vector3 scale)
    {
        scale = new Vector3(1f, 1f, 1f);
        this.gameObject.transform.localScale = scale;
    }
    public void Dead()
    {
        this.gameObject.transform.position = Cp.checkpointpos[Cp.currentcheckpoint];

        if(hp <= 0)
            hp = 5;
    }
}
