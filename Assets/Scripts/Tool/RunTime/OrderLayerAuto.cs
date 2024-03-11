using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderLayerAuto : MonoBehaviour
{
    public void Refresh()
    {
        int orderOffset = 0;
        for(int i = 0; i < transform.childCount; ++i)
        {
            var child = transform.GetChild(i);
            orderOffset = RefreshOrder(child, orderOffset);
        }
    }

    private int RefreshOrder(Transform trans, int baseValue)
    {
        int newValue = baseValue;
        var targets = trans.GetComponentsInChildren<SpriteRenderer>();
        foreach(var target in targets)
        {
            target.sortingOrder += baseValue;
            newValue = Mathf.Max(newValue, target.sortingOrder);
        }
        return newValue + 1;
    }
}
