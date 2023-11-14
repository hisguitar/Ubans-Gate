using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    /// UnityEvent cannot read any values from other scripts, it can only execute functions.
    [SerializeField] private UnityEvent updateUI;

    /// PlayerData variables can't viewed by another class, must be changed from within or using Method only.
    [Header("HP")]
    [SerializeField] private float hp = 100;
    [SerializeField] private float maxHp = 100;
    public float Hp
    {
        get => hp;
        set => hp = value;
    }
    public float MaxHp
    {
        get => maxHp;
        set => maxHp = value;
    }
    [Header("MP")]
    [SerializeField] private float mp = 100;
    [SerializeField] private float maxMp = 100;
    public float Mp {
        get => mp;
        set => mp = value;
    }
    public float MaxMp {
        get => maxMp;
        set => maxMp = value;
    }

    /// Start Method
    private void Start()
    {
        hp = maxHp;
        mp = maxMp;
    }
    /// Function method must be public because I have to call it from another class.
    public void Damage(float amount)
    {
        if(hp > 0)
        {
            hp -= amount;

            // Call UpdateUI() from UIManager
            updateUI.Invoke();
        }
        else
        {
            Debug.Log("Die");
        }
    }

    public void Heal(float amount)
    {
        if(hp < maxHp)
        {
            hp += amount;

            // Call UpdateUI() from UIManager
            updateUI.Invoke();
        }
    }

    public void DecreaseMp(float amount)
    {
        if (mp > 0 && mp >= amount)
        {
            mp -= amount;

            // Call UpdateUI() from UIManager
            updateUI.Invoke();
        }
        else
        {
            Debug.Log("Not enough MP");
        }
    }

    public void IncreaseMp(float amount)
    {
        if (mp < maxMp)
        {
            mp += amount;

            // Call UpdateUI() from UIManager
            updateUI.Invoke();
        }
    }
}