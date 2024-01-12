using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static PlayerData playerData;
    private static TooltipSystem current;
    public Tooltip tooltip;

    public void Awake()
    {
        current = this;

        playerData = FindObjectOfType<PlayerData>();
    }

    public static void Show(string content, string header = "")
    {
        switch (content)
        {
            case "Level":
                content = $"Level {playerData.Level}";
                break;
            case "Hit Points":
                content = "Hit Points " + playerData.Hp + "/" + playerData.MaxHp;
                break;
            case "Mana Points":
                content = "Mana Points " + playerData.Mp + "/" + playerData.MaxMp;
                break;
        }

        current.tooltip.SetText(content, header);
        current.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        current.tooltip.gameObject.SetActive(false);
    }
}