using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring
{
    public Body bodyA { get; set; } = null;
    public Body bodyB { get; set; } = null;

    public float restLength { get; set; } = 0.0f;
    public float k { get; set; } = 20.0f;

    public void ApplyForce()
    {
        Vector2 force = Utilities.SpringForce(bodyA.position, bodyB.position, restLength, k);
        float modifier = (bodyA.type == Body.eType.Static || bodyB.type == Body.eType.Static) ? 1.0f : 0.5f;

        bodyA.AddForce(-force * modifier);
        bodyB.AddForce(force * modifier);
    }

    public void Draw()
    {
        Lines.Instance.AddLine(bodyA.position, bodyB.position, Color.white, 0.1f);
    }
}