using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Circle
{
    public float radius;
    public Vector2 center;

    public Circle(Vector2 _center, float _radius)
    {
        radius = _radius;
        center = _center;
    }

    public bool Contains(Vector2 point)
    {
        Vector2 direction = center - point;
        float sqrDistance = direction.sqrMagnitude;
        float sqrRadius = radius * radius;

        return (sqrDistance <= sqrRadius);
    }

    public bool Contains(Circle circle)
    {
        Vector2 direction = center - circle.center;
        float sqrDistance = direction.sqrMagnitude;
        float sqrRadius = (radius + circle.radius) * (radius + circle.radius);

        return (sqrDistance <= sqrRadius);
    }
}
