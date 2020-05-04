using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Collider : MonoBehaviour
{
    GameObject checkpoint;
    public bool triggered;

    void Start()
    {
        checkpoint = this.gameObject;
    }


    void Update()
    {

    }
    void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            triggered = true;
        }
    }
}
