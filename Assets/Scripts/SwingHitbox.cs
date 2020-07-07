using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingHitbox : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.parent = transform;
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.transform.parent = null;
        }
    }
}
