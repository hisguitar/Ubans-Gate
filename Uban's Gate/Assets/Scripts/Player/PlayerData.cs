using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    /// UnityEvent cannot read any values from other scripts, it can only execute functions.
    [SerializeField] private UnityEvent updateUI;

    /// PlayerData variables can't viewed by another class, must be changed from within or using Method only.
    [SerializeField] private float hp = 10, maxHp = 10;
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
    [SerializeField] private bool isBattle = false;
    public bool IsBattle
    {
        get => isBattle;
        set => isBattle = value;
    }
    [SerializeField] private int activityPoint = 3;
    public int ActivityPoint
    {
        get => activityPoint;
        set => activityPoint = value;
    }

    private void Start()
    {
        hp = maxHp;
    }
    /// Function method must be public because I have to call it from another class.
    public void Damage(float damage)
    {
        if(hp > 0)
        {
            hp -= damage;

            // Call UpdateUI() from UIManager
            updateUI.Invoke();
        }
    }

    public void Heal(float healing)
    {
        if(hp < maxHp)
        {
            hp += healing;

            // Call UpdateUI() from UIManager
            updateUI.Invoke();
        }
    }

    public void SwitchIsBattle()
    {
        isBattle = !isBattle;
    }

    public void SetActivityPoint(int delta)
    {
        activityPoint += delta;
        if (activityPoint < 0)
        {
            activityPoint = 0;
        }

        // Call UpdateUI() from UIManager
        updateUI.Invoke();
    }
}