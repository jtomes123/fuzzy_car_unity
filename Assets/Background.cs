using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    float scrollSpeed = 1, speed = 20;
    public float minSpeed = 20, maxSpeed = 140, minScrollSpeed = 1, maxScrollSpeed = 3;

    void Update()
    {
        float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, speed);
        float targetScrollSpeed = Mathf.Lerp(minScrollSpeed, maxScrollSpeed, normalizedSpeed);

        scrollSpeed = Mathf.Lerp(scrollSpeed, targetScrollSpeed, 0.1f);

        foreach(Transform child in transform)
        {
            var mult = 1;
            if (child.gameObject.name == "car")
                mult = 2;
            child.transform.position += Vector3.down * scrollSpeed * Time.deltaTime * mult;

            if (child.transform.position.y < -13)
            {
                child.transform.position += Vector3.up * 26;
            }
        }
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }
}
