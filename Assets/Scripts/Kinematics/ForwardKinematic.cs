using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardKinematic : MonoBehaviour
{
    public ForwardKinematicSegment original;
    public int count = 5;
    public float length = 1;

    List<ForwardKinematicSegment> segments = new List<ForwardKinematicSegment>();

    private void Start()
    {
        KinematicSegment parent = null;

        for (int i=0; i < count; i++)
        {
            var segment =  Instantiate(original);
            segment.Initialize(parent, transform.position, 0, length, 1);

            segments.Add(segment);
            parent = segment;
        }
    }

    void Update()
    {
        foreach (ForwardKinematicSegment segment in segments)
        {
            if (segment.parent != null)
            {
                segment.start = segment.parent.end;
            }
            segment.CalculateEnd();
        }
    }
}
