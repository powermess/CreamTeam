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
    
    class GameOverEnrageEffectBehaviour : EnrageEffectBehaviour
    {
        [HideInInspector] [Dependency] public Game Game;

        void Start()
        {
            this.Inject();
        }
        
        public override void Enrage()
        {
            Game.GameOver();
        }
    }

    public interface IEnrageEffect
    {
        void Enrage();
    }
}