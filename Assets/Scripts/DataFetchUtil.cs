using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataFetchUtil : MonoBehaviour
{
    // Start is called before the first frame update
    public Driver driver;
    FuzzySmartCarController controller;
    void Start()
    {
        controller = GetComponent<FuzzySmartCarController>();
    }

    // Update is called once per frame
    void Update()
    {
        controller.maxDelta = driver.maxDelta;
        //controller.speed = 90;
        //controller.reactionTime = 0.3f;
        driver.distance = (float)controller.safeDistance;
    }
}
