using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    private float speed = 10.0f;
    public GameObject character;
    private float jumpHeight = 5;
    private bool IsJumping = false;

    void Start()
    {
        character = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.position += Vector3.back * speed * Time.deltaTime;
        }

        //will fix later

        //if (Input.GetKey(KeyCode.Space) && IsJumping == false)
        //{
            
        //    transform.position += Vector3.up * jumpHeight * Time.deltaTime;
        //    IsJumping = true;
        //}
        //else
        //{

        //}

    }
    void OnCollisionEnter(Collision Col)
    {
        if (Col.gameObject.name == "Floor")
        {
            IsJumping = false;
        }
    }
    void OnCollisionExit(Collision Col)
    {
        if (Col.gameObject.name == "Floor")
        {
            IsJumping = true;
        }
    }
}
