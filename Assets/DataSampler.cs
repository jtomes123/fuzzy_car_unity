using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataSampler : MonoBehaviour
{
    public FloatEvent onValueSpeed, onValueDistance, onValueDeviation;
    public UnityEvent onSamplingFinished;
    public FuzzySmartCarController controller;

    private void Start()
    {
        StartCoroutine(PeriodicSampler());
    }

    IEnumerator PeriodicSampler()
    {
        while (true)
        {
            onValueSpeed?.Invoke((float)controller.speed);
            onValueDeviation?.Invoke((float)controller.maxDelta);
            onValueDistance?.Invoke((float)controller.safeDistance);

            onSamplingFinished?.Invoke();

            yield return new WaitForSecondsRealtime(1);
        }
    }
}
