using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player;
    public GameObject pauseObject;
    public float zoomDistance; //How far the camera stays away from the player by default
    public float tooCloseDistance; //How close the player can be to the camera to cause the camera to back up.
    public float tooFarDistance; //How far the player can be before camera goes for its true max speed.
    public float maxFollowSpeed; //How fast the camera follows the player. This is its max speed (MAKE THIS FASTER THAN THE PLAYER)
    public float cameraAcceleration; //How much acceleration the camera has.
    public float cameraHeight; //How high the camera will position itself relative to the player

    private float cameraSpeed; //How fast the camera is moving.
    private float maxSpeed; //This max speed gets chosen by the camera when following player. 
    private bool isMoving;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isPaused = pauseObject.GetComponent<PauseScript>().isPaused;
        if (!isPaused || player != null) //As long as it is not paused or the player is not null, it will begin to do its thing
        {
            AdjustCameraOnXZAxis();
            AdjustCameraOnYAxis();
            CameraSpeed();

            transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
        }
    }

    private void AdjustCameraOnXZAxis()
    {
        isMoving = false; //Make sure this is false by default

        float distanceBetweenCameraAndPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceBetweenCameraAndPlayer > zoomDistance) //Begin to follow player if player moves outside the zoom distance
        {
            isMoving = true;
            
        }

        float distanceXZBetweenCameraAndPlayer = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));
        if (distanceXZBetweenCameraAndPlayer < tooCloseDistance) //If the player gets too close, begin to back up
        {
            isMoving = true;
            #region Logic For Moving Away
            if (transform.position.x > player.transform.position.x)
            {
                transform.position += new Vector3(cameraSpeed, 0, 0);
            }
            if (transform.position.x < player.transform.position.x)
            {
                transform.position += new Vector3(-cameraSpeed, 0, 0);
            }
            if (transform.position.z > player.transform.position.z)
            {
                transform.position += new Vector3(0, 0, cameraSpeed);
            }
            if (transform.position.z < player.transform.position.z)
            {
                transform.position += new Vector3(0, 0, -cameraSpeed);
            }
            #endregion
        }
        else
        { //Always do this unless the player gets too close. That's what the else statement is
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, cameraSpeed);
        }
    }

    private void AdjustCameraOnYAxis()
    {
        RaycastHit hit;
        Ray cameraRay = new Ray(transform.position, new Vector3(0, -1, 0));
        Physics.Raycast(cameraRay, out hit, Mathf.Infinity); //The camera is raycasting the ground right here.
        float distanceBetweenCameraHeightAndPlayerHeight = Mathf.Abs(transform.position.y) - Mathf.Abs(player.transform.position.y);
        if (hit.distance < cameraHeight || distanceBetweenCameraHeightAndPlayerHeight < cameraHeight)
        { //Move the camera up if they get too close to the ground or if the player is inside cameraHeight.

            //I actually want to make it so whenever the player is grounded that is only when it checks for height.
            //Currently, the camera will move up every time the player jumps
            //isMoving = true;
            transform.position += new Vector3(0, cameraSpeed, 0);
        }
        
    }

    private void CameraSpeed()
    {
        float distanceBetweenCameraAndPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (isMoving)
        {
            //this is unless the player is too far, then it should change its max speed to its own max speed rather than the player's.
            //reentering the zone makes it match the player's speed again.
            //so I need to figure out how far the camera can be from the player without choosing its own max speed
            maxSpeed = player.GetComponent<Player>().speed; //max speed should match the player's speed every time isMoving becomes true



            cameraSpeed += Time.deltaTime * cameraAcceleration;
            if(distanceBetweenCameraAndPlayer > tooFarDistance || maxSpeed > maxFollowSpeed)
            {
                maxSpeed = maxFollowSpeed;
            }
            if (cameraSpeed > maxFollowSpeed)
            {
                cameraSpeed = maxFollowSpeed;
            }
            
        }
        else
        {
            if(cameraSpeed > 0)
            {
                cameraSpeed -= Time.deltaTime * cameraAcceleration;
                if (distanceBetweenCameraAndPlayer < zoomDistance)
                {
                    cameraSpeed = 0;
                }
                if (cameraSpeed < .001)
                {
                    cameraSpeed = 0;
                }
            }
        }
    }
}
