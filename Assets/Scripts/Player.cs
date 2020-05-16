using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    CharacterController characterController;

    private Vector3 moveDirection = Vector3.zero;
    private bool moving;
    GameObject[] DeathObjs = new GameObject[20];
    //just a temp obj
    public GameObject obj;
    //these values are just test values, the values in the start
    //are for the playable characters
    private int hp = 6;
    private float speed = 6.0f;
    private float jumpSpeed = 8.0f;
    private float gravity = 20.0f;

    public Text text;
    float hit_timer;

    bool playerDead;


    public GameObject[] players = new GameObject[2];
    Vector3[] playerpos = new Vector3[3];
    public float[] distbetweenobj = new float[2];
    public bool activeplayer;
    public float switchtime;
    void Start()
    {
        //gets the CharacterController 
        characterController = GetComponent<CharacterController>();
        DeathObjs = GameObject.FindGameObjectsWithTag("Death");

        
        List<GameObject> PlayerList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
        PlayerList.RemoveAll(delegate (GameObject player)
        {
            return (Vector3.Distance(player.transform.position, transform.position) == 0);
        });
        players = PlayerList.ToArray();


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
        distbetweenobj[0] = Vector3.Distance(players[0].transform.position, transform.position);
        distbetweenobj[1] = Vector3.Distance(players[1].transform.position, transform.position);
        text.text = hp.ToString();
        //switchs player
        if (activeplayer == true)
        {
            if (Input.GetKeyDown(KeyCode.E) && distbetweenobj[0] < 3 && switchtime < 0)
            {
                players[0].GetComponent<Player>().activeplayer = true;
                players[0].GetComponent<Player>().switchtime = 2;
                switchtime = 2;
                text.text = players[0].GetComponent<Player>().hp.ToString();
                activeplayer = false;
            }
            if (Input.GetKeyDown(KeyCode.E) && distbetweenobj[1] < 3 && switchtime < 0)
            {
                players[1].GetComponent<Player>().activeplayer = true;
                players[1].GetComponent<Player>().switchtime = 2;
                text.text = players[1].GetComponent<Player>().hp.ToString();
                switchtime = 2;
                activeplayer = false;
            }
            this.gameObject.GetComponentInChildren<Camera>().enabled = true;

        }
        if(activeplayer == false)
            this.gameObject.GetComponentInChildren<Camera>().enabled = false;
        #region tiemrs
        if (switchtime >= -1)
            switchtime -= Time.deltaTime;

        text.text = hp.ToString();
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
    public void Dead()
    {
        //for later when we have lives and stuff
    }
}
