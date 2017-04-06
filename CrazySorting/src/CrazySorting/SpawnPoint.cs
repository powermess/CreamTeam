using UnityEngine;

namespace CrazySorting
{
    [RequireComponent(typeof(Renderer))]
    class SpawnPoint : MonoBehaviour
    {
        Bounds mBounds;

        public Bounds Bounds {  get { return mBounds; } }

        private void Awake()
        {
            gameObject.SetActive(false);
            mBounds = GetComponent<Renderer>().bounds;
        }
    }
}
