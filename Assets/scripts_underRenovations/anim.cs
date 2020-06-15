using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    Animator anime;
    // Use this for initialization
    float hVal;
    float vVal;
    public BoxCollider shield;
    public BoxCollider sword;
    
    ////void Start()
    ////{
    ////    anime = GetComponent<Animator>();
       
    ////}

    ////// Update is called once per frame
    ////void Update()
    ////{
    ////    hVal = GetComponent<Player_moving>().forwardMovement;
    ////    vVal = GetComponent<Player_moving>().strafeMovement;

    ////    anime.SetFloat("VelX", vVal);
    ////    anime.SetFloat("VelY", hVal);


    ////    //    if (anime == null) return;
    ////    //;   var X = Input.GetAxis("Horizonal");
    ////    //       var Y = Input.GetAxis("Vertical");
    ////    //      Move(X,Y);
    ////    // }
    ////    //   private void Move(float x,float y)
    ////    //  {
    ////    //  anime.SetFloat("VelX",x);
    ////    //   anime.SetFloat("VelY", y);


    ////    //if (Input.GetKeyDown(KeyCode.Space))
    ////    //{
    ////    //    anime.SetBool("Jump", true);
    ////    //}
    ////    //if (Input.GetKeyUp(KeyCode.Space))
    ////    //{
    ////    //    anime.SetBool("Jump", false);
    ////    //}

    ////    //attack combos
    ////    if (Input.GetMouseButtonDown(0))
    ////    {
    ////        anime.SetBool("shield", true);
    ////        shield.enabled = true;
            
    ////    }

    ////    if (Input.GetMouseButtonUp(0))
    ////    {
    ////        anime.SetBool("shield", false);
    ////        shield.enabled = false;
    ////    }

    ////    if (Input.GetMouseButtonDown(1))
    ////    {
    ////        anime.SetBool("sword", true);
    ////      sword.enabled = true;
    ////    }

    ////    if (Input.GetMouseButtonUp(1))
    ////    {
    ////        anime.SetBool("sword", false);
    ////        sword.enabled =false;
    ////    }
    ////} 
}
   
  

