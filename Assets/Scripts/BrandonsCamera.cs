using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrandonsCamera : MonoBehaviour
{
    public float CameraSpeed = 120;
    public GameObject followOBJ;
    Vector3 followPOS;
    public float clampAngle = 80;
    public float inputSensitivity = 150;
    public GameObject CameraOBJ;
    GameObject[] PlayerOBJS;
    public float camdistToXPlayer;
    public float camdistToYPlayer;
    public float camdistToZPlayer;
    public float mouseX;
    public float mouseY;
    public float stickX;
    public float stickY;
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
        
    }
}
