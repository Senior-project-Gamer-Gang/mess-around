using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public float timeBetweenAttacks;
    public GameObject bullet;
    GameObject temp;
    public bool enemyshooter = false;
    public GameObject collider;
    float timer;
    public float wonderTime;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collider.GetComponent<EnemyTerritory>().interritory == false)
        {
            if (wonderTime > 0)
            {
                transform.Translate(Vector3.forward * .04f);
                wonderTime -= Time.deltaTime;
            }
            if (wonderTime <= 0)
            {
                wonderTime = Random.Range(5.0f, 15.0f);
                wonder();
            }
        }
        timer -= Time.deltaTime;

    }

    public void MoveToPlayer(Transform player)
    {
        transform.LookAt(player.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);

        //move towards player
        if (Vector3.Distance(transform.position, player.position) > attackRange)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        } 
    }
    public void shootAtPlayer(Transform player)
    {
        transform.LookAt(player.position);
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

}
