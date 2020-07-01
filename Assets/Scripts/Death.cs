using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool lose_Hp;
    public bool IsFloor, IsCar;
    Vector3 lastpos, curpos;
    float timer = 2;
    void OnTriggerEnter(Collider Col)
    {
        //checks if players collides with checkpoints 
        if (Col.gameObject.tag == "Player" && IsFloor == false && IsCar == false)
        { 
            lose_Hp = true;
        }
        if (Col.gameObject.tag == "Player" && IsCar == true)
        {
            Debug.Log("Hit");
            if ( this.gameObject.GetComponent<Rigidbody>().IsSleeping() == false)
            {
                lose_Hp = true;
            }
        }
       
    }
    //this is for a box around the level
    //if you leave the box the player dies
    void OnTriggerExit(Collider other)
    {
        if (IsFloor == true)
        {
            lose_Hp = true;
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
        curpos = gameObject.transform.position;

        lastpos = curpos;
        //for bullets
        if (timer <= 0 && IsFloor == false && IsCar == false)
        {
            Destroy(this.gameObject);
        }
    }


}
