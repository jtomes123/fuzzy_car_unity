using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartPlot : MonoBehaviour
{
    public float[] values;
    public float maxY = 100;
    public Color color;

    ChartPoint[] points = { };
    Vector2 size;
    // Start is called before the first frame update
    void Start()
    {
        points = transform.GetComponentsInChildren<ChartPoint>();
        size = GetComponent<RectTransform>().rect.size;
    }

    void Refresh()
    {
        for (int i = 0; i < values.Length; i++)
        {
            if (i > points.Length - 1)
            {
                break;
            }

            var offsetI = i < points.Length - 1 && i < values.Length - 1 ? i + 1 : i;
            //var nextPosition = new Vector2(offsetI * (size.x / 10), (values[offsetI] / maxY) * size.y);
            //var position = new Vector2(i * (size.x / 10), (values[i] / maxY) * size.y);

            var position = CalculatePosition(i, values[i], size.x, size.y);
            var positionNext = CalculatePosition(offsetI, values[offsetI], size.x, size.y);

            Debug.Log("Position: " + position + " Next Position: " + positionNext);

            points[i].Setup(position, positionNext, color);
        }
    }

    Vector2 CalculatePosition(int index, float value, float width, float height)
    {
        return new Vector2(index * (width / 10) - width / 2, (value / maxY) * height - height / 2);
    }

    public void NewValue(float value)
    {
        for (int i = 0; i < values.Length - 1; i++)
        {
            values[i] = values[i + 1];
        }
        values[values.Length - 1] = value;

        Refresh();
    }
}
