using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private InventoryObject playerInventory;
    [SerializeField] private PlayerData playerData;

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
            $"STR <color=white>{playerData.PlayerStr}</color>{((playerData.Str.ToString() == "0") ? "" : $" <color=green>+ {playerData.Str}</color>")}{Environment.NewLine}" +
            $"DEF <color=white>{playerData.PlayerDef}</color>{((playerData.Def.ToString() == "0") ? "" : $" <color=green>+ {playerData.Def}</color>")}{Environment.NewLine}" +
            $"AGI <color=white>{playerData.PlayerAgi}</color>{((playerData.Agi.ToString() == "0") ? "" : $" <color=green>+ {playerData.Agi}</color>")}{Environment.NewLine}" +
            $"VIT <color=white>{playerData.PlayerVit}</color>{((playerData.Vit.ToString() == "0") ? "" : $" <color=green>+ {playerData.Vit}</color>")}{Environment.NewLine}" +
            $"INT <color=white>{playerData.PlayerInt}</color>{((playerData.Int.ToString() == "0") ? "" : $" <color=green>+ {playerData.Int}</color>")}{Environment.NewLine}" +
            $"LCK <color=white>{playerData.PlayerLck}</color>{((playerData.Lck.ToString() == "0") ? "" : $" <color=green>+ {playerData.Lck}</color>")}{Environment.NewLine}{Environment.NewLine}" +
            $"EXP <color=yellow>{playerData.Exp} / {playerData.ExpToLevelUp}</color>";

        // STATS DISPLAYED ON UI
        levelText.text = $"Lv.{playerData.Level}";
        hpText.text = $"{playerData.Hp:F0}/{playerData.MaxHp:F0}";
        mpText.text = $"{playerData.Mp:F0}/{playerData.MaxMp:F0}";
    }

    private void UIBarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerData.Hp / playerData.MaxHp, lerpSpeed);
        hpBar.fillAmount = Mathf.Clamp01(hpBar.fillAmount);

        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, playerData.Mp / playerData.MaxMp, lerpSpeed);
        mpBar.fillAmount = Mathf.Clamp01(mpBar.fillAmount);
    }
}