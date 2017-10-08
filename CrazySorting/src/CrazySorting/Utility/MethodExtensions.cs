using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CrazySorting.Utility
{
    public static class MethodExtensions
    {
        public static bool ContainsEntireCollider(this Collider2D container, Collider2D containee)
        {
            return container.bounds.Contains(containee.bounds.min) && container.bounds.Contains(containee.bounds.max);
        }

        public static bool OverlapsColliders(this Collider2D container, Collider2D containee)
        {
            var a = new Vector2(containee.bounds.min.x, containee.bounds.min.y);
            var b = new Vector2(containee.bounds.min.x, containee.bounds.max.y);
            var c = new Vector2(containee.bounds.max.x, containee.bounds.min.y);
            var d = new Vector2(containee.bounds.max.x, containee.bounds.max.y);

            return
                container.bounds.Contains(a) ||
                container.bounds.Contains(b) ||
                container.bounds.Contains(c) ||
                container.bounds.Contains(d);
        }

        public static bool ContainsEntireCollider(this Renderer container, Renderer containee)
        {
            "bounds container: {0}, bounds containee: {1}".Log(container.bounds, containee.bounds);

            return container.bounds.Contains(containee.bounds.min) && container.bounds.Contains(containee.bounds.max);
        }

        public static void Apply<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var e in self)
            {
                action(e);
            }
        }

        public static void Log(this string self, params object[] parameters)
        {
            Debug.Log(string.Format(self, parameters));
        }

        public static T RandomElement<T>(this IEnumerable<T> self)
        {
            var ran = UnityEngine.Random.Range(0, self.Count());
            return self.ElementAt(ran);
        }
    }
}
