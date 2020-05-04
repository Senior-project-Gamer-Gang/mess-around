using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool Dead;

    void OnTriggerEnter(Collider Col)
    {
        //checks if players collides with checkpoints 
        if (Col.gameObject.tag == "Player")
        {
            //if player collides with checkpoint triggered is true 
            Dead = true;
        }
    }
}
