using UnityEngine;

namespace CrazySorting.Enraging
{
    class GameOverEnrageEffectBehaviour : EnrageEffectBehaviour
    {
        [HideInInspector] [Dependency] public IGame Game;

        void Start()
        {
            this.Inject();
        }
        
        public override void Enrage()
        {
            Game.GameOver();
        }
    }
}