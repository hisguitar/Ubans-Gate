using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [Header("HP")]
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private Image hpBar;

    [Header("MP")]
    [SerializeField] private TMP_Text mpText;
    [SerializeField] private Image mpBar;

    private float lerpSpeed;

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        UIBarFiller();
    }

    // Method must be public because I have to call it from inspector.
    public void UITextUpdate()
    {
        hpText.text = playerData.playerStats.hp + "/" + playerData.playerStats.maxHp;
        mpText.text = playerData.playerStats.mp + "/" + playerData.playerStats.maxMp;
    }

    private void UIBarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerData.playerStats.hp / playerData.playerStats.maxHp, lerpSpeed);
        hpBar.fillAmount = Mathf.Clamp01(hpBar.fillAmount);

        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, playerData.playerStats.mp / playerData.playerStats.maxMp, lerpSpeed);
        mpBar.fillAmount = Mathf.Clamp01(mpBar.fillAmount);
    }
}