using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region topValues
    CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private bool moving;
    GameObject[] DeathObjs = new GameObject[20];
    //just a temp obj
    public GameObject obj;
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
    private float speed;
    private float jumpSpeed;
    private float gravity = 20.0f;
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
    #endregion

    void Start()
    {
        //gets the CharacterController 
        characterController = GetComponent<CharacterController>();
        DeathObjs = GameObject.FindGameObjectsWithTag("Death");

        //gets the game manager + camera -Jon
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        camera = GameObject.FindGameObjectWithTag("MainCamera");


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
            jumpSpeed = 8;
            gravity = 20;
            hp = 3;
        }
        if (this.gameObject.name == "Shooter")
        {
            speed = 8.0f;
            jumpSpeed = 12;
            gravity = 20;
            hp = 4;
        }
        if (this.gameObject.name == "HandMan")
        {
            speed = 4.0f;
            jumpSpeed = 4;
            gravity = 20;
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
        distbetweenobj[0] = Vector3.Distance(players[0].transform.position, transform.position);
        distbetweenobj[1] = Vector3.Distance(players[1].transform.position, transform.position);

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
                    jumpSpeed = 20;

                if (Input.GetMouseButtonDown(0) && rof <= 0)
                {
                    bullet = Instantiate(fireobj[1], new Vector3 (this.gameObject.transform.position.x, this.gameObject.transform.position.y + 3, 
                        this.gameObject.transform.position.z + 2), Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                    rof = .5f;
                }
            }
            if (this.gameObject.name == "HandMan")
            {

                if (Input.GetMouseButtonDown(0) && punch_time < 0)
                {
                    hand = Instantiate(fireobj[0], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 2,
                        this.gameObject.transform.position.z + 3), Quaternion.identity);
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
        }





        //if (activeplayer == false)
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
        }
        #endregion
        #region playerHit
        //keeps running through the for loop to see if the player collides with the DeathObjs
        for (int i = 0; i < DeathObjs.Length; i++)
        {
            if (DeathObjs[i].GetComponent<Death>().lose_Hp == true && activeplayer == true)
            {
                //this is for if you fall off the map
                if (DeathObjs[i].GetComponent<Death>().IsFloor == true)
                {
                    //repositions you at the most recent checkpoint
                    this.gameObject.transform.position = obj.GetComponent<CheckPoints>().checkpointpos[
                    obj.GetComponent<CheckPoints>().currentcheckpoint];

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
            //resets after it loops through the all the checkpoints 
            if (i >= DeathObjs.Length)
            {
                i = 0;
            }
        }
        if (hp <= 0)
        {
            Dead();
        }
        #endregion
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
        //for later when we have lives and stuff
    }
}
