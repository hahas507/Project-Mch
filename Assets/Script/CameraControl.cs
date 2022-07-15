using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float camSpeed;
    float currentXRotation = 0f;
    float currentYRotation = 0f;
    void Update()
    {
        MoveCam();
    }

    void MoveCam()
    {
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");
        float xRotation = xInput * camSpeed;
        float yRotation = yInput * camSpeed;
        currentXRotation += xRotation;
        currentYRotation -= yRotation;
        transform.localRotation = Quaternion.Euler(currentYRotation, currentXRotation, 0f);
    }
}
