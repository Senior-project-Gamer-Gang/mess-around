using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool lose_Hp;
    public bool IsFloor;
    public int Hp = 1;
    void OnTriggerEnter(Collider Col)
    {
        //checks if players collides with checkpoints 
        if (Col.gameObject.tag == "Player" && IsFloor == false)
        {
            //if player collides with checkpoint triggered is true 
            lose_Hp = true;
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
        //if this gameobjects hp is zero destroy gameobject
        if (this.Hp <= 0)
        {
            Destroy(this.gameObject);
        }
    }


}
