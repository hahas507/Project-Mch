using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
   [SerializeField] GameObject playerCam;
    CameraControl camControl;

    void Start()
    {
        camControl = playerCam.GetComponent<CameraControl>();
    }
    void Update()
    {
       GunAim();
    }

    void GunAim()
    {
      transform.LookAt(camControl.Hitpoint);    
      Debug.DrawRay(transform.position, transform.forward * Vector3.Distance(transform.position, camControl.Hitpoint), Color.red);
    }
}
