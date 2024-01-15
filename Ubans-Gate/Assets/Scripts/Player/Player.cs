using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameObject floatingTextPrefab;

    // Weapon Lists
    public GameObject BeginnerStaffPrefab;

    // Exp and Level
    public int Level { get; private set; } = 1;
    public int Exp { get; private set; } = 0;
    public int ExpToLevelUp { get; private set; } = 100;

    // Hp and Mp
    public float Hp { get; private set; }
    public float MaxHp => (PlayerVit + Vit) * 10;
    public float Mp { get; private set; }
    public float MaxMp => (PlayerInt + Int) * 10;

    // Stats from player
    public int PlayerStr { get; private set; } = 10;
    public int PlayerDef { get; private set; } = 10;
    public int PlayerAgi { get; private set; } = 10;
    public int PlayerVit { get; private set; } = 10;
    public int PlayerInt { get; private set; } = 10;
    public int PlayerLck { get; private set; } = 10; // need an idea

    // Stats from equipment
    public int Str { get; private set; }
    public int Def { get; private set; }
    public int Agi { get; private set; }
    public int Vit { get; private set; }
    public int Int { get; private set; }
    public int Lck { get; private set; }

    // Random damage between "PlayerDamage - 5" and "PlayerDamage + 5" in enemy's TakeDamage
    public float PlayerDamage => (PlayerStr + Str);

    private void Start()
    {
        UIManager = FindObjectOfType<UIManager>();

        // Default stats
        Hp = MaxHp;
        Mp = MaxMp;

        // Update UI when Start()
        UIManager.UITextUpdate();
    }

    #region Save and Load
    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
        Debug.Log("Player data Saved");
    }

    public void LoadPlayer()
    {
        // Load playerData from SaveSystem
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData != null)
        {
            Level = playerData.Level;
            Exp = playerData.Exp;

            Hp = playerData.Hp;
            Mp = playerData.Mp;

            PlayerStr = playerData.Str;
            PlayerDef = playerData.Def;
            PlayerAgi = playerData.Agi;
            PlayerVit = playerData.Vit;
            PlayerInt = playerData.Int;
            PlayerLck = playerData.Lck;
            
            UIManager.UITextUpdate();
            Debug.Log("Player data Loaded");

            SavePlayer();
        }
        else
        {
            Debug.LogError("Unable to load player data.");
        }
    }
    #endregion

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
        // Gain Exp
        Exp += amount;
        if (floatingTextPrefab != null && Exp < ExpToLevelUp)
        {
            ShowFloatingText($"+{amount} EXP", new Color(255f/255f, 235f/255f, 0f/255f)); //FFEA00 (RGB 0-255)
        }

        // Level Up!
        while (Exp >= ExpToLevelUp)
        {
            // Why use while and not if: The use of while is intended to make it possible to level up multiple levels in a single move if the player has enough Exp to skip multiple levels. Use while to check conditions. And so on until it is false.
            Level++;
            Exp -= ExpToLevelUp;
            ExpToLevelUp = CalculateExpToLevelUp();

            // Do other things when LevelUp
            // Show floating text
            if (floatingTextPrefab != null)
            {
                ShowFloatingText($"Level up to {Level}!", new Color(255f/255f, 235f/255f, 0f/255f)); //FFEA00 (RGB 0-255)
            }
            // 2 levels : +1 Increase all stats
            if (Level % 2 == 0)
            {
                PlayerStr += 1;
                PlayerDef += 1;
                PlayerAgi += 1;
                PlayerVit += 1;
                PlayerInt += 1;
                PlayerLck += 1;
            }
        }
        UIManager.UITextUpdate();
    }

    private int CalculateExpToLevelUp()
    {
        return 100 * Level;
    }

    public void TakeDamage(float amount)
    {
        #region Calculate Damage
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
        // Round to the nearest integer
        totalDamage = Mathf.RoundToInt(totalDamage);

        // Random damage modifier
        int randomDamage = (int)Random.Range(-5f, 5f); // Note that Random.Range does not include the maximum value.
        #endregion
        #region Take Damage
        // Take Damage
        Hp -= (totalDamage + randomDamage);
        if (floatingTextPrefab != null)
        {
            ShowFloatingText($"-{totalDamage + randomDamage} HP", new Color(255f/255f, 20f/255f, 0f/255f)); //FF1400 (RGB 0-255)
        }

        // Check if player die
        if (Hp < 1)
        {
            Debug.Log("Die");
            Hp = 0;
        }

        // Update UI
        UIManager.UITextUpdate();
        #endregion
    }

    public void Heal(float amount)
    {
        float totalHeal = amount + (PlayerInt + Int);
        Hp += totalHeal;
        if (floatingTextPrefab != null)
        {
            ShowFloatingText($"+{totalHeal} HP", new Color(145f/255f, 255f/255f, 0f/255f)); //91FF40 (RGB 0-255)
        }
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
            if (floatingTextPrefab != null)
            {
                ShowFloatingText($"-{amount} MP", new Color(0f/255f, 110f/255f, 255f/255f)); //0055FF (RGB 0-255)
            }
        }
        UIManager.UITextUpdate();
    }

    public void IncreaseMp(float amount)
    {
        if (Mp < MaxMp)
        {
            Mp += amount;
            if (floatingTextPrefab != null)
            {
                ShowFloatingText($"+{amount} MP", new Color(255f/255f, 85f/255f, 255f/255f)); //40A0FF (RGB 0-255)
            }
        }
        if (Mp > MaxMp)
        {
            Mp = MaxMp;
        }
        UIManager.UITextUpdate();
    }
    #endregion

    private void ShowFloatingText(string text, Color textColor)
    {
        GameObject go = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
        go.GetComponent<TMP_Text>().color = textColor;
        go.GetComponent<TMP_Text>().text = text;
    }
}