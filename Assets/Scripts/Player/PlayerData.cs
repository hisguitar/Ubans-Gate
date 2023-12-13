using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private UnityEvent UITextUpdate;

    [Header("ATTRIBUTES")]
    public int Str = 10;
    public int Def = 10;
    public int Agi = 10;
    public int Vit = 10;
    public int Int = 10;
    public int Cha = 10;
    public int Lck = 10;

    [Header("UI STATS")]
    public float hp;
    public float maxHp;
    public float mp;
    public float maxMp;

    private void Start()
    {
        // Default attributes to stats
        maxHp = Vit * 10;
        maxMp = Int * 10;

        // Default stats
        hp = maxHp;
        mp = maxMp;

        // Update UI when Start()
        UITextUpdate.Invoke();
    }

    #region Upgrade-stats
    // Vitality afftects "HP Value"
    public void UpdateVitality(int vitality)
    {
        Vit += vitality;

        Debug.Log("Update MaxHp");
        maxHp += (vitality * 10);
        UITextUpdate.Invoke();
    }
    // Intelligence affects "Damage rate of magic skills" and "MP"
    public void UpdateIntelligence(int intelligence)
    {
        Int += intelligence;

        Debug.Log("Update MaxMp");
        maxMp += (intelligence * 10);
        UITextUpdate.Invoke();
    }
    #endregion
    #region In-combat
    public void TakeDamage(float amount)
    {
        if (hp < 1)
        {
            Debug.Log("Die");
        }
        else
        {
            hp -= amount;
            UITextUpdate.Invoke();

            if (hp < 1)
            {
                Debug.Log("Die");
            }
        }
    }

    public void Heal(float amount)
    {
        if (hp < maxHp)
        {
            hp += amount;
            UITextUpdate.Invoke();
        }
    }

    public void DecreaseMp(float amount)
    {
        if (mp < amount)
        {
            Debug.Log("Not enough MP");
        }
        else
        {
            mp -= amount;
            UITextUpdate.Invoke();
        }
    }

    public void IncreaseMp(float amount)
    {
        if (mp < maxMp)
        {
            mp += amount;
            UITextUpdate.Invoke();
        }
    }
    #endregion
}