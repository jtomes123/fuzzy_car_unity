using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lidar : MonoBehaviour
{
    public float range;
    float distanceToObject = float.PositiveInfinity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool success = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);

        if (success && hit.distance <= range)
        {
            distanceToObject = hit.distance;
        } else
        {
            distanceToObject = float.PositiveInfinity;
        }
    }

    public float GetDistance()
    {
        return distanceToObject;
    }

    void OnDrawGizmos()
    {
        if (float.IsPositiveInfinity(distanceToObject))
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * range);
        } else
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * distanceToObject);
        }
    }
}
