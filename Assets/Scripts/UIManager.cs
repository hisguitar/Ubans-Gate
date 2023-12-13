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
        hpText.text = playerData.Hp + "/" + playerData.MaxHp;
        mpText.text = playerData.Mp + "/" + playerData.MaxMp;
    }

    private void UIBarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerData.Hp / playerData.MaxHp, lerpSpeed);
        hpBar.fillAmount = Mathf.Clamp01(hpBar.fillAmount);

        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, playerData.Mp / playerData.MaxMp, lerpSpeed);
        mpBar.fillAmount = Mathf.Clamp01(mpBar.fillAmount);
    }
}