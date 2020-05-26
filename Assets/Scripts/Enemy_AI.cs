using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    public float speed = 2f;
    public float attackRange = 1f;
    public int attackDamage = 1;
    public float timeBetweenAttacks;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void wonder()
    {
        //for later
    }

}
