using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [Header("STATS FROM PLAYER + EQUIPMENT")]
    [SerializeField] private TMP_Text statsText;

    [Header("HP AND MP")]
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
            $"Str : <color=white>{playerData.PlayerStr}</color>{((playerData.Str.ToString() == "0") ? "" : $" <color=green>+ {playerData.Str}</color>")}{Environment.NewLine}" +
            $"Def : <color=white>{playerData.PlayerDef}</color>{((playerData.Def.ToString() == "0") ? "" : $" <color=green>+ {playerData.Def}</color>")}{Environment.NewLine}" +
            $"Agi : <color=white>{playerData.PlayerAgi}</color>{((playerData.Agi.ToString() == "0") ? "" : $" <color=green>+ {playerData.Agi}</color>")}{Environment.NewLine}" +
            $"Vit : <color=white>{playerData.PlayerVit}</color>{((playerData.Vit.ToString() == "0") ? "" : $" <color=green>+ {playerData.Vit}</color>")}{Environment.NewLine}" +
            $"Int : <color=white>{playerData.PlayerInt}</color>{((playerData.Int.ToString() == "0") ? "" : $" <color=green>+ {playerData.Int}</color>")}{Environment.NewLine}" +
            $"Cha : <color=white>{playerData.PlayerCha}</color>{((playerData.Cha.ToString() == "0") ? "" : $" <color=green>+ {playerData.Cha}</color>")}{Environment.NewLine}" +
            $"Lck : <color=white>{playerData.PlayerLck}</color>{((playerData.Lck.ToString() == "0") ? "" : $" <color=green>+ {playerData.Lck}</color>")}{Environment.NewLine}" +
            $"Exp : <color=yellow>{playerData.Exp}/{playerData.ExpToLevelUp}</color>";

        // STATS DISPLAYED ON UI
        hpText.text = $"{playerData.Hp:F0} / {playerData.MaxHp:F0}";
        mpText.text = $"{playerData.Mp:F0} / {playerData.MaxMp:F0}";
    }

    private void UIBarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerData.Hp / playerData.MaxHp, lerpSpeed);
        hpBar.fillAmount = Mathf.Clamp01(hpBar.fillAmount);

        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, playerData.Mp / playerData.MaxMp, lerpSpeed);
        mpBar.fillAmount = Mathf.Clamp01(mpBar.fillAmount);
    }
}