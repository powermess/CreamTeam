using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class UnityExtensionMethods
{
    public static Vector2 GetRandomPositionInBounds(this Bounds bounds, float padding = 0)
    {
        var minX = bounds.min.x + padding;
        var minY = bounds.min.y + padding;
        var maxX = bounds.max.x - padding;
        var maxY = bounds.max.y - padding;

        var randX = UnityEngine.Random.Range(minX, maxX);
        var randY = UnityEngine.Random.Range(minY, maxY);

        return new Vector2(randX, randY);
    }
}
