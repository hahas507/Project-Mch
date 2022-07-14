using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightUnit : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 0f;
    [SerializeField] float rotRecoverSpeed;
    float currentZRotation = 0f;
    float currentXRotation = 0f;

    Vector3 flightUnitDir = Vector3.down;

    //get set flightUnitDir
    public Vector3 FlightUnitDir
    {
        get { return flightUnitDir; }
        set { flightUnitDir = value; }
    }
    
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    void Update()
    {   
        Direction();
        Debug.Log(FlightUnitDir);
    }

    void Direction()
    {
        //마우스 x축
        float xInput = Input.GetAxis("Mouse X");
        //마우스 y축
        float yInput = Input.GetAxis("Mouse Y");

        float xRotation = xInput * rotationSpeed;
        float yRotation = yInput * rotationSpeed;
        
        if(Input.GetKey(KeyCode.LeftShift))
        {
            currentZRotation -= xRotation;
            currentXRotation -= yRotation;
        }
        else
        {
            currentZRotation = Mathf.Lerp(currentZRotation, 0, rotRecoverSpeed * Time.deltaTime);
            //currentXRotation은 기본이 90임. 정면으로 가는 방향을 보여주기 위해서 이를 조정해줌.
            currentXRotation = Mathf.Lerp(currentXRotation, 90, rotRecoverSpeed * Time.deltaTime);
            if(Mathf.Abs(currentZRotation) < 0.01f)
            {
                currentZRotation = 0;
            }

            if(Mathf.Abs(currentXRotation) < 0.01f)
            {
                currentXRotation = 0;
            }
        }
        currentZRotation = Mathf.Clamp(currentZRotation, -90f, 90f);
        currentXRotation = Mathf.Clamp(currentXRotation, 0f, 180f);
        //transform.rotation = Quaternion.Euler(currentXRotation, 0f, currentZRotation);
        transform.localRotation = Quaternion.Euler(currentXRotation, 0f, currentZRotation);
        flightUnitDir = transform.up;
    }
}
