using UnityEngine;

namespace CrazySorting.CharacterEffects
{
    [RequireComponent(typeof(Character))]
    abstract class ACharacterEffect : MonoBehaviour
    {
        protected Character Character => mCharacter ?? (mCharacter = GetComponent<Character>());

        private Character mCharacter;
    }
}