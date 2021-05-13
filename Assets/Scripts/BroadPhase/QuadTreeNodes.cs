using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadtreeNode
{
	AABB aabb;
	int capacity;
	List<Body> bodies;
	bool subdivided = false;

	QuadtreeNode northeast;
	QuadtreeNode northwest;
	QuadtreeNode southeast;
	QuadtreeNode southwest;

	public QuadtreeNode(AABB aabb, int capacity)
	{
		this.aabb = aabb;
		this.capacity = capacity;

		bodies = new List<Body>();
	}

	public void Insert(Body body)
	{
		if (!aabb.Contains(body.shape.aabb)) return;

		if (bodies.Count < capacity)
		{
			bodies.Add(body);
		}
		else
		{
			if (!subdivided)
			{
				Subdivide();
			}

			northeast.Insert(body);
			northwest.Insert(body);
			southeast.Insert(body);
			southwest.Insert(body);
		}
	}

	private void Subdivide()
	{
		float xo = aabb.extents.x * 0.5f;
		float yo = aabb.extents.y * 0.5f;

		northeast = new QuadtreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y + yo), aabb.extents), capacity);
		northwest = new QuadtreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y + yo), aabb.extents), capacity);
		southeast = new QuadtreeNode(new AABB(new Vector2(aabb.center.x - xo, aabb.center.y - yo), aabb.extents), capacity);
		southwest = new QuadtreeNode(new AABB(new Vector2(aabb.center.x + xo, aabb.center.y - yo), aabb.extents), capacity);

		subdivided = true;
	}
}