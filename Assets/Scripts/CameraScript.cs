using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public float zoomDistance; //How far the camera stays away from the player by default
    public float tooCloseDistance; //How close the player can be to the camera to cause the camera to back up.
    public float followPlayerSpeed; //How fast the camera follows the player.

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceBetweenCameraAndPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(distanceBetweenCameraAndPlayer > zoomDistance) //Begin to follow player if player moves outside the zoom distance
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followPlayerSpeed);
        }
        if(distanceBetweenCameraAndPlayer < tooCloseDistance) //If the player gets too close, begin to back up
        { //ISSUE: I need to get this to also check if the player is directly under the tooCloseDistance radius to make sure the player cannot run under the camera.
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, -followPlayerSpeed);
        }
        transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
    }
}
