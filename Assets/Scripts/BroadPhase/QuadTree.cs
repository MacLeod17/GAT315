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
        throw new System.NotImplementedException();
    }

    public override void Query(Body body, List<Body> bodies)
    {
        throw new System.NotImplementedException();
    }
    public override void Draw()
    {
        rootNode?.Draw();
    }
}