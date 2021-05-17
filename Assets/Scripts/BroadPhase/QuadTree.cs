using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTree : BroadPhase
{
    public int capacity { get; set; } = 4;
    QuadTreeNode rootNode;

    public override void Build(AABB aabb, List<Body> bodies)
    {
        potientialCollisionCount = 0;
        rootNode = new QuadTreeNode(aabb, capacity);
        bodies.ForEach(body => rootNode.Insert(body));
    }

    public override void Query(AABB aabb, List<Body> bodies)
    {
        rootNode.Query(aabb, bodies);
        potientialCollisionCount += bodies.Count;
    }

    public override void Query(Body body, List<Body> bodies)
    {
        Query(body.shape.aABB, bodies);
    }

    public override void Draw()
    {
        rootNode?.Draw();
    }
}