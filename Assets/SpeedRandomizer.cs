using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedRandomizer : MonoBehaviour
{
    public float maxDelta = 2;
    float minSpeed = 4;
    float nextChange = 0;
    float currnetChange = 0;
    PDController controller;

    private void Start()
    {
        controller = GetComponent<PDController>();
    }

    void Update()
    {
        if (nextChange < Time.timeSinceLevelLoad)
        {
            currnetChange = Time.timeSinceLevelLoad;
            nextChange = currnetChange + Random.Range(3, 10);

            controller.controllers[0].target += (Random.Range(0, maxDelta * 2) - maxDelta);

            if (controller.controllers[0].target < minSpeed)
            {
                controller.controllers[0].target = minSpeed;
            }
        }
    }
}
