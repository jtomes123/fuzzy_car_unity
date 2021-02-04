using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistributeLines : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var rect = GetComponent<RectTransform>();
        for (int i = 0; i < 10; i++)
        {
            var childRect = transform.GetChild(i).GetComponent<RectTransform>();
            childRect.localPosition = new Vector2(childRect.localPosition.x, i * rect.rect.size.y / 10 - rect.rect.size.y / 2);
        }
    }
}
