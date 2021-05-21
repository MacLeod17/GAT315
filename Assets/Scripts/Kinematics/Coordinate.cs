using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Coordinate
{
    public struct Polar
    {
        public float angle;
        public float length;
    }

    public static Vector2 PolarToCartesian(Polar polar)
    {
        Vector2 v2 = Vector2.zero;
        v2.x = Mathf.Cos(polar.angle * Mathf.Deg2Rad) * polar.length;
        v2.y = Mathf.Sin(polar.angle * Mathf.Deg2Rad) * polar.length;

        return v2;
    }

    public static Polar CartesianToPolar(Vector2 v)
    {
        Polar polar;
        polar.angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
        polar.length = v.magnitude;

        return polar;
    }
}
