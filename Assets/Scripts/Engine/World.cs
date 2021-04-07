using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class World : MonoBehaviour
{
    public BoolData simulate;
    public FloatData gravity;
    public FloatData fixedFPS;
    public TMP_Text fpsText = null;
    public bool useExplicit = false;

    float timeAccumulator;
    float fpsUpdateCounter;

    static World instance;
    public static World Instance { get { return instance; } }

    public float fixedDeltaTime { get { return (1.0f / fixedFPS.value); } }
    public Vector2 Gravity { get { return new Vector2(0, gravity.value); } }
    public List<Body> bodies { get; set; } = new List<Body>();

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        fpsUpdateCounter++;

        if (!simulate.value) return;

        float dt = Time.deltaTime;
        timeAccumulator += dt;

        if (fpsUpdateCounter >= 250)
        {
            float fps = (1.0f / dt);
            fpsText.text = $"FPS: {fps.ToString("F1")}";
            fpsUpdateCounter = 0;
        }

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

            timeAccumulator -= fixedDeltaTime;
        }
        

        bodies.ForEach(body => body.force = Vector2.zero);
        bodies.ForEach(body => body.acceleration = Vector2.zero);
    }
}
