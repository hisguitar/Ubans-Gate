using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public ModifiableInt playerHP;
    public ModifiableInt playerMP;
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
        if (playerStats.hp < 1)
        {
            Debug.Log("Die");
        }
        else
        {
            playerStats.hp -= amount;
            UITextUpdate.Invoke();

            if (playerStats.hp < 1)
            {
                Debug.Log("Die");
            }
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
        if (playerStats.mp < amount)
        {
            Debug.Log("Not enough MP");
        }
        else
        {
            playerStats.mp -= amount;
            UITextUpdate.Invoke();
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