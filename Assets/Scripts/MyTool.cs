using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyTool : MonoBehaviour
{
    public static bool IsInside(Collider c, Vector3 point)
    {
        Vector3 closest = c.ClosestPoint(point);
        // Because closest=point if point is inside - not clear from docs I feel
        return closest == point;
    }

}
