using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PDController : MonoBehaviour
{
    public List<PDControllerInfo> controllers = new List<PDControllerInfo>();
    
    void Update()
    {
        foreach (var controller in controllers)
        {
            controller.Update();
        }
    }

    public void SetControllerValue()
    {

    }

    public void SetControllerTarget()
    {

    }
}

[Serializable]
public class PDControllerInfo
{
    public FloatEvent onNewValue;

    public float pC, dC, dI, target, currentValue;
    float error, lastError, sum;

    public void Update()
    {
        error = target - currentValue;
        var p = pC * error;
        var d = dC * (error - lastError);
        var i = dI * (sum + error);

        lastError = error;
        sum += error;

        onNewValue.Invoke(p + d + i);
    }
}

[Serializable]
public class FloatEvent: UnityEvent<float>
{

}
