using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLS;
using FLS.Rules;

[RequireComponent(typeof(Rigidbody))]
public class FuzzyController : MonoBehaviour
{
    // Start is called before the first frame update

    IFuzzyEngine fuzzyEngine;
    public double speed = 65;
    public double variability = 0;
    public float distance = 0;
    Rigidbody myRigidbody;
    PDController controller;

    public FloatEvent onValue;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        controller = GetComponent<PDController>();

        var speed = new LinguisticVariable("Speed");
        //var slow = speed.MembershipFunctions.AddTrapezoid("Slow", 0, 0, 5, 10);
        //var average = speed.MembershipFunctions.AddTrapezoid("Average", 10, 15, 20, 25);
        //var fast = speed.MembershipFunctions.AddTrapezoid("Fast", 5, 15, 100, 100);

        var slow = speed.MembershipFunctions.AddTriangle("Slow", 0, 5, 10);
        var fast = speed.MembershipFunctions.AddTriangle("Fast", 7.5, 20, 50);

        var distance = new LinguisticVariable("Distance");
        /*
        var low = distance.MembershipFunctions.AddTrapezoid("Low", 0, 0, 2.5, 5);
        var medium = distance.MembershipFunctions.AddTrapezoid("Medium", 2.5, 5, 12.5, 20);
        var high = distance.MembershipFunctions.AddTrapezoid("High", 12.5, 20, 38, 50);
        */

        var low = distance.MembershipFunctions.AddTriangle("Low", 0, 10, 20);
        var high = distance.MembershipFunctions.AddTriangle("High", 10, 20, 55);

        var variability = new LinguisticVariable("Variability");
        var varH = variability.MembershipFunctions.AddTrapezoid("VarH", 1.5, 2, 5, 20);
        var varL = variability.MembershipFunctions.AddTrapezoid("VarL", 0, 0, 1.5, 2);

        fuzzyEngine = new FuzzyEngineFactory().Default();

        var rule1 = Rule.If(speed.Is(slow)).Then(distance.Is(low));
        //var rule2 = Rule.If(speed.Is(average)).Then(distance.Is(medium));
        var rule3 = Rule.If(speed.Is(fast)).Then(distance.Is(high));
        var rule4 = Rule.If(variability.Is(varL)).Then(distance.Is(low));
        var rule5 = Rule.If(variability.Is(varH)).Then(distance.Is(high));

        fuzzyEngine.Rules.Add(rule1);
        //fuzzyEngine.Rules.Add(rule2);
        fuzzyEngine.Rules.Add(rule3);
        fuzzyEngine.Rules.Add(rule4);
        fuzzyEngine.Rules.Add(rule5);

        StartCoroutine(CycleAI());
    }

    IEnumerator CycleAI()
    {
        while (true)
        {
            speed = myRigidbody.velocity.magnitude;
            distance = (float)fuzzyEngine.Defuzzify(new { speed = speed, variability = variability });

            //controller.controllers[0].target = distance;
            onValue?.Invoke(distance);

            yield return new WaitForSecondsRealtime(1);
        }
    }
}
