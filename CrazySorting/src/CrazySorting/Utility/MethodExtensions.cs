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

        public static void Apply<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach(var e in self)
            {
                action(e);
            }
        }

        public static void Log(this string self, params object[] parameters)
        {
            Debug.Log(string.Format(self, parameters));
        }
    }
}
