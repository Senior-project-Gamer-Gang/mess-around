using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject othertele;
    Vector3 tele;
    bool recentaly;
    // Start is called before the first frame update
    void Start()
    {
        tele = othertele.transform.position;
    }
    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            
            if (recentaly == false)
            {
                othertele.GetComponent<Teleporter>().recentaly = true;
                col.GetComponent<CharacterController>().enabled = false;
                col.transform.position = tele;
                col.GetComponent<CharacterController>().enabled = true;
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            recentaly = false;
        }

        }
}

