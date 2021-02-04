using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUpdate : MonoBehaviour
{
    public TextMeshProUGUI speed, reactionTime, safeDistance, variability;

    private void Awake()
    {
        SetSpeed(0);
        //SetReactionTime(0);
        SetSafeDistance(0);
        SetVariability(0);
    }

    public void SetSpeed(float value)
    {
        speed.SetText(value + " km/h");
    }
    public void SetReactionTime(float value)
    {
        reactionTime.SetText(value + " s");
    }
    public void SetSafeDistance(float value)
    {
        safeDistance.SetText(value + " m");
    }
    public void SetVariability(float value)
    {
        variability.SetText(value + " km/h");
    }
}
