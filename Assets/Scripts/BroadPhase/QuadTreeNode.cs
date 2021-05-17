using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadTreeNode
{
    private AABB aABB;
    private int capacity;
    private List<Body> bodies;
    private bool subDivided = false;

    QuadTreeNode northeast;
    QuadTreeNode northwest;
    QuadTreeNode southeast;
    QuadTreeNode southwest;

    public QuadTreeNode(AABB aABB, int capacity)
    {
        this.aABB = aABB;
        this.capacity = capacity;

        bodies = new List<Body>();
    }

    public void Insert(Body body)
    {
        if (!aABB.Contains(body.shape.aABB)) return;

        if (bodies.Count < capacity)
        {
            bodies.Add(body);
        }
        else
        {
            if (!subDivided)
            {
                SubDivide();
            }
            northeast.Insert(body);
            northwest.Insert(body);
            southeast.Insert(body);
            southwest.Insert(body);
        }
    }

    public void Query(AABB aabb, List<Body> bodies)
    {
        if (!this.aABB.Contains(aabb)) return;

        foreach (Body body in this.bodies)
        {
            if (body.shape.aABB.Contains(aabb))
            {
                bodies.Add(body);
            }
        }

        if (subDivided)
        {
            northeast.Query(aabb, bodies);
            northwest.Query(aabb, bodies);
            southeast.Query(aabb, bodies);
            southwest.Query(aabb, bodies);
        }
    }

    private void SubDivide()
    {
        float xo = aABB.extents.x * 0.5f;
        float yo = aABB.extents.y * 0.5f;

        northeast = new QuadTreeNode(new AABB(new Vector2(aABB.center.x - xo, aABB.center.y + yo), aABB.extents), capacity);
        northwest = new QuadTreeNode(new AABB(new Vector2(aABB.center.x + xo, aABB.center.y + yo), aABB.extents), capacity);
        southeast = new QuadTreeNode(new AABB(new Vector2(aABB.center.x - xo, aABB.center.y - yo), aABB.extents), capacity);
        southwest = new QuadTreeNode(new AABB(new Vector2(aABB.center.x + xo, aABB.center.y - yo), aABB.extents), capacity);

        subDivided = true;
    }

    public void Draw()
    {
        aABB.Draw(Color.red);

        northeast?.Draw();
        northwest?.Draw();
        southeast?.Draw();
        southwest?.Draw();
    }
}