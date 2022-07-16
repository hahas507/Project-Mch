using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [Range(0, 1)] [SerializeField] float rotationSpeed;
    [SerializeField] float postureControl;
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
        PostureControl();
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
        
        /* //Direction control with keyboard
        float yInput = Input.GetAxis("Horizontal");
        float xInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(-xInput, yInput, 0f);
        rb.AddRelativeTorque(direction * rotationSpeed, ForceMode.Acceleration); */
        
        //mouse position
        Vector3 mousePosition = Input.mousePosition;
        float curMousePosX = Mathf.InverseLerp(0, screenSize.x, mousePosition.x);
        float curMousePosY = Mathf.InverseLerp(0, screenSize.y, mousePosition.y);
        Vector3 curMousePos = new Vector3(-curMousePosY, curMousePosX, 0f);
        Vector3 direction = curMousePos - (new Vector3(-1f, 1f, 0f) * 0.5f);
        rb.AddRelativeTorque(direction * rotationSpeed, ForceMode.Acceleration);

        //Rotation control
        if(Input.GetKey(KeyCode.Q))
        {
            rb.AddRelativeTorque(Vector3.forward * rotationSpeed, ForceMode.Acceleration);
        }
        if(Input.GetKey(KeyCode.E))
        {
            rb.AddRelativeTorque(-Vector3.forward * rotationSpeed, ForceMode.Acceleration);
        }
    }    
    
    void PostureControl()
    {
        //Posture control
        if(Input.GetKey(KeyCode.F))
        {
            rb.angularVelocity -= rb.angularVelocity * postureControl * Time.deltaTime;
        }
    }

    void MouseMinMax()
    {
        screenSize.x = Screen.width;
        screenSize.y = Screen.height;    
    }
}
