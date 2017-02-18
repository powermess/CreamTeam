using CrazySorting.Utility;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
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
        var characterCollider = character.GetComponent<BoxCollider2D>();
        var prevEnabledState = characterCollider.enabled;

        //make sure to enable collider if disabled
        characterCollider.enabled = true;
        var res = mCollider.ContainsEntireCollider(characterCollider);
        characterCollider.enabled = prevEnabledState;

        return res;
    }

    public bool IsCharacterNearGoal(Character character)
    {
        var characterCollider = character.GetComponent<BoxCollider2D>();
        var prevEnabledState = characterCollider.enabled;

        //make sure to enable collider if disabled
        characterCollider.enabled = true;
        var res = mCollider.OverlapsColliders(characterCollider);
        characterCollider.enabled = prevEnabledState;

        return res;
    }

    public void MoveCharacterIntoGoal(Character character)
    {
        var x = Mathf.Clamp(character.transform.position.x, mCollider.bounds.min.x + 0.5f, mCollider.bounds.max.x - 0.5f);
        var y = Mathf.Clamp(character.transform.position.y, mCollider.bounds.min.y + 0.5f, mCollider.bounds.max.y - 0.5f);

        character.transform.position = new Vector2(x,y);
    }
}
