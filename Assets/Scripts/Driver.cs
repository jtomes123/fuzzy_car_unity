using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Driver : MonoBehaviour
{
    float baseDistanceY = -0.9f;
    float maxDistanceY = 4.5f;
    float currentDistance = 0;

    public float minDistance = 0, maxDistance = 100, distance = 50;
    public float speedOffset = 0;

    public float a, b, c, maxDelta;
    public float[] samples;
    public int sampleCount = 10;

    public FloatEvent onVaribilityChange;
    public FloatEvent onDistanceChange;

    private void Start()
    {
        GenerateConstants();
        samples = new float[10];

        StartCoroutine(SpeedOffsets());
    }

    void Update()
    {
        speedOffset = Mathf.Sin(Time.timeSinceLevelLoad * a + c) +
    Mathf.Sin(Time.timeSinceLevelLoad * b + a) +
    Mathf.Sin(Time.timeSinceLevelLoad * c + b);

        var targetDistance = Mathf.InverseLerp(minDistance,
            maxDistance, distance + speedOffset);

        currentDistance = Mathf.Lerp(currentDistance, targetDistance, 0.1f);

        transform.position = new Vector3(transform.position.x,
            Mathf.Lerp(baseDistanceY, maxDistanceY,
            currentDistance), transform.position.z);

        onDistanceChange?.Invoke(distance);
    }

    IEnumerator SpeedOffsets()
    {
        while (true)
        {
            

            float min = float.MaxValue, max = float.MinValue;
            for (int i = 1; i < samples.Length; i++)
            {
                samples[i - 1] = samples[i];
            }

            samples[samples.Length - 1] = speedOffset;

            foreach (var sample in samples)
            {
                if (min > sample)
                    min = sample;
                if (max < sample)
                    max = sample;
            }

            maxDelta = Mathf.Abs(max - min);
            onVaribilityChange?.Invoke(maxDelta);

            yield return new WaitForSecondsRealtime(.25f);
        }
    }

    public void GenerateConstants()
    {
        a = Random.Range(0.1f, 3f);
        b = Random.Range(0.1f, 3f);
        c = Random.Range(0.1f, 3f);
    }
}
