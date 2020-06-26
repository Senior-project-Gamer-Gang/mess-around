using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint_Collider : MonoBehaviour
{
    public bool triggered;
    Animator anim;
    ParticleSystem PS;
    GameObject CheckPoint;
    void Start()
    {
        CheckPoint = GameObject.Find("CheckPoint");
        PS = transform.GetChild(0).GetComponent<ParticleSystem>();
        anim = GetComponent<Animator>();
        anim.Play("Idle");
        if(triggered == true)
        {
            PS.Play();
        }
    }
        void Update()
    {
        if(triggered == true && this.gameObject != CheckPoint.GetComponent<CheckPoints>().lastchecked)
        {
            PS.Play();
            anim.Play("Checkpoint");
        }
        if (triggered == false)
        {
            anim.Play("Idle");
        }
    }
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
