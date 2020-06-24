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

        focalPoint.parent = null;

        rotationSpeed = cameraScript.rotationSpeed;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if(cameraScript.player != null)
        {
            focalPoint.position = cameraScript.player.transform.position;
            offset = new Vector3(0, -cameraScript.cameraHeight, cameraScript.zoomDistance); //offset is literally the zoomDistance from cameraScript 


            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            focalPoint.Rotate(0, horizontalRotation, 0, Space.World);
            float verticalRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
            focalPoint.Rotate(verticalRotation, 0, 0, Space.Self);

            if(focalPoint.rotation.eulerAngles.x > maxOrbitAngle && focalPoint.rotation.eulerAngles.x < 180f) //Maximum angle it can stay at
            {
                focalPoint.rotation = Quaternion.Euler(maxOrbitAngle, focalPoint.rotation.eulerAngles.y, 0f);
            }

            if (focalPoint.rotation.eulerAngles.x > 180f && focalPoint.rotation.eulerAngles.x < 360f + minOrbitAngle) //Minimum angle it can stay at
            {
                focalPoint.rotation = Quaternion.Euler(360f + minOrbitAngle, focalPoint.rotation.eulerAngles.y, 0f);
            }

            Quaternion focalPointRotation = Quaternion.Euler(focalPoint.eulerAngles.x, focalPoint.eulerAngles.y, 0f);

            transform.position = focalPoint.position - (focalPointRotation * offset);
            if (transform.position.y < focalPoint.position.y) //Make sure the camerapivot cannot go below the focal point
            {
                transform.position = new Vector3(transform.position.x, focalPoint.position.y, transform.position.z);
            }

            transform.LookAt(focalPoint.position);
        }
    }
}
