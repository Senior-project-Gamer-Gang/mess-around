using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    #region topValues
    CharacterController characterController;
    public Vector3 moveDirection = Vector3.zero;
    private bool moving;
    public GameObject[] DeathObjs = new GameObject[50];

    Text HPtext;
    float hit_timer;
    bool playerDead;
    public GameObject[] players = new GameObject[2];
    Vector3[] playerpos = new Vector3[3];
    public float[] distbetweenobj = new float[2];
    public bool activeplayer;
    public float switchtime;
    public bool switcher;
    Scene curscene;

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
    public GameObject Cp;
    //*------------------------------*

    GameObject ShopKeeper;
    public bool inshop;



    //big handmans varibles
    public float punch_time = .5f;
    float handSpeed = 8;
    GameObject hand;
    bool handinmotion;
    public bool handattack;
    float NotGrounded;
    GameObject fallfist;
    //*-------------------------------------*

    //peashooters values --------
    float rof = .5f; //rof = rate of fire
    GameObject bullet;
    //*--------------------------*

    bool jeffroll;
    Rigidbody rb;
    bool isrigidbody;
    int currentskill = 0;
    GameObject healthbar;



    private Vector3 rotation;
    Transform MovingPlatform;
    private GameObject gameManager; //The manager of course -Jon
    private GameObject camera; //The camera of course -Jon
    private Transform pivot; //what the player uses to determine camera's rotation -Jon
    #endregion

    void Start()
    {
        
        HPtext = GameObject.FindGameObjectWithTag("HPTXT").GetComponent<Text>();
        ShopKeeper = GameObject.FindGameObjectWithTag("ShopKeeper");

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
            speed = 15.0f;
            jumpSpeed = 15;
            hp = 6;
            anim.Play("Jeff_idle");
        }
        if (this.gameObject.name == "Shooter")
        {
            anim.Play("idle");
            speed = 10;
            jumpSpeed = 17;
            hp = 7;
        }
        if (this.gameObject.name == "HandMan")
        {
            anim.Play("BHMidle");
            speed = 8;
            jumpSpeed = 15;
            hp = 8;
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
        //maybe for later i have no clue ATM
        //if(hit.gameObject.tag == "PlayerEvent")
        //{
        //    this.playerEvent = true;
        //}
    }

    void Update()
    {
        healthbar = GameObject.FindGameObjectWithTag("HealthBar");
        DeathObjs = GameObject.FindGameObjectsWithTag("Death");

        curscene = SceneManager.GetActiveScene();
        #region SwitchPlayer
        distbetweenobj[0] = Vector3.Distance(players[0].transform.position, transform.position);

        distbetweenobj[1] = Vector3.Distance(players[1].transform.position, transform.position);
        //switchs player
        if (activeplayer == true)
        {
            
            if (ShopKeeper != null)
            {
                if (Vector3.Distance(this.gameObject.transform.position, ShopKeeper.transform.position) <= 5
                    && Input.GetKeyDown(KeyCode.Q))
                {
                    inshop = true;
                    gameManager.GetComponent<GameManagerScript>().ShopOpen();
                }
            }
            if (Input.GetKeyDown(KeyCode.Q) && curscene.name == "Hub")
            {
                switcher = true;
            }
            if (inshop == false)
            {
                if (Input.GetKeyDown(KeyCode.E) && switchtime < 0
                && distbetweenobj[0] < distbetweenobj[1])
                {
                    players[0].GetComponent<Player>().activeplayer = true;
                    players[0].GetComponent<Player>().switchtime = 2;
                    switchtime = 2;
                    //makes the default skill punch/shoot
                    currentskill = 0;
                    activeplayer = false;
                    camera.GetComponent<CameraScript>().isFocused = false;
                }

                if (Input.GetKeyDown(KeyCode.E) && switchtime < 0
                    && distbetweenobj[1] < distbetweenobj[0])
                {
                    players[1].GetComponent<Player>().activeplayer = true;
                    players[1].GetComponent<Player>().switchtime = 2;
                    //makes the default skill punch/shoot
                    currentskill = 0;
                    switchtime = 2;
                    activeplayer = false;
                    camera.GetComponent<CameraScript>().isFocused = false;
                }
            }
            #endregion

            HPtext.text = this.hp.ToString();

            //this.gameObject.GetComponentInChildren<Camera>().enabled = true; (Commented out for now -Jon)
            //Tells the camera to now focus on this active player -Jon
            camera.GetComponent<CameraScript>().player = gameObject;

            #region PlayerMechs
            if (inshop == false && gameManager.GetComponent<PauseScript>().isPaused == false)
            {
                if (this.gameObject.name == "Jeff")
                {
                    if(healthbar != null)
                        healthbar.GetComponent<HealthBarScript>().HPChange(hp, 6);
                    #region Dontlook@thisneedtorewritelater1
                    if (players[0].gameObject.name == "Shooter" && distbetweenobj[0] <= 3)
                    {
                        jumpSpeed = 25;
                    }
                    if (players[0].gameObject.name == "Shooter" && distbetweenobj[0] >= 3)
                    {
                        jumpSpeed = 12;
                    }
                    if (players[1].gameObject.name == "Shooter" && distbetweenobj[1] <= 3)
                    {
                        jumpSpeed = 25;
                    }
                    if (players[1].gameObject.name == "Shooter" && distbetweenobj[1] >= 3)
                    {
                        jumpSpeed = 12;
                    }

                    #endregion

                    if (Input.GetMouseButtonDown(0) && currentskill == 0 && punch_time < 0)
                    {
                        
                        anim.SetBool("Jeff_ball", false);
                        anim.Play("Jeff_punch");
                        if (rb != null)
                            Destroy(rb);

                        hand = Instantiate(fireobj[0], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1,
                           this.gameObject.transform.position.z), Quaternion.identity);
                        punch_time = .5f;
                        handinmotion = true;
                        isrigidbody = false;
                        this.gameObject.GetComponent<SphereCollider>().enabled = false;
                        characterController.enabled = true;
                    }
                    if (Input.GetMouseButtonDown(0) && currentskill == 1)
                    {
                        isrigidbody = true;
                        this.gameObject.GetComponent<SphereCollider>().enabled = true;
                        this.gameObject.AddComponent<Rigidbody>();
                        rb = this.GetComponent<Rigidbody>();
                        anim.SetBool("Jeff_walk", false);
                        anim.SetBool("Jeff_ball", true);
                        characterController.enabled = false;

                    }
                    if (Input.GetMouseButtonDown(0) && currentskill == 2)
                    {
                        anim.SetBool("Jeff_walk", false);
                        anim.SetBool("Jeff_ball", false);
                        characterController.enabled = true;
                        jeffscale(this.gameObject.transform.localScale);
                    }


                    if (Input.GetMouseButtonUp(0) && currentskill == 1)
                    {
                        isrigidbody = false;
                        this.gameObject.GetComponent<SphereCollider>().enabled = false;
                        Destroy(rb);
                        anim.SetBool("Jeff_ball", false);
                        characterController.enabled = true;
                    }
                    if (Input.GetMouseButtonUp(0) && currentskill == 2)
                    {

                        jefforiganal(this.gameObject.transform.localScale);
                    }
                    if (activeplayer == true && isrigidbody == true && currentskill == 1)
                    {
                        float moveHorizontal = Input.GetAxis("Horizontal");
                        float moveVertical = Input.GetAxis("Vertical");

                        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

                        rb.AddForce(movement);
                    }
                }
                if (this.gameObject.name == "Shooter")
                {
                    if (healthbar != null)
                        healthbar.GetComponent<HealthBarScript>().HPChange(hp, 7);
                    #region Dontlook@thisneedtorewritelater2
                    if (players[0].gameObject.name == "HandMan" && distbetweenobj[0] <= 3)
                    {
                        jumpSpeed = 25;
                    }
                    if (players[0].gameObject.name == "HandMan" && distbetweenobj[0] >= 3)
                    {
                        jumpSpeed = 12;
                    }
                    if (players[1].gameObject.name == "HandMan" && distbetweenobj[1] <= 3)
                    {
                        jumpSpeed = 25;
                    }
                    if (players[1].gameObject.name == "HandMan" && distbetweenobj[1] >= 3)
                    {
                        jumpSpeed = 12;
                    }

                    #endregion


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
                    if (healthbar != null)
                        healthbar.GetComponent<HealthBarScript>().HPChange(hp, 8);
                    if (Input.GetMouseButtonDown(0) && punch_time < 0 && currentskill == 0)
                    {
                        //plays attack
                        anim.Play("attack");
                        hand = Instantiate(fireobj[0], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1,
                            this.gameObject.transform.position.z), Quaternion.identity);
                        punch_time = .5f;
                        handinmotion = true;
                    }
                    if (characterController.isGrounded == false)
                    {
                        NotGrounded += Time.deltaTime;
                    }

                    if (Input.GetMouseButtonDown(0) && NotGrounded >= 1 && currentskill == 1)
                    {
                        fallfist = Instantiate(fireobj[1], new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 2,
                            this.gameObject.transform.position.z), Quaternion.identity);

                    }
                    if (characterController.isGrounded == false)
                    {
                        if (fallfist != null)
                            fallfist.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 2,
                            this.gameObject.transform.position.z);
                        NotGrounded += Time.deltaTime;
                    }
                    if (characterController.isGrounded == true)
                    {
                        if (fallfist != null)
                            Destroy(fallfist);
                        NotGrounded = 0;
                    }
                }


                if (handinmotion == true && hand.gameObject != null)
                {
                    hand.transform.position += transform.forward * Time.deltaTime * handSpeed;
                    Destroy(hand, punch_time);
                }
                if (punch_time <= 0)
                    handinmotion = false;
            }
            #endregion
            #region playerHit
            if (DeathObjs != null)
            {
                foreach (GameObject badobj in DeathObjs)
                {
                    if (badobj.GetComponent<Death>().lose_Hp == true)
                    {
                        if (badobj.GetComponent<Death>().IsFloor == true)
                        {
                            hp -= 1;
                            characterController.enabled = false;
                            //repositions you at the most recent checkpoint
                            Cp.GetComponent<CheckPoints>().RepoPlayer(this.gameObject);

                            badobj.GetComponent<Death>().lose_Hp = false;
                        }
                        if (badobj.GetComponent<Death>().IsFloor == false)
                        {
                            Destroy(badobj);
                            if (hit_timer <= 0)
                            {
                                hp -= 1;
                                hit_timer = 2;
                            }
                            badobj.GetComponent<Death>().lose_Hp = false;
                        }
                    }
                }
            }

        }
        #endregion

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

        //this is so the handman wont keep walking after he's deactivated
        if (activeplayer == false && this.gameObject.name == "HandMan")
            anim.SetBool("walk", false);
        if (activeplayer == false && this.gameObject.name == "Shooter")
            anim.SetBool("run", false);
        if (activeplayer == false && this.gameObject.name == "Jeff")
            anim.SetBool("Jeff_walk", false);
        if (activeplayer == true && isrigidbody == false)
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
                currentskill = 0;
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
                currentskill = 1;
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
                currentskill = 2;
            //moves the player 
            float yStore = moveDirection.y; //Saving the y data before it gets manipulated -Jon
            moveDirection = (pivot.forward * Input.GetAxis("Vertical")) + (pivot.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * speed; //This is so moving diagonally is not faster than moving...well not diagonally -Jon
            moveDirection.y = yStore; //Applying the y data after moveDirection is manipulated -Jon
                                      //moveDirection *= speed;
            if (inshop == false)
            {
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
                        if (this.gameObject.name == "Jeff")
                        {
                            anim.Play("Juff_jump");
                        }
                    }
                    NotGrounded = 0f;
                }
                //Player's rotation
                Quaternion desiredRotation;
                if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
                {
                    desiredRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z)); //Uses moveDirection to determine where the player would want to rotate towards -Jon
                    if (this.gameObject.name == "HandMan" && this.characterController.isGrounded == true)
                    {
                        anim.SetBool("walk", true);
                    }
                    if (this.gameObject.name == "HandMan" && this.characterController.isGrounded == false)
                    {
                        anim.SetBool("walk", false);
                    }
                    if (this.gameObject.name == "Shooter" && this.characterController.isGrounded == true)
                    {
                        anim.SetBool("run", true);
                    }
                    if (this.gameObject.name == "Shooter" && characterController.isGrounded == false)
                    {
                        anim.SetBool("run", false);
                    }
                    if (this.gameObject.name == "Jeff" && characterController.isGrounded == true)
                    {
                        anim.SetBool("Jeff_walk", true);
                    }
                    if (this.gameObject.name == "Jeff" && characterController.isGrounded == false)
                    {
                        anim.SetBool("Jeff_walk", false);
                    }

                }
                else { desiredRotation = transform.rotation; } //It will not try to rotate if player is not moving -Jon
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime); //Gradually rotates towards desiredRotation -Jon
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && this.gameObject.name == "HandMan")
                {
                    anim.SetBool("walk", false);
                }
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && this.gameObject.name == "Shooter")
                {
                    anim.SetBool("run", false);
                }
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0 && this.gameObject.name == "Jeff")
                {
                    anim.SetBool("Jeff_walk", false);
                }
                // Moves the controller
                characterController.Move(moveDirection * Time.deltaTime);


            }
            
            if (characterController.isGrounded == false)
            {
                characterController.SimpleMove(Vector3.forward * 0);
                //the players always getting effected by gravity when off the ground
                moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            }
        }
        #endregion

        if (hp <= 0)
        {
            characterController.enabled = false;
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

        Cp.GetComponent<CheckPoints>().RepoPlayer(this.gameObject);

        if (hp <= 0)
        {
            if(this.gameObject.name == "Jeff")
            {
                hp = 6;
            }
            if (this.gameObject.name == "HandMan")
            {
                hp = 8;
            }
            if (this.gameObject.name == "Shooter")
            {
                hp = 7;
            }
        }
            
    }
}
