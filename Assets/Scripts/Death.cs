using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool lose_Hp;
    public bool IsFloor;
    public int Hp = 1;

    float timer = 2;
    void OnTriggerEnter(Collider Col)
    {
        //checks if players collides with checkpoints 
        if (Col.gameObject.tag == "Player" && IsFloor == false)
        { 
            lose_Hp = true;
            Destroy(this.gameObject);
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

        //if tiemr is less then zero destroy bullet
        if (timer <= 0 && IsFloor == false)
        {
            Destroy(this.gameObject);
        }
    }


}
