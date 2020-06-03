using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivotScript : MonoBehaviour
{
    private CameraScript cameraScript;
    private GameObject targetPlayer;
    public Transform focalPoint;
    public float maxOrbitAngle;
    public float minOrbitAngle; //must be a negative number
    private Vector3 offset;
    private float rotationSpeed;


    // Start is called before the first frame update
    void Start()
    {
        cameraScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>();

        offset = focalPoint.position - new Vector3(0, -cameraScript.cameraHeight, cameraScript.zoomDistance); //offset is literally the zoomDistance based off 

        focalPoint.parent = null;

        rotationSpeed = 2f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if(cameraScript.player != null)
        {
            focalPoint.position = cameraScript.player.transform.position;
            

            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            focalPoint.Rotate(0, horizontalRotation, 0);
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            focalPoint.Rotate(verticalRotation, 0, 0);

            if(focalPoint.rotation.eulerAngles.x > maxOrbitAngle && focalPoint.rotation.eulerAngles.x < 180f) //Maximum angle it can stay at
            {
                focalPoint.rotation = Quaternion.Euler(maxOrbitAngle, 0f, 0f);
            }

            if (focalPoint.rotation.eulerAngles.x > 180f && focalPoint.rotation.eulerAngles.x < 360f + minOrbitAngle) //Minimum angle it can stay at
            {
                focalPoint.rotation = Quaternion.Euler(360f + minOrbitAngle, 0f, 0f);
            }

            Quaternion focalPointRotation = Quaternion.Euler(focalPoint.eulerAngles.x, focalPoint.eulerAngles.y, 0);

            transform.position = focalPoint.position - (focalPointRotation * offset);
            if (transform.position.y < focalPoint.position.y) //Make sure the camerapivot cannot go below the focal point
            {
                transform.position = new Vector3(transform.position.x, focalPoint.position.y + .5f, transform.position.z);
            }
            transform.LookAt(focalPoint.position);
        }
        
        
    }
}
