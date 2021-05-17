using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AABB
{
    public Vector2 center { get; set; }
    public Vector2 size { get; set; }
    public Vector2 extents { get => size * 0.5f; }

    public Vector2 min { get => center - extents; }
    public Vector2 max { get => center + extents; }

    public AABB(Vector2 center, Vector2 size)
    {
        this.center = center;
        this.size = size;
    }

    public bool Contains(Vector2 point)
    {
        return point.x >= min.x && point.x <= max.x &&
            point.y >= min.y && point.y <= max.y;
    }

    public bool Contains(AABB aABB)
    {
        return aABB.max.x >= min.x && aABB.min.x <= max.x &&
               aABB.max.y >= min.y && aABB.min.y <= max.y;
    }

    public void Draw(Color color, float width = 0.05f)
    {
        Lines.Instance.AddLine(new Vector2(min.x, min.y), new Vector2(max.x, min.y), color, width);
        Lines.Instance.AddLine(new Vector2(min.x, max.y), new Vector2(max.x, max.y), color, width);
        Lines.Instance.AddLine(new Vector2(min.x, min.y), new Vector2(min.x, max.y), color, width);
        Lines.Instance.AddLine(new Vector2(max.x, min.y), new Vector2(max.x, max.y), color, width);
    }

}