using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitUI : MonoBehaviour
{
    public UnityEvent onTimeElapled;

    void OnEnable()
    {
        StartCoroutine(RandomWait());
    }

    IEnumerator RandomWait()
    {
        yield return new WaitForSecondsRealtime(Random.Range(1, 4));

        onTimeElapled?.Invoke();
    }
}
