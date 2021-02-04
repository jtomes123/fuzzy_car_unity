using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetValues : MonoBehaviour
{
    Rigidbody myRigidbody;
    PDController controller;
    public int steeringId, accelerationId;
    public Lidar lidar;
    public bool distanceFromLidar;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<PDController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (distanceFromLidar)
        {
            var lidarDistance = lidar.GetDistance();
            controller.controllers[accelerationId].currentValue = float.IsPositiveInfinity(lidarDistance) ? 0 : lidarDistance;
        }
        else
        {
            controller.controllers[accelerationId].currentValue = myRigidbody.velocity.magnitude;
        }
        controller.controllers[steeringId].currentValue = transform.position.x;
    }
}
