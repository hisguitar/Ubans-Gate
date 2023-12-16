using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private UnityEvent UITextUpdate;

    // STATS FROM PLAYER
    public int PlayerStr { get; private set; } = 10;
    public int PlayerDef { get; private set; } = 10;
    public int PlayerAgi { get; private set; } = 10;
    public int PlayerVit { get; private set; } = 10;
    public int PlayerInt { get; private set; } = 10;
    public int PlayerCha { get; private set; } = 10;
    public int PlayerLck { get; private set; } = 10;

    // STATS FROM EQUIPMENT
    public int Str { get; private set; }
    public int Def { get; private set; }
    public int Agi { get; private set; }
    public int Vit { get; private set; }
    public int Int { get; private set; }
    public int Cha { get; private set; }
    public int Lck { get; private set; }

    // EXP AND LEVEL
    public int Level { get; private set; } = 1;
    public int Exp { get; private set; } = 0;
    public int ExpToLevelUp { get; private set; } = 100;

    // HP AND MP
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
    // Upgrade-stats method for update attributes value
    /* These functions cannot be combined into a single state because of the different values. must work separately */
    // Str
    public void UpdateStrength(int modifiedValue)
    {
        Str = modifiedValue;
        UITextUpdate.Invoke();
    }
    // Def
    public void UpdateDefense(int modifiedValue)
    {
        Def = modifiedValue;
        UITextUpdate.Invoke();
    }
    // Agi
    public void UpdateAgility(int modifiedValue)
    {
        Agi = modifiedValue;
        UITextUpdate.Invoke();
    }
    // Vit
    public void UpdateVitality(int modifiedValue)
    {
        Vit = modifiedValue;
        UITextUpdate.Invoke();
    }
    // Int
    public void UpdateIntelligence(int modifiedValue)
    {
        Int = modifiedValue;
        UITextUpdate.Invoke();
    }
    // Cha
    public void UpdateCharisma(int modifiedValue)
    {
        Cha = modifiedValue;
        UITextUpdate.Invoke();
    }
    // Lck
    public void UpdateLuck(int modifiedValue)
    {
        Lck = modifiedValue;
        UITextUpdate.Invoke();
    }
    #endregion
    #region In-combat
    public void GainExp(int amount)
    {
        Exp += amount;

        while (Exp >= ExpToLevelUp)
        {
            LevelUp();
        }
    }

    private int CalculateExpToLevelUp()
    {
        return 100 * Level;
    }

    private void LevelUp()
    {
        // Why use while and not if: The use of while is intended to make it possible to level up multiple levels in a single move if the player has enough Exp to skip multiple levels. Use while to check conditions. And so on until it is false.
        Level++;
        Exp -= ExpToLevelUp;
        ExpToLevelUp = CalculateExpToLevelUp();
        // Do other things when LevelUp
    }

    public void TakeDamage(float amount)
    {
        int averageDef = (PlayerDef + Def) / 2;
        Hp -= amount - averageDef;
        if (Hp < 1)
        {
            Debug.Log("Die");
            Hp = 0;
        }
        UITextUpdate.Invoke();
    }

    public void Heal(float amount)
    {
        if (Hp < MaxHp)
        {
            Hp += amount + (PlayerInt + Int);
        }
        if (Hp > MaxHp)
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
        if (Mp > MaxMp)
        {
            Mp = MaxMp;
        }
        UITextUpdate.Invoke();
    }
    #endregion
}