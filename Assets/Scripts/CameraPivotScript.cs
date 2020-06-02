using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotScript : MonoBehaviour
{
    private CameraScript cameraScript;


    // Start is called before the first frame update
    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.localPosition = new Vector3(0f, 1f, -cameraScript.zoomDistance);
    }
}
