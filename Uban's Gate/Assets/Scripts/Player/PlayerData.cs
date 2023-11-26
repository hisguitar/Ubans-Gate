using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    // Create playerStats by using ScriptableObject "CharacterStats"
    public PlayerStats playerStats;
    [SerializeField] private UnityEvent UITextUpdate;

    private void Start()
    {
        playerStats.hp = playerStats.maxHp;
        playerStats.mp = playerStats.maxMp;
    }

    public void Damage(float amount)
    {
        if (playerStats.hp > 0)
        {
            playerStats.hp -= amount;
            UITextUpdate.Invoke();
        }
        else
        {
            Debug.Log("Die");
        }
    }

    public void Heal(float amount)
    {
        if (playerStats.hp < playerStats.maxHp)
        {
            playerStats.hp += amount;
            UITextUpdate.Invoke();
        }
    }

    public void DecreaseMp(float amount)
    {
        if (playerStats.mp > 0 && playerStats.mp >= amount)
        {
            playerStats.mp -= amount;
            UITextUpdate.Invoke();
        }
        else
        {
            Debug.Log("Not enough MP");
        }
    }

    public void IncreaseMp(float amount)
    {
        if (playerStats.mp < playerStats.maxMp)
        {
            playerStats.mp += amount;
            UITextUpdate.Invoke();
        }
    }
}