using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoritoManScript : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("IdleDontoman");
    }


    public void Talking()
    {
        anim.SetBool("Talking", true);
    }
    public void TalkingNot()
    {
        anim.SetBool("Talking", false);
    }
}
