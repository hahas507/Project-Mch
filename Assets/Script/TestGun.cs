using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : GunController
{
    CameraControl cameraControl;

    void Start()
    {
        cameraControl = GameObject.Find("Camera").GetComponent<CameraControl>();
    }
    void Update()
    {
        GunAim();
    }

    void GunAim()
    {
        // Player에서 전부 컨트롤 하는게 나을듯
    }
}
