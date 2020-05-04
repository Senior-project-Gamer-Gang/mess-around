using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Collider : MonoBehaviour
{
    public bool triggered;

    void OnTriggerEnter(Collider Col)
    {
        //checks if players collides with checkpoints 
        if (Col.gameObject.tag == "Player")
        {
            //if player collides with checkpoint triggered is true 
            triggered = true;
        }
    }
}
