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
      Debug.Log(flightUnit);
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
        //Posture control
        if(Input.GetKey(KeyCode.Q))
        {
            rb.angularVelocity -= rb.angularVelocity * postureControl * Time.deltaTime;
        }    
        //Rotation control
        float yInput = Input.GetAxis("Horizontal");
        float xInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(xInput, yInput, 0f);
        rb.AddTorque(direction * rotationSpeed, ForceMode.Acceleration);
    }    
}
