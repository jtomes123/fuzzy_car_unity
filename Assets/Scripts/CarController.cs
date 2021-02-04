﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public float accelleration, steering;

    public void FixedUpdate()
    {
        if (accelleration > 1)
        {
            accelleration = 1;
        } else if (accelleration < -1)
        {
            accelleration = -1;
        }

        if (this.steering > 1)
        {
            this.steering = 1;
        } else if (this.steering < -1)
        {
            this.steering = -1;
        }

        float motor = maxMotorTorque * accelleration;
        float steering = maxSteeringAngle * this.steering;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        }
    }

    public void SetAcceleration(float amount)
    {
        accelleration = amount;
    }

    public void SetSteering(float amount)
    {
        steering = amount;
    }
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}