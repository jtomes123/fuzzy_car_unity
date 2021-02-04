using System.Collections;
using System.Collections.Generic;
using FLS;
using FLS.Rules;
using UnityEngine;

public class FuzzyAccelerator : MonoBehaviour
{
    IFuzzyEngine fuzzyEngine;
    public double acceleration = 0;
    public double distance = 0;
    public double targetDistance = 0;
    CarController controller;
    public Lidar lidar;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CarController>();

        var acceleration = new LinguisticVariable("Acceleration");
        var accelerate = acceleration.MembershipFunctions.AddTrapezoid("Accelerate", 1, 1.5, 2, 2);
        var coast = acceleration.MembershipFunctions.AddTriangle("Coast", 0.5, 1, 1.5);
        var decelerate = acceleration.MembershipFunctions.AddTrapezoid("Decelerate", 0, 0, 0.5, 1);

        var distance = new LinguisticVariable("Distance");
        var close = distance.MembershipFunctions.AddTrapezoid("Close", 0, 0, 6, 10);
        var far = distance.MembershipFunctions.AddTrapezoid("Far", 5, 20, 100, 100);

        var deltaToTargetDistance = new LinguisticVariable("DistanceDelta");
        var below = deltaToTargetDistance.MembershipFunctions.AddTrapezoid("Below" , 0, 0, 5, 10);
        var onPoint = deltaToTargetDistance.MembershipFunctions.AddTriangle("Middle", 5, 10, 15);
        var above = deltaToTargetDistance.MembershipFunctions.AddTrapezoid("Above" , 10, 15, 20, 20);

        var rule1 = Rule.If(deltaToTargetDistance.Is(below).Or(distance.Is(close))).Then(acceleration.Is(decelerate));
        var rule2 = Rule.If(deltaToTargetDistance.Is(above).And(distance.IsNot(close))).Then(acceleration.Is(accelerate));
        var rule3 = Rule.If(deltaToTargetDistance.Is(onPoint)).Then(acceleration.Is(coast));

        fuzzyEngine = new FuzzyEngineFactory().Default();

        fuzzyEngine.Rules.Add(rule1);
        fuzzyEngine.Rules.Add(rule2);

        StartCoroutine(AICycle());
    }

    IEnumerator AICycle()
    {
        while (true)
        {
            Debug.Log("tick");

            this.distance = lidar.GetDistance();

            var distanceDelta = distance - targetDistance;
            if (distanceDelta < -10)
                distanceDelta = -10;
            else if (distanceDelta > 10)
                distanceDelta = 10;

            distanceDelta += 10;
            distanceDelta = Mathf.Abs((float)distanceDelta);

            acceleration = fuzzyEngine.Defuzzify(new { Distance = distance, DistanceDelta = distanceDelta }) - 1;
       
            controller.accelleration = (float)acceleration;

            yield return new WaitForSecondsRealtime(1);
        }
    }

    public void SetTargetDistance(float value)
    {
        targetDistance = value;
    }
}
