using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Wall" )
        {
            Destroy(Col.gameObject);
            Destroy(this.gameObject);
        }
    }
}
