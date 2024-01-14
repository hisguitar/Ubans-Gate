using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    private static Player player;
    private static TooltipSystem current;
    public Tooltip tooltip;

    public void Awake()
    {
        current = this;

        player = FindObjectOfType<Player>();
    }

    public static void Show(string content, string header = "")
    {
        switch (content)
        {
            case "Level":
                content = $"Level {player.Level}";
                break;
            case "Hit Points":
                content = "Hit Points " + player.Hp + "/" + player.MaxHp;
                break;
            case "Mana Points":
                content = "Mana Points " + player.Mp + "/" + player.MaxMp;
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