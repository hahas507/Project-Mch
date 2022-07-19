using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    // [Range(0, 100)] [SerializeField] float rotationSpeedXY;
    // [Range(0, 100)] [SerializeField] float rotationSpeedZ;
    [Range(0, 1)] [SerializeField] float rotationSpeed;
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
        CenterOfMass();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        //가속
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(flightUnit.FlightUnitDir * speed);
        }
        //XY축 회전
        Vector3 mousePosition = Input.mousePosition;
        float curMousePosX = Mathf.InverseLerp(0, screenSize.x, mousePosition.x);
        float curMousePosY = Mathf.InverseLerp(0, screenSize.y, mousePosition.y);
        Vector3 curMousePos = new Vector3(-curMousePosY, curMousePosX, 0f);
        Vector3 direction = ((curMousePos - (new Vector3(-1f, 1f, 0f) * 0.5f)) * 2f);        
        if(Mathf.Abs(direction.x) < 0.5f)
        {
            direction.x = 0f;
        }            
        if(Mathf.Abs(direction.y) < 0.5f)
        {
            direction.y = 0f;
        }        
        rb.AddRelativeTorque(direction * rotationSpeed, ForceMode.VelocityChange);
        //적당히 멈춤
        if(rb.angularVelocity.magnitude < 0.1f)
        {
            rb.angularVelocity = Vector3.zero;
        }

        //Z축 회전
        if(Input.GetKey(KeyCode.Q))
        {
            rb.AddRelativeTorque(Vector3.forward * rotationSpeed, ForceMode.VelocityChange);
        }
        else if(Input.GetKey(KeyCode.E))
        {
            rb.AddRelativeTorque(-Vector3.forward * rotationSpeed, ForceMode.VelocityChange);
        }
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
}
