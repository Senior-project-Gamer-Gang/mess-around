using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    public GameObject[] checkpointamount = new GameObject[10];
    public Vector3 curCPpos;
    void Start()
    {
        //adds all the checkpoints to the array 
        checkpointamount = GameObject.FindGameObjectsWithTag("CheckPoint");
        //goes through all the checkpoints and gets all the vector3s from the checkpoints and adds them to the array
    }

    void Update()
    {
        //keeps running through the for loop to see if the player collides with the checkpoints 
        if(checkpointamount != null)
        {
            foreach(GameObject Cp in checkpointamount)
            {
                if (Cp.GetComponent<Checkpoint_Collider>().triggered == true)
                {
                    curCPpos = Cp.transform.position;
                    Cp.GetComponent<Checkpoint_Collider>().triggered = false;
                }
            }
        }


    }
    public void RepoPlayer(GameObject players)
    {
        players.transform.position = curCPpos;
        players.GetComponent<CharacterController>().enabled = true;
    }


}
