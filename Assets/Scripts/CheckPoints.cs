using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    GameObject player;
    public GameObject[] checkpointamount = new GameObject[10];
    public Vector3[] checkpointpos = new Vector3[10];

    public int currentcheckpoint;

    void Start()
    {
        checkpointamount = GameObject.FindGameObjectsWithTag("CheckPoint");

        for (int i = 0; i < checkpointamount.Length; i++)
        {
            checkpointpos[i] = checkpointamount[i].transform.position;
        }

        player = GameObject.FindGameObjectWithTag("Player");

        player.transform.position = checkpointpos[0];
    }

    void Update()
    {
      
    }


    

}
