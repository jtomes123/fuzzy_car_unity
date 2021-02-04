using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarController))]
public class ManualCarControl : MonoBehaviour
{
    CarController controller;
    public bool enableAcceleration = true, enableSteering = true;
    private void Start()
    {
        controller = GetComponent<CarController>();
    }

    private void Update()
    {
        if (enableAcceleration)
            controller.accelleration = Input.GetAxis("Vertical");
        if (enableSteering)
            controller.steering = Input.GetAxis("Horizontal");
    }
}
