using CrazySorting.Utility;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
class Goal : MonoBehaviour
{
    public Faction Faction;

    Collider2D mCollider;

    void Awake()
    {
        mCollider = GetComponent<Collider2D>();
    }

    public bool IsCharacterInGoal(Character character)
    {
        var characterCollider = character.GetComponent<Collider2D>();
        return mCollider.ContainsEntireCollider(characterCollider);
    }
}
