using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData gravitation;
    public FloatData fixedFPS;
    public StringData fpsData;
    float fps = 0;
    public TMP_Text fpsText = null;

    static World instance;

    public float timeAccumaltor;
    public float fpsAverage;
    public float smoothing = 0.95f;
    public float fixedDeltaTime { get { return 1.0f / fixedFPS.value; } }
    static public World Instance { get { return instance; } }

    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
    }
    
    void Update()
    {
        if (!simulate.value)
        {
            return;
        }
        float dt = Time.deltaTime;
        fps = (1.0f / dt);
        fpsAverage = (fpsAverage * smoothing) + (fps * (1 - smoothing));
        fpsText.text = fpsAverage.ToString("F2");


        timeAccumaltor += Time.deltaTime;

        GravitationalForce.ApplyForce(bodies, gravitation.value);

        while (timeAccumaltor > fixedDeltaTime)
        {
            bodies.ForEach(body => body.Step(fixedDeltaTime));
            bodies.ForEach(body => Integrator.SemiImplicitEuler(body, fixedDeltaTime));//ExplicitEuler(body, dt));
            bodies.ForEach(body => body.shape.color = Color.green);

            Collision.CreateContacts(bodies, out List<Contact> contacts);
            contacts.ForEach(contact => { contact.bodyA.shape.color = Color.red; contact.bodyB.shape.color = Color.red; });
            ContactSolver.Resolve(contacts);

            timeAccumaltor = timeAccumaltor - fixedDeltaTime;
        }

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);

        //Debug.Log(1.0f / Time.deltaTime);
    }
}