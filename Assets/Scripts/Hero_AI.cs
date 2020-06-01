using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero_AI : MonoBehaviour
{
    float wonderTime;
    float WaitTime;
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("idle");
    }
    void Update()
    {
        if (WaitTime <= 0 && wonderTime > 0)
        {
            anim.Play("run");
            anim.SetBool("run", true);
            transform.Translate(Vector3.forward * .04f);
            wonderTime -= Time.deltaTime;
        }
        if (wonderTime <= 0)
        {
            anim.SetBool("run", false);
            WaitTime = Random.Range(3.0f, 5.0f);
            wonderTime = Random.Range(5.0f, 15.0f);
            wonder();
        }
        
            
        
        WaitTime -= Time.deltaTime;
    }
    void wonder()
    {
        transform.eulerAngles = new Vector3(0, Random.Range(0, 360), 0);
    }
}
