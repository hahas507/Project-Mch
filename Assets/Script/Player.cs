using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [Range(0, 100)] [SerializeField] float rotationSpeedXY;
    [Range(0, 100)] [SerializeField] float rotationSpeedZ;
    [Range(1, 5)] [SerializeField] float maxRotationSpeed;
    [SerializeField] float postureControl;
    [SerializeField] GameObject body;
    Vector3 screenSize;
    FlightUnit flightUnit;
    CameraControl camControl;
    
    Rigidbody rb;
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        flightUnit = GetComponentInChildren(typeof(FlightUnit)) as FlightUnit;
        camControl = GetComponentInChildren(typeof(CameraControl)) as CameraControl;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        MouseMinMax();
    }

    // Update is called once per frame
    void Update()
    {
        CenterOfMass();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //Driving force
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(flightUnit.FlightUnitDir * speed);
        }
        Vector3 mousePosition = Input.mousePosition;
        float curMousePosX = Mathf.InverseLerp(0, screenSize.x, mousePosition.x);
        float curMousePosY = Mathf.InverseLerp(0, screenSize.y, mousePosition.y);
        Vector3 curMousePos = new Vector3(-curMousePosY, curMousePosX, 0f);
        Vector3 direction = ((curMousePos - (new Vector3(-1f, 1f, 0f) * 0.5f)) * 20f);
        Debug.Log(direction);
        if(Mathf.Abs(direction.x) < 3f)
        {            
            direction.x = 0f;
        }            
        if(Mathf.Abs(direction.y) < 3f)
        {
            direction.y = 0f;
        }
        transform.Rotate(direction * rotationSpeedXY * Time.deltaTime);
        
        if(Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(Vector3.forward * rotationSpeedZ * Time.deltaTime);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            transform.Rotate(Vector3.back * rotationSpeedZ * Time.deltaTime);
        }
        /* 
        rb.AddRelativeTorque(direction * rotationSpeedXY, ForceMode.VelocityChange);
        if(rb.angularVelocity.magnitude > 1f)
        {
            //clamp rotation speed
            rb.angularVelocity = new Vector3(Mathf.Clamp(rb.angularVelocity.x, -maxRotationSpeed, maxRotationSpeed),
                                            Mathf.Clamp(rb.angularVelocity.y, -maxRotationSpeed, maxRotationSpeed),
                                            Mathf.Clamp(rb.angularVelocity.z, -maxRotationSpeed, maxRotationSpeed));
        }
        if(Mathf.Abs(direction.x) < 0.5f && Mathf.Abs(direction.y) < 0.5f)
        {
            rb.angularVelocity -= new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, 0f) * postureControl * Time.deltaTime;
            if(rb.angularVelocity.magnitude < 0.1f)
            {
                rb.angularVelocity = Vector3.zero;
            }
        } */
        
        //기존 코드
        /* if(direction.magnitude > 0.5f)
        {
            rb.AddRelativeTorque(direction * rotationSpeedXY, ForceMode.VelocityChange);
            if(rb.angularVelocity.magnitude > 1f)
            {
                //clamp rotation speed
                rb.angularVelocity = new Vector3(Mathf.Clamp(rb.angularVelocity.x, -maxRotationSpeed, maxRotationSpeed),
                                                 Mathf.Clamp(rb.angularVelocity.y, -maxRotationSpeed, maxRotationSpeed),
                                                 Mathf.Clamp(rb.angularVelocity.z, -maxRotationSpeed, maxRotationSpeed));
            }
        }
        else
        {
            rb.angularVelocity -= new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, 0f) * postureControl * Time.deltaTime;
        } */

        //Rotation control
        /* if(Input.GetKey(KeyCode.Q))
        {
            rb.AddRelativeTorque(Vector3.forward * rotationSpeedXY, ForceMode.VelocityChange);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            rb.AddRelativeTorque(-Vector3.forward * rotationSpeedXY, ForceMode.VelocityChange);
        }
        else
        {
            rb.angularVelocity -= new Vector3(0f, 0f, rb.angularVelocity.z) * postureControl * Time.deltaTime;
        } */
    }    
    
    void MouseMinMax()
    {
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;    
    }

    void CenterOfMass()
    {
        rb.centerOfMass = Vector3.zero;
    }

    //draw gizmos sphere
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(Vector3.zero, 2f);
    }
}
