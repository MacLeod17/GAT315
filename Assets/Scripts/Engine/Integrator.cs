using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static void ExplicitEuler(Body body, float dt)
    {
        body.position += body.velocity * dt;
        body.velocity += body.acceleration * dt;
        body.velocity *= 1.0f / (1.0f + (body.damping * dt));
    }
}
