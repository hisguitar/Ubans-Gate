using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private UnityEvent UITextUpdate;

    [Header("HP")]
    public float hp = 100;
    public float maxHp = 100;

    [Header("MP")]
    public float mp = 100;
    public float maxMp = 100;

    [Header("OTHER STATS")]
    public float moveSpeed = 10;

    private void Start()
    {
        hp = maxHp;
        mp = maxMp;
    }

    public void Damage(float amount)
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
}