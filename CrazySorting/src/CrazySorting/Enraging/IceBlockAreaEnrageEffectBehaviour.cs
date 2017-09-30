using System.Linq;
using CrazySorting.CharacterEffects;
using CrazySorting.Utility;
using UnityEngine;

namespace CrazySorting.Enraging
{
    class IceBlockAreaEnrageEffectBehaviour : EnrageEffectBehaviour
    {
        [HideInInspector] [Dependency] public IGame Game;


        void Start()
        {
            this.Inject();
        }

        public float EffectRadius = 3f;
        public override void Enrage()
        {
            var character = gameObject.GetComponent<Character>();
            character.Stop();
            Game.OnCharacterDespawned(character);
            
            var characters = Game.ActiveCharacters.Where(c => c.Active && c.GetComponent<IceBlockEffect>() == null);
            characters = characters.Where(c => Vector3.Distance(c.transform.position, transform.position) < EffectRadius);
            characters.Apply(c => c.gameObject.AddComponent<IceBlockEffect>());
            
            transform.root.gameObject.SetActive(false);
        }
    }
}