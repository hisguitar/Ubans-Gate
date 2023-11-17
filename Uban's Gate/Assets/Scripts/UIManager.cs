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

    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        BarFiller();
    }

    // Method must be public because I have to call it from inspector.
    public void UpdateUI()
    {
        hpText.text = $"{playerData.playerStats.hp}/{playerData.playerStats.maxHp}";
        mpText.text = $"{playerData.playerStats.mp}/{playerData.playerStats.maxMp}";
    }

    private void BarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerData.playerStats.hp / playerData.playerStats.maxHp, lerpSpeed);
        mpBar.fillAmount = Mathf.Lerp(mpBar.fillAmount, playerData.playerStats.mp / playerData.playerStats.maxMp, lerpSpeed);
    }
}