using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LidarPrintout : MonoBehaviour
{
    public Lidar lidar;
    public TextMeshProUGUI title, data;

    private void Start()
    {
        if (!lidar)
            return;

        title.SetText(lidar.name);
        StartCoroutine(UpdateTextCoroutine());
    }

    IEnumerator UpdateTextCoroutine()
    {
        while (true)
        {
            UpdateText();
            yield return new WaitForSecondsRealtime(0.25f);
        }
    }

    void UpdateText()
    {
        if (!lidar)
            return;
        var dist = lidar.GetDistance();
        data.text = float.IsPositiveInfinity(dist) ? "INF" : dist + "m";
    }
}
