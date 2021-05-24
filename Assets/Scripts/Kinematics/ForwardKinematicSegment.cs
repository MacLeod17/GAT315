using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematicSegment : KinematicSegment
{
    [Range(-90, 90)] public float inputAngle;
    public bool enableNoise = false;

    float baseAngle;
    float noise;

    private void Start()
    {
        noise = Random.value * 20;
    }

    void Update()
    {
        transform.localScale = Vector2.one * size;

        float localAngle = inputAngle;
        if (enableNoise)
        {
            noise += Time.deltaTime;
            float t = Mathf.PerlinNoise(noise, 0);
            localAngle = Mathf.Lerp(-90, 90, t);
        }

        angle = (parent != null) ? (localAngle + parent.angle) : (localAngle + baseAngle);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public override void Initialize(KinematicSegment parent, Vector2 position, float angle, float length, float size)
    {
        this.parent = parent;
        this.size = size;

        this.angle = angle;
        this.length = length;

        start = position;
        baseAngle = angle;
    }
}
