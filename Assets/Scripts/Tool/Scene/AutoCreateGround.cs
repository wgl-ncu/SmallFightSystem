using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class AutoCreateGround : MonoBehaviour
{
    public int head;
    public int length;
    public int height;

    private readonly string pathGroundLeft = "Prefab/GroundPrefab/TileGroundLeft";
    private readonly string pathGroundMid = "Prefab/GroundPrefab/TileGroundMid";
    private readonly string pathGroundRight = "Prefab/GroundPrefab/TileGroundRight";

    private GameObject left;
    private GameObject mid;
    private GameObject right;

    private void OnValidate()
    {
        LoadPrefab();
        //for(int i = 0; i < transform.childCount; ++i)
        //{
        //    var child = transform.GetChild(i);
        //    DestroyImmediate(child.gameObject);
        //}
        for(int i = 0; i < length; ++i)
        {
            if(i == 0)
            {
                CreateGroundNode(i, left);
            }
            else if(i == length - 1)
            {
                CreateGroundNode(i, right);
            }
            else
            {
                CreateGroundNode(i, mid);
            }
        }
    }

    private void CreateGroundNode(int i, GameObject objPrefab)
    {
        var obj = GameObject.Instantiate(objPrefab, transform);
        obj.SetActive(true);
        obj.name = "Ground" + i;
        obj.transform.position = GetPos(i);
    }

    private Vector3 GetPos(int index)
    {
        return new Vector3(head + index, height - 0.5f, 0);
    }

    private void LoadPrefab()
    {
        if (left == null)
        {
            left = Resources.Load<GameObject>(pathGroundLeft);
        }
        if (mid == null)
        {
            mid = Resources.Load<GameObject>(pathGroundMid);
        }
        if (right == null)
        {
            right = Resources.Load<GameObject>(pathGroundRight);
        }
    }
}
