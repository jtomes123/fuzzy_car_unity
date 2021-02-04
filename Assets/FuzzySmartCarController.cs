using System.Collections;
using System.Collections.Generic;
using FLS;
using FLS.Rules;
using UnityEngine;

public class FuzzySmartCarController : MonoBehaviour
{
    public double maxDelta, reactionTime, speed, safeDistance;
    IFuzzyEngine engine;
    // Start is called before the first frame update
    void Start()
    {
        var speed = new LinguisticVariable("Speed");
        var slow = speed.MembershipFunctions.AddTrapezoid("Slow", 0, 0, 30, 50);
        var average = speed.MembershipFunctions.AddTrapezoid("Average", 30, 50, 80, 90);
        var fast = speed.MembershipFunctions.AddTrapezoid("Fast", 80, 90, 200, 200);

        var reactionTime = new LinguisticVariable("ReactionTime");
        var bad = reactionTime.MembershipFunctions.AddTrapezoid("Bad", 0.5, 0.6, 3, 3);
        var mediocre = reactionTime.MembershipFunctions.AddTrapezoid("Mediocre", 0.3, 0.35f, 0.5f, 0.6);
        var good = reactionTime.MembershipFunctions.AddTrapezoid("Good", 0, 0, .3f, 0.35f);

        var speedVariability = new LinguisticVariable("Variability");
        var low = speedVariability.MembershipFunctions.AddTrapezoid("Low", 0, 0, 2, 3);
        var medium = speedVariability.MembershipFunctions.AddTriangle("Medium", 2, 3, 5);
        var high = speedVariability.MembershipFunctions.AddTrapezoid("High", 3, 5, 10, 10);

        var distance = new LinguisticVariable("Distance");
        var close = distance.MembershipFunctions.AddTrapezoid("Close", 2, 2, 20, 30);
        var near = distance.MembershipFunctions.AddTriangle("Near", 30, 65, 100);
        var far = distance.MembershipFunctions.AddRectangle("Far", 100, 150);

        var rule1 = Rule.If(speed.Is(slow)).Then(distance.Is(close));
        var rule2 = Rule.If(speed.Is(average)).Then(distance.Is(near));
        var rule3 = Rule.If(speed.Is(fast)).Then(distance.Is(far));

        var rule4 = Rule.If(reactionTime.Is(good)).Then(distance.Is(close));
        var rule5 = Rule.If(reactionTime.Is(mediocre)).Then(distance.Is(near));
        var rule6 = Rule.If(reactionTime.Is(bad)).Then(distance.Is(far));

        var rule7 = Rule.If(speedVariability.Is(low)).Then(distance.Is(close));
        var rule8 = Rule.If(speedVariability.Is(medium)).Then(distance.Is(near));
        var rule9 = Rule.If(speedVariability.Is(high)).Then(distance.Is(far));

        engine = new FuzzyEngineFactory().Default();

        engine.Rules.Add(rule1, rule2, rule3, rule4, rule5, rule6,
            rule7, rule8, rule9);
    }

    // Update is called once per frame
    void Update()
    {
        safeDistance = engine.Defuzzify(new { Speed = speed, ReactionTime = reactionTime, Variability = maxDelta });
    }

    public void SetReactionTime(float value)
    {
        reactionTime = value;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
}
