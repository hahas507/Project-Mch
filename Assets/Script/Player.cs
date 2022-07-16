using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [Range(0, 1)] [SerializeField] float rotationSpeed;
    [SerializeField] float postureControl;
    FlightUnit flightUnit;
    
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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
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
        
        //Direction control
        float yInput = Input.GetAxis("Horizontal");
        float xInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(-xInput, yInput, 0f);
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
}
