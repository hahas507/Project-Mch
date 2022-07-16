using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] float camSpeed;
    float currentXRotation = 0f;
    float currentYRotation = 0f;
    RaycastHit hit;
    [SerializeField] LayerMask layerMask;
    [SerializeField] float maxDistance;
    Vector3 hitpoint;
    Player player;

    public Vector3 Hitpoint
    {
        get { return hitpoint; }
        set { hitpoint = value; }
    }

    void Update()
    {
        MoveCam();
        AimRay();
    }

    void MoveCam()
    {
        float xInput = Input.GetAxis("Mouse X");
        float yInput = Input.GetAxis("Mouse Y");
        float xRotation = xInput * camSpeed;
        float yRotation = yInput * camSpeed;
        currentXRotation += xRotation;
        currentYRotation -= yRotation;
        //임시로 정했지만 추후 수정해야함.
        currentXRotation = Mathf.Clamp(currentXRotation, -30f, 30f);
        currentYRotation = Mathf.Clamp(currentYRotation, -11f, 30f);
        transform.localRotation = Quaternion.Euler(currentYRotation, currentXRotation, 0f);
    }

    void AimRay()
    {
        //screen to ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if ray doesn't hit anything, return
        if(Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            hitpoint = hit.point;
        }
        else
        {
            hitpoint = ray.origin + ray.direction * maxDistance;
        }
        //디버깅용으로 출력해보기 위해서 추가함.
        float distance = Vector3.Distance(transform.position, hitpoint);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.yellow);
    }
}
