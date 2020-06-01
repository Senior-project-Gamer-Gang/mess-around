using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging_Platforms : MonoBehaviour
{
    //if false it's == to Z if true == X
    public bool axis;
    //speed of the platforms 
    public float speed = 2.0f;
    //the starting direction 
    public float direction = 1;
    //starting position
    private Quaternion startPos;
    //how far moves left and right 
    public float dist = 1.5f;
    Quaternion a;
    void Start()
    {
        //sets it == to the gameobjects transform rotation
        startPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        a = startPos;
        if (axis == true)
            a.x += direction * (dist * Mathf.Sin(Time.time * speed));
       
        if(axis == false)
            a.z += direction * (dist * Mathf.Sin(Time.time * speed));
        transform.rotation = a;
    }
}
