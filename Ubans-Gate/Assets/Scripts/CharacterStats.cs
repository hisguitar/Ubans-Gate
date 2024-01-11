using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Character Stats", order = 1)]
public class CharacterStats : ScriptableObject
{
    // This is a scriptableObject Used to create NPC, Monster Stats

    [Header("HP")]
    public float hp = 100;
    public float maxHp = 100;

    [Header("MP")]
    public float mp = 100;
    public float maxMp = 100;

    public float moveSpeed = 15;
}