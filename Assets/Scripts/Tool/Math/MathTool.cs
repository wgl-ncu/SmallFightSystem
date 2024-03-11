using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathTool
{
    public static Vector3 LerpVector3(Vector3 v1, Vector3 v2, float t)
    {
        float x = Mathf.Lerp(v1.x, v2.x, t);
        float y = Mathf.Lerp(v1.y, v2.y, t);
        float z = Mathf.Lerp(v1.z, v2.z, t);
        return new Vector3(x, y, z);
    }
}
