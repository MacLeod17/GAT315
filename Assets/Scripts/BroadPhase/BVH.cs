using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BVH : BroadPhase
{
	BVHNode rootNode;

	public override void Build(AABB aabb, List<Body> bodies)
	{
		potentialCollisionCount = 0;
		List<Body> sorted = new List<Body>(bodies);

		// sort bodies along x-axis (position.x)
		//sorted.Sort(< https://stackoverflow.com/questions/24187287/c-sharp-listt-orderby-float-member>);
		sorted.Sort((x, y) => x.position.x.CompareTo(y.position.x));

		// set sorted bodies to root bvh node
		rootNode = new BVHNode(sorted);
	}

	public override void Query(AABB aabb, List<Body> bodies)
	{
		rootNode.Query(aabb, bodies);
		// update the number of potential collisions
		potentialCollisionCount += bodies.Count;
	}

	public override void Query(Body body, List<Body> bodies)
	{
		//Query(< get body shape aabb >, bodies);
		rootNode.Query(body.shape.aABB, bodies);
	}

	public override void Draw()
	{
		//< check if the root node is not null below, use ? operator>

		//< call Draw() on root node >
		rootNode?.Draw();
	}
}
