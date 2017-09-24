using UnityEngine;

namespace CrazySorting.Enraging
{
    [RequireComponent(typeof(Character))]
    abstract class EnrageEffectBehaviour : MonoBehaviour, IEnrageEffect
    {
        void Awake()
        {
            GetComponent<Character>().Register(this);
        }
        public abstract void Enrage();
    }
}