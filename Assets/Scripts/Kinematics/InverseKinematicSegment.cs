using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InverseKinematicSegment : KinematicSegment
{
    void Update()
    {
        transform.localScale = Vector2.one * size;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void Initialize(KinematicSegment parent, Vector2 position, float angle, float length, float size)
    {
        this.parent = parent;
        this.size = size;

        this.angle = angle;
        this.length = length;

        start = position;
    }

    public void Follow(Vector2 target)
    {
        Vector2 direction = target - start;
        Coordinate.Polar polar = Coordinate.CartesianToPolar(direction);
        this.angle = polar.angle;

        start = target - (direction.normalized * length);
    }
}
