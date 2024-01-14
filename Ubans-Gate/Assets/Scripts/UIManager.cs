using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InventoryObject playerInventory;
    [SerializeField] private Player player;

    [Header("STATS FROM PLAYER + EQUIPMENT + HP AND MP")]
    [SerializeField] private TMP_Text statsText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image hpBar;
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private Image mpBar;

    private float lerpSpeed;

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        UIBarFiller();
    }

    // Method must be public because I have to call it from inspector, and to create console button.
    public void UITextUpdate()
    {
        // ATTRIBUTES FROM PLAYER + EQUIPMENT
        statsText.text =
            $"STR <color=white>{player.PlayerStr}</color>{((player.Str.ToString() == "0") ? "" : $" <color=green>+ {player.Str}</color>")}{Environment.NewLine}" +
            $"DEF <color=white>{player.PlayerDef}</color>{((player.Def.ToString() == "0") ? "" : $" <color=green>+ {player.Def}</color>")}{Environment.NewLine}" +
            $"AGI <color=white>{player.PlayerAgi}</color>{((player.Agi.ToString() == "0") ? "" : $" <color=green>+ {player.Agi}</color>")}{Environment.NewLine}" +
            $"VIT <color=white>{player.PlayerVit}</color>{((player.Vit.ToString() == "0") ? "" : $" <color=green>+ {player.Vit}</color>")}{Environment.NewLine}" +
            $"INT <color=white>{player.PlayerInt}</color>{((player.Int.ToString() == "0") ? "" : $" <color=green>+ {player.Int}</color>")}{Environment.NewLine}" +
            $"LCK <color=white>{player.PlayerLck}</color>{((player.Lck.ToString() == "0") ? "" : $" <color=green>+ {player.Lck}</color>")}{Environment.NewLine}{Environment.NewLine}" +
            $"EXP <color=yellow>{player.Exp} / {player.ExpToLevelUp}</color>";

        // STATS DISPLAYED ON UI
        levelText.text = $"Lv.{player.Level}";
        hpText.text = $"{player.Hp:F0}/{player.MaxHp:F0}";
        mpText.text = $"{player.Mp:F0}/{player.MaxMp:F0}";
    }

    private void UIBarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, player.Hp / player.MaxHp, lerpSpeed);
        hpBar.fillAmount = Mathf.Clamp01(hpBar.fillAmount);

        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, player.Mp / player.MaxMp, lerpSpeed);
        mpBar.fillAmount = Mathf.Clamp01(mpBar.fillAmount);
    }
}