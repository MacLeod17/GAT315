using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public bool useExplicit = false;

    static World instance;
    public static World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!simulate.value) return;

        float dt = Time.deltaTime;

        bodies.ForEach(body => body.Step(dt));
        if (useExplicit)
        {
            bodies.ForEach(body => Integrator.ExplicitEuler(body, dt));
        }
        else
        {
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, dt));
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
