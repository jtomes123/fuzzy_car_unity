using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactionTest : MonoBehaviour
{
    public FloatEvent onTestFinished;
    public float startTime;

    private void OnEnable()
    {
        startTime = Time.time;
    }

    public void Click()
    {
        onTestFinished?.Invoke(Time.time - startTime);
    }
}
