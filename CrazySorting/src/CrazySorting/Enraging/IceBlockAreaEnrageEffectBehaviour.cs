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
        
        //TODO: effect cannot overwrite bubble => make type of effect?
        public override void Enrage()
        {
            var character = gameObject.GetComponent<Character>();
            character.Stop();
            Game.OnCharacterDespawned(character);
            
            var characters = Game.ActiveCharacters.Where(c => c.Active && c.GetComponent<IceBlockEffect>() == null);
            characters = characters.Where(c => Vector3.Distance(c.transform.position, transform.position) < EffectRadius);
            characters.Apply(AttachEffect);
            
            transform.root.gameObject.SetActive(false);
        }

        //TODO: do this more configurably? Requires prefab to be setup in Start on iceblock, not great
        void AttachEffect(Component character)
        {
            var effect = character.gameObject.AddComponent<IceBlockEffect>();
            effect.EffectPrefab = Instantiate(Resources.Load("iceblock")) as GameObject;
            effect.TapsToBreak = 3;
            effect.Frozen = true;
        }
    }
}