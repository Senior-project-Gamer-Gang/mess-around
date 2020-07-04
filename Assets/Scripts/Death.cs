﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool lose_Hp;
    public bool IsFloor, IsCar;
    float timer = 2;

    void OnTriggerEnter(Collider Col)
    {
        //checks if players collides with checkpoints 
        if (Col.gameObject.tag == "Player" && IsFloor == false && IsCar == false)
        {
            Debug.Log("Bullet");
            lose_Hp = true;
        }
        if (Col.gameObject.tag == "Player" && IsCar == true)
        {

            if ( this.gameObject.GetComponent<Rigidbody>().IsSleeping() == false)
            {
                Debug.Log("Car");
                lose_Hp = true;
            }
        }
       
    }
    //this is for a box around the level
    //if you leave the box the player dies
    void OnTriggerExit(Collider Col)
    {
        if (Col.gameObject.tag == "Player" && IsFloor == true)
        {
            Debug.Log("Floor");
            lose_Hp = true;
        }
    }
    void Update()
    {
        timer -= Time.deltaTime;
        //for bullets
        if (timer <= 0 && IsFloor == false && IsCar == false)
        {
            Destroy(this.gameObject);
        }
    }


}
