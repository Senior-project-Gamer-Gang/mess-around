using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swinging_Platforms : MonoBehaviour
{
    //if false it's == to Z if true == X
    public bool axis;
    public bool upanddown;
    //speed of the platforms 
    public float speed = 2.0f;
    //the starting direction 
    public float direction = 1;
    //starting position
    private Quaternion startPos;
    //how far moves left and right 
    public float dist = 1.5f;
    Quaternion a;
    Vector3 dir;


    public bool rotate;

    float startpoint;
    public float upamount;
    float endpoint;
    bool up = true, down;
    public float upspeed = 5.5f;
    void Start()
    {
        if (upanddown == true)
        {
            startpoint = this.gameObject.transform.position.y;
            endpoint = startpoint + upamount;
        }
        if(rotate == true)
        {
            startpoint = this.gameObject.transform.position.x;
            endpoint = startpoint + upamount;
        }
        
        //sets it == to the gameobjects transform rotation
        startPos = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (upanddown == false && rotate == false)
        {
            dir = this.transform.position;
            a = startPos;
            if (axis == true)
                a.x += direction * (dist * Mathf.Sin(Time.time * speed));

            if (axis == false)
                a.z += direction * (dist * Mathf.Sin(Time.time * speed));
            transform.rotation = a;
        }
        if(upanddown == true)
        {
            if(up == true)
            {
                this.gameObject.transform.Translate(new Vector3(0.0f, 1.0f * Time.deltaTime * upspeed, 0.0f), Space.World);
                if(this.gameObject.transform.position.y >= endpoint)
                {
                    up = false;
                    down = true;
                }
            }
            if (down == true)
            {
                this.gameObject.transform.Translate(new Vector3(0.0f, -1.0f * Time.deltaTime * upspeed, 0.0f), Space.World);
                if (this.gameObject.transform.position.y <= startpoint)
                {
                    down = false;
                    up = true;
                }
            }
        }
        //if(rotate == true)
        //{

        //    if (up == true)
        //    {
        //        this.gameObject.transform.Rotate(new Vector3(0.0f, 1.0f * Time.deltaTime * upspeed, 0.0f), Space.World);
        //        if (this.gameObject.transform.rotation.y >= endpoint)
        //        {
        //            up = false;
        //            down = true;
        //        }
        //    }
        //    if (down == true)
        //    {
        //        this.gameObject.transform.Rotate(new Vector3(0.0f, -1.0f * Time.deltaTime * upspeed, 0.0f), Space.World);
        //        if (this.gameObject.transform.rotation.y <= startpoint)
        //        {
        //            down = false;
        //            up = true;
        //        }
        //    }
        //}
    }
    
}
