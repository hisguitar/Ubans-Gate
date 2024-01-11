using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;

    // STATS FROM PLAYER
    public int PlayerStr { get; private set; } = 10; // T
    public int PlayerDef { get; private set; } = 10; // T
    public int PlayerAgi { get; private set; } = 10; // T
    public int PlayerVit { get; private set; } = 10; // T
    public int PlayerInt { get; private set; } = 10; // T
    public int PlayerLck { get; private set; } = 10;

    // STATS FROM EQUIPMENT
    public int Str { get; private set; }
    public int Def { get; private set; }
    public int Agi { get; private set; }
    public int Vit { get; private set; }
    public int Int { get; private set; }
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

    // Random this variable between "PlayerDamage - 5" and "PlayerDamage + 5" in enemy's TakeDamage
    public float PlayerDamage => (PlayerStr + Str) * 10;

    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();

        // Default stats
        Hp = MaxHp;
        Mp = MaxMp;

        // Update UI when Start()
        UIManager.UITextUpdate();
    }

    #region Upgrade-stats
    // Upgrade-stats method for update attributes value
    /* These functions cannot be combined into a single state because of the different values. must work separately */
    // Str
    public void UpdateStrength(int modifiedValue)
    {
        Str = modifiedValue;
        UIManager.UITextUpdate();
    }
    // Def
    public void UpdateDefense(int modifiedValue)
    {
        Def = modifiedValue;
        UIManager.UITextUpdate();
    }
    // Agi
    public void UpdateAgility(int modifiedValue)
    {
        Agi = modifiedValue;
        UIManager.UITextUpdate();
    }
    // Vit
    public void UpdateVitality(int modifiedValue)
    {
        Vit = modifiedValue;
        UIManager.UITextUpdate();
    }
    // Int
    public void UpdateIntelligence(int modifiedValue)
    {
        Int = modifiedValue;
        UIManager.UITextUpdate();
    }
    // Lck
    public void UpdateLuck(int modifiedValue)
    {
        Lck = modifiedValue;
        UIManager.UITextUpdate();
    }
    #endregion
    #region In-combat
    public void GainExp(int amount)
    {
        Exp += amount;
        // Level Up!
        while (Exp >= ExpToLevelUp)
        {
            // Why use while and not if: The use of while is intended to make it possible to level up multiple levels in a single move if the player has enough Exp to skip multiple levels. Use while to check conditions. And so on until it is false.
            Level++;
            Exp -= ExpToLevelUp;
            ExpToLevelUp = CalculateExpToLevelUp();

            // Do other things when LevelUp
        }
        UIManager.UITextUpdate();
    }

    private int CalculateExpToLevelUp()
    {
        return 100 * Level;
    }

    public void TakeDamage(float amount)
    {
        // Calculate damage rate
        float totalDamage = amount;
        float damageRate = totalDamage / 200;

        /// With a maximum level of 90
        /// In case of player spends all points on "Def", The maximum of "PlayerDef" is 10 + 178 = 188 (Doesn't include stats from equipment.)
        /// totalDef 200 = 100% (2 points of Def : 1% of defPercent)

        // Calculates Def percentage
        int totalDef = (PlayerDef + Def);
        float defPercentage = totalDef * damageRate;

        // if percentage >= damage 99% of totalDamage, set totalDamage = 1%
        if (defPercentage >= 0.99f * totalDamage)
        {
            totalDamage = 0.01f * totalDamage;
        }
        else
        {
            totalDamage -= defPercentage;
        }

        // Take Damage
        Hp -= totalDamage;

        // Check if player die
        if (Hp < 1)
        {
            Debug.Log("Die");
            Hp = 0;
        }

        // Update UI
        UIManager.UITextUpdate();
    }

    public void Heal(float amount)
    {
        Hp += amount + (PlayerInt + Int);
        if (Hp > MaxHp)
        {
            Hp = MaxHp;
        }
        UIManager.UITextUpdate();
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
        UIManager.UITextUpdate();
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
        UIManager.UITextUpdate();
    }
    #endregion
}