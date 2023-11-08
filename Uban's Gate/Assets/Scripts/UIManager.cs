using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TMP_Text hpName;
    [SerializeField] private TMP_Text hp;
    [SerializeField] private TMP_Text ap;
    [SerializeField] private Image hpBar;

    private float lerpSpeed;

    private void Start()
    {
        UpdateUI();
    }
    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        HPBarFiller();
        ColorChanger();
    }

    // Method must be public because I have to call it from inspector.
    public void UpdateUI()
    {
        hp.text = ($"{playerData.Hp} / {playerData.MaxHp}");
        ap.text = ("AP: ") + playerData.ActivityPoint;
    }

    private void HPBarFiller()
    {
        hpBar.fillAmount = Mathf.Lerp(hpBar.fillAmount, playerData.Hp / playerData.MaxHp, lerpSpeed);
    }

    private void ColorChanger()
    {
        Color redFF6969 = new Color(1.0f, 0.411f, 0.411f);
        Color greenA6FF69 = new Color(166f / 255f, 255f / 255f, 105f / 255f);

        Color hpColor = Color.Lerp(redFF6969, greenA6FF69, (playerData.Hp / playerData.MaxHp));
        hpName.color = hpColor;
        hpBar.color = hpColor;
    }
}