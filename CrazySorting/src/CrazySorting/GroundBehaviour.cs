using UnityEngine;

namespace CrazySorting.Utility
{
    [RequireComponent(typeof(Bounds))]
    public class GroundBehaviour : MonoBehaviour
    {
        [HideInInspector]
        public Bounds Bounds {
            get { return GetComponent<Renderer>().bounds; } 
        }

        public Vector2 GetRandomPositionOnGround(float padding = 0)
        {
            var randomPositionInBounds = Bounds.GetRandomPositionInBounds(padding);
            return randomPositionInBounds;
        }
    }
}