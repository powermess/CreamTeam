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
            Debug.Log(string.Format("Bounds: {0}", Bounds.ToString()));
            var randomPositionInBounds = Bounds.GetRandomPositionInBounds(padding);
            Debug.Log(string.Format("Random position in bounds: {0}", randomPositionInBounds));
            return randomPositionInBounds;
        }
    }
}