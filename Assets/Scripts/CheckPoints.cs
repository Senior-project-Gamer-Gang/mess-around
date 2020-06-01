using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public GameObject player;
    public GameObject[] checkpointamount = new GameObject[10];
    public Vector3[] checkpointpos = new Vector3[10];
    public int currentcheckpoint;

    void Start()
    {
        //adds all the checkpoints to the array 
        checkpointamount = GameObject.FindGameObjectsWithTag("CheckPoint");
        //goes through all the checkpoints and gets all the vector3s from the checkpoints and adds them to the array
        for (int i = 0; i < checkpointamount.Length; i++)
        {
            checkpointpos[i] = checkpointamount[i].transform.position;
        }
        
        //the player position = the first checkpoint position 
        //this will change when we have save files
        player.transform.position = checkpointpos[0];
    }

    void Update()
    {
        //keeps running through the for loop to see if the player collides with the checkpoints 
        for (int i = 0; i < checkpointamount.Length; i++)
        {
            if (checkpointamount[i].GetComponent<Checkpoint_Collider>().triggered == true)
            {
                currentcheckpoint = i;
                checkpointamount[i].GetComponent<Checkpoint_Collider>().triggered = false;
            }
            //resets after it loops through the all the checkpoints 
            if (i >= checkpointamount.Length)
            {
                i = 0;
            }
        }
    }



}
