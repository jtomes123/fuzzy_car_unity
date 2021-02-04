using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TrackPooler : MonoBehaviour
{
    public GameObject prefab;
    public float z;
    public int poolCount = 100, n = -3, updatesPerCycle = 3;
    float length = 10;
    Coroutine updateCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolCount; i++)
        {
            Instantiate(prefab, transform).SetActive(false);
        }

        Rebuild();
        StartCoroutine(Recycle());
    }

    /**
     * Rebuilds the whole track
     */
    void Rebuild()
    {
        
        for (int i = 0; i < poolCount; i++)
        {
            var obj = transform.GetChild(i);
            var n = i - 3;

            obj.localPosition = new Vector3(0, 0, n * length);
            obj.gameObject.SetActive(true);
        }

        this.n = poolCount - 4;
    }

    /**
     * Recycles tiles behind player
     */
    IEnumerator Recycle()
    {
        int updates = 0;
        while(true)
        {
            Transform child = transform.GetChild(updates % poolCount);
            if ((child.position.z + length * 3) < z)
            {
                child.localPosition = new Vector3(0, 0, n * length);
                n++;
            }
            updates++;

            if (updates % 3 == 0)
            {
                yield return null;
            }
        }
    }
}
