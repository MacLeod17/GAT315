using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEffector : Force
{
	public CircleShape shape;

	public Vector2 position { get => transform.position; set => transform.position = value; }
	public float forceMagnitude { get; set; }
	public ForceModeData.eType forceMode { get; set; }

	public override void ApplyForce(Body body)
	{
		Circle circleA = new Circle(position, shape.radius);
		//Circle circleB = new Circle(body.position, <get body shape radius>);
		Circle circleB = new Circle(body.position, ((CircleShape)body.shape).radius);
		if (circleA.Contains(circleB))
		{
			//Vector2 direction = < direction vector body position(head) from position (tail) >;
			Vector2 direction = body.position - position;
			//float distance = <distance of direction vector>;
			float distance = direction.magnitude;
			//float t = < normalized distance(0 - 1) by dividing distance by shape.radius >;
			float t = distance / shape.radius;
			//Vector2 force = < normalized direction >;
			Vector2 force = direction.normalized;

			switch (forceMode)
			{
				case ForceModeData.eType.Constant:
					//force = <force * force magnitude>;
					//force *= force.magnitude;
					force *= forceMagnitude;
					break;
				case ForceModeData.eType.InverseLinear:
					//force = <force * ((1 - t) * force magnitude)>;
					//force *= ((1 - t) * force.magnitude);
					force *= ((1 - t) * forceMagnitude);
					break;
				case ForceModeData.eType.InverseSquared:
					//force = <force * (((1 - t) * (1 - t)) * force magnitude
					//force *= (((1 - t) * (1 - t)) * force.magnitude);
					force *= (((1 - t) * (1 - t)) * forceMagnitude);
					break;
			}

			body.AddForce(force);
		}
	}
}
