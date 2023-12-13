using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private UnityEvent UITextUpdate;

    // ATTRIBUTES FROM PLAYER
    public int PlayerStr { get; private set; } = 10;
    public int PlayerDef { get; private set; } = 10;
    public int PlayerAgi { get; private set; } = 10;
    public int PlayerVit { get; private set; } = 10;
    public int PlayerInt { get; private set; } = 10;
    public int PlayerCha { get; private set; } = 10;
    public int PlayerLck { get; private set; } = 10;

    // ATTRIBUTES FROM EQUIPMENT
    public int Str { get; private set; }
    public int Def { get; private set; }
    public int Agi { get; private set; }
    public int Vit { get; private set; }
    public int Int { get; private set; }
    public int Cha { get; private set; }
    public int Lck { get; private set; }

    // STATS DISPLAYED ON UI
    public float Hp { get; private set; }
    public float MaxHp => (PlayerVit + Vit) * 10;
    public float Mp { get; private set; }
    public float MaxMp => (PlayerInt + Int) * 10;

    private void Start()
    {
        // Default stats
        Hp = MaxHp;
        Mp = MaxMp;

        // Update UI when Start()
        UITextUpdate.Invoke();
    }

    #region Upgrade-stats
    // Vitality(Vit) afftects "HP Value"
    public void UpdateVitality(int vitality)
    {
        Vit = vitality;
        UITextUpdate.Invoke();
    }

    // Intelligence(Int) affects "Damage rate of magic skills" and "MP"
    public void UpdateIntelligence(int intelligence)
    {
        Int = intelligence;
        UITextUpdate.Invoke();
    }
    #endregion
    #region In-combat
    public void TakeDamage(float amount)
    {
        Hp -= amount;
        if (Hp < 1)
        {
            Debug.Log("Die");
        }
        UITextUpdate.Invoke();
    }

    public void Heal(float amount)
    {
        if (Hp < MaxHp)
        {
            Hp += amount;
        }
        else
        {
            Hp = MaxHp;
        }
        UITextUpdate.Invoke();
    }

    public void DecreaseMp(float amount)
    {
        if (Mp < amount)
        {
            Debug.Log("Not enough MP");
        }
        else
        {
            Mp -= amount;
        }
        UITextUpdate.Invoke();
    }

    public void IncreaseMp(float amount)
    {
        if (Mp < MaxMp)
        {
            Mp += amount;
        }
        else
        {
            Mp = MaxMp;
        }
        UITextUpdate.Invoke();
    }
    #endregion
}