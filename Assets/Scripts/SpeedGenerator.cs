using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedGenerator : MonoBehaviour
{
    public FloatEvent onBarValueChange, onSpeedChange;
    public float startTime, endTime, minDuration = 5, maxDuration = 15;
    public float minSpeed = 20, maxSpeed = 140;
    float speed = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BarValueUpdateRoutine());
        StartCoroutine(SpeedGeneratorRoutine());
    }

    IEnumerator BarValueUpdateRoutine()
    {
        while (true)
        {
            onBarValueChange?.Invoke(Mathf.InverseLerp(startTime, endTime, Time.timeSinceLevelLoad));
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    IEnumerator SpeedGeneratorRoutine()
    {
        while (true)
        {
            float duration = Random.Range(minDuration, maxDuration);
            speed = Random.Range(minSpeed, maxSpeed);
            startTime = Time.timeSinceLevelLoad;
            endTime = Time.timeSinceLevelLoad + duration;
            onSpeedChange?.Invoke(speed);
            yield return new WaitForSecondsRealtime(duration);
        }
    }
}
