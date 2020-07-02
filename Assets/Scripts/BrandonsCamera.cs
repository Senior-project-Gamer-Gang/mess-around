using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandonsCamera : MonoBehaviour
{
    public float CameraSpeed = 100;
    public GameObject followOBJ;
    Vector3 followPOS;
    public float clampAngle = 80;
    public float inputSensitivity = 50;
    public GameObject CameraOBJ;
    GameObject[] PlayerOBJS;
    public float camdistToXPlayer;
    public float camdistToYPlayer;
    public float camdistToZPlayer;
    public float mouseX;
    public float mouseY;
    public float finalInputX;
    public float finalInputZ;
    public float smoothX;
    public float smoothY;
    private float rotY;
    private float rotX;

    void Start()
    {
        PlayerOBJS = GameObject.FindGameObjectsWithTag("Player");
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < PlayerOBJS.Length; i++)
        {
            if(PlayerOBJS[i].GetComponent<Player>().activeplayer == true)
            {
                followOBJ = PlayerOBJS[i].transform.GetChild(0).gameObject;
            }
        }
        float inputX = Input.GetAxis("RightStickHorizontal");
        float inputZ = Input.GetAxis("RightStickVertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");
        finalInputX = inputX + mouseX;
        finalInputZ = inputZ + mouseY;

        rotY += finalInputX * inputSensitivity * Time.deltaTime;
        rotX += finalInputZ * inputSensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion localRotation = Quaternion.Euler(rotX,rotY,0);
        transform.rotation = localRotation;
    }
    void LateUpdate()
    {
        CameraUpdater();
    }
    void CameraUpdater()
    {
        Transform target = followOBJ.transform;
        float step = CameraSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
