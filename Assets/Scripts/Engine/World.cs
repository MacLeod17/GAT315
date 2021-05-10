using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public BoolData collision;
    public BoolData wrap;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData fpsText;
    public VectorField vectorField;

    static World instance;
    static public World Instance { get => instance; }

    public Vector2 Gravity { get { return new Vector2(0, gravity); } }
    public List<Body> bodies { get; set; } = new List<Body>();
    public List<Force> forces { get; set; } = new List<Force>();
    public List<Spring> springs { get; set; } = new List<Spring>();

    public Vector2 WorldSize { get => size * 2; }
    public AABB AABB { get => aabb; }

    AABB aabb;
    Vector2 size;
    float fixedDeltaTime { get { return 1.0f / fixedFPS; } }
    float timeAccumulator = 0;

    private void Awake()
    {
        instance = this;
        size = Camera.main.ViewportToWorldPoint(Vector2.one);
        aabb = new AABB(Vector2.zero, size * 2);
    }

    void Update()
    {
        Timer.Update();
        fpsText.value = $"FPS: {Timer.fps:F1} : {(Timer.dt * 1000.0f):F1} ms";

        springs.ForEach(spring => spring.Draw());

        if (!simulate) return;

        // Forces
        GravitationalForce.ApplyForce(bodies, gravitation);
        forces.ForEach(force => bodies.ForEach(body => force.ApplyForce(body)));
        springs.ForEach(spring => spring.ApplyForce());
        bodies.ForEach(body => vectorField.ApplyForce(body));

        timeAccumulator = timeAccumulator + Time.deltaTime;
        while (timeAccumulator >= fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));

            if (collision)
            {
                bodies.ForEach(body => body.shape.color = Color.white);
                Collision.CreateContacts(bodies, out List<Contact> contacts);
                contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
                ContactSolver.Resolve(contacts);
            }

            timeAccumulator = timeAccumulator - fixedDeltaTime;
        }

        if (wrap)
        {
            bodies.ForEach(body => body.position = Utilities.Wrap(body.position, -size, size));
        }
        bodies.ForEach(body => { body.force = Vector2.zero; body.acceleration = Vector2.zero; });
    }
}