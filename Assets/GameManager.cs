using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject car;
    public TrackPooler pooler;

    void Update()
    {
        pooler.z = car.transform.position.z;
    }
}
