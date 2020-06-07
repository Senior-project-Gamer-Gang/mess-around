using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 2f;
    float attackRange = 3f;
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
    GameObject curplay;

    bool collidingwithplayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.GetComponent<EnemyTerritory>().interritory == false)
        {
            if (WaitTime <= 0 && wonderTime > 0)
            {
                anim.Play("run");
                anim.SetBool("run", true);
                transform.Translate(Vector3.forward * .04f);
                wonderTime -= Time.deltaTime;
            }
            if (wonderTime <= 0)
            {
                anim.SetBool("run", false);
                WaitTime = Random.Range(3.0f, 5.0f);
                wonderTime = Random.Range(5.0f, 15.0f);
                wonder();
            }
        }
        #region Timers
        WaitTime -= Time.deltaTime;
        timer -= Time.deltaTime;
        hittimer -= Time.deltaTime;
        #endregion
    }

    public void MoveToPlayer(Vector3 player, string playname)
    {
        anim.Play("run");
        anim.SetBool("run", true);
        if (playname != "")
            curplay = GameObject.Find(playname);
    
        transform.LookAt(player);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);


        //move towards player
        if (Vector3.Distance(transform.position, player) > attackRange)
        {

            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        if (Vector3.Distance(transform.position, player) <= attackRange && hittimer <= 0)
        {

            curplay.GetComponent<Player>().hp -= 1;
            hittimer = 2;
        }

    }
    public void shootAtPlayer(Vector3 player, string playname)
    {
        anim.Play("idle");
        anim.SetBool("run", false);
        transform.LookAt(player);
        //transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        if (timer <= 0)
        {
            temp = Instantiate(bullet, transform.position,
            Quaternion.identity);

            temp.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
            timer = .5f;
        }
    }
    void wonder()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag != "Player" && col.gameObject.tag != "Floor")
        {
            wonder();
        }
    }
}
