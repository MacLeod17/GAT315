using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public FloatData gravitation;
    public FloatData gravity;
    public FloatData fixedFPS;
    public StringData fpsText;
    public bool useExplicit = false;

    float timeAccumulator;
    public float fixedDeltaTime { get { return (1.0f / fixedFPS.value); } }
    float fps = 0;
    float fpsAverage = 0;
    float smoothing = 0.975f;

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
        float dt = Time.deltaTime;

        fps = (1.0f / dt);
        fpsAverage = (fpsAverage * smoothing) + (fps * (1.0f - smoothing));
        fpsText.value = $"FPS: {fpsAverage.ToString("F1")}";

        if (!simulate.value) return;

        timeAccumulator += dt;

        GravitationalForce.ApplyForce(bodies, gravitation);

        while (timeAccumulator > fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            if (useExplicit)
            {
                bodies.ForEach(body => Integrator.ExplicitEuler(body, fixedDeltaTime));
            }
            else
            {
                bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));
            }

            bodies.ForEach(body => body.shape.color = Color.white);
            Collision.CreateContacts(bodies, out List<Contact> contacts);
            contacts.ForEach(contact => { contact.bodyA.shape.color = Color.green; contact.bodyB.shape.color = Color.green; });

            timeAccumulator -= fixedDeltaTime;
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
