using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 2f;
    float attackRange = 2f;
    public int attackDamage = 1;
    public float timeBetweenAttacks;
    public GameObject bullet;
    GameObject temp;
    public bool enemyshooter = false;
    public GameObject collider;
    float timer, hittimer;
    public float wonderTime;

    Animator anim;
    float WaitTime;
    public GameObject curplay;
    GameObject gamemanager;
    bool IsAttacking;

    public enum enemystates { idle, wondering, attacking, dead }
    public enemystates EnemyStates;
    bool isDead;
    public Transform player;

    void Start()
    {
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");
        anim = GetComponent<Animator>();
        anim.Play("idle");
    }
    // Update is called once per frame
    void Update()
    {
        if (gamemanager.GetComponent<PauseScript>().isPaused == false)
        {
            if (EnemyStates == enemystates.idle)
            {
                if (wonderTime <= 0)
                {
                    anim.SetBool("run", false);
                    WaitTime = Random.Range(3.0f, 5.0f);
                    wonderTime = Random.Range(5.0f, 15.0f);
                    wonder();

                }
                if (WaitTime <= 0)
                {
                    EnemyStates = enemystates.wondering;
                }
            }

            if (EnemyStates == enemystates.wondering)
            {
                if (WaitTime <= 0 && wonderTime > 0)
                {
                    anim.SetBool("idle", false);
                    anim.Play("run");

                    transform.Translate(Vector3.forward * .04f);

                }
                if (wonderTime <= 0)
                {
                    EnemyStates = enemystates.idle;
                }

            }
            if (EnemyStates == enemystates.attacking)
            {
                if (isDead == false)
                {
                    //move towards player
                    if (Vector3.Distance(transform.position, player.position) < 15 && enemyshooter == false)
                    {
                        transform.LookAt(player);
                        if (Vector3.Distance(transform.position, player.position) > attackRange)
                        {
                            this.gameObject.transform.position += this.gameObject.transform.forward * speed * Time.deltaTime;
                            anim.Play("run");
                            anim.SetBool("punch", false);
                        }

                        if (Vector3.Distance(transform.position, player.position) <= attackRange && hittimer <= 0)
                        {
                            anim.Play("punch");
                            anim.SetBool("run", false);
                            anim.SetBool("punch", true);
                            curplay.GetComponent<Player>().hp -= 1;
                            hittimer = 2;
                        }
                    }
                    if (Vector3.Distance(transform.position, player.position) < 15 && enemyshooter == true)
                    {
                        transform.LookAt(player.position + player.GetComponent<CharacterController>().center);
                        if (timer <= 0)
                        {
                            temp = Instantiate(bullet, transform.position,
                            Quaternion.identity);

                            temp.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
                            timer = .5f;
                        }
                    }

                    if (Vector3.Distance(transform.position, player.position) > 15)
                    {
                        EnemyStates = enemystates.idle;
                    }
                }
                if(isDead == true)
                {
                    EnemyStates = enemystates.dead;
                }
            }
            if (EnemyStates == enemystates.dead)
            {
                anim.SetBool("run", false);
                anim.SetBool("punch", false);
                anim.Play("death");
                Destroy(this.gameObject, 1);
            }

            }
        #region Timers
        WaitTime -= Time.deltaTime;
        timer -= Time.deltaTime;
        hittimer -= Time.deltaTime;
        wonderTime -= Time.deltaTime;
        #endregion
    }

    void wonder()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Floor" && col.gameObject.tag != "Player_Hit")
        {
            wonder();
        }

        if (col.gameObject.tag == "Player_Hit")
        {
            isDead = true;
            EnemyStates = enemystates.dead;
            Destroy(col);

        }

    }
}
