using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "Player Stats", order = 1)]
public class PlayerStats : ScriptableObject
{
    // This is a scriptableObject Used to create player stats only. (only player!)

    [Header("HP")]
    public float hp = 100;
    public float maxHp = 100;

    [Header("MP")]
    public float mp = 100;
    public float maxMp = 100;

    public float moveSpeed = 15;

    public float jumpSpeed = 15;
}