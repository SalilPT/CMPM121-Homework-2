using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    public Transform target;
    public Transform pivotObject;
    public float rotationAngle = 45f; // Slower than what was originally given in this script

    void Update()
    {
        transform.RotateAround(pivotObject.transform.position, Vector3.up, rotationAngle * Time.deltaTime);

        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);
    }
}