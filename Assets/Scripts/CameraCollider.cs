using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    public float minDist = 1.0f;
    public float maxDist = 4.0f;
    public float smooth = 10;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float dist;
    void Awake()
    {
        dollyDir = transform.localPosition.normalized;
        dist = transform.localPosition.magnitude;
    }
    void Update()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(dollyDir * maxDist);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position, desiredCameraPos, out hit))
        {
            dist = Mathf.Clamp((hit.distance * 0.9f), minDist, maxDist);
        }else
        {
            dist = maxDist;
        }
        transform.localPosition = Vector3.Lerp(transform.localPosition, dollyDir * dist, Time.deltaTime * smooth);
    }
}
