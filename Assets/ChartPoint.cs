using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChartPoint : MonoBehaviour
{
    RectTransform rect, lineRect;
    Image myImage, lineImage;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        lineRect = transform.GetChild(0).GetComponent<RectTransform>();

        myImage = GetComponent<Image>();
        lineImage = transform.GetChild(0).GetComponent<Image>();
    }

    public void Setup(Vector2 position, Vector2 nextPointPosition, Color color)
    {
        rect.localPosition = position;
        //var angle = Mathf.Atan2(nextPointPosition.x - position.x, nextPointPosition.y - position.y) * Mathf.Rad2Deg;
        var angle = AngleTo(position, nextPointPosition);
        var distance = Vector2.Distance(position, nextPointPosition);

        Debug.Log(angle);

        lineRect.localEulerAngles = new Vector3(0, 0, angle);
        lineRect.sizeDelta = new Vector2(distance, lineRect.sizeDelta.y);

        myImage.color = color;
        lineImage.color = color;
    }

    private float AngleTo(Vector2 pos, Vector2 target)
    {
        Vector2 diference = target - pos;
        float sign = (target.y < pos.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }
}
