using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [Header("SEND MESSAGE")]
    [SerializeField] private string username;
    [SerializeField] private int maxMessages = 25;
    [SerializeField] private GameObject chatPanel, chatTextPrefab;
    [SerializeField] private TMP_InputField chatBox;
    [SerializeField] private Color playerMessage, info;
    [SerializeField] private List<Message> messageList = new List<Message>();

    private float lerpSpeed;

    private void Update()
    {
        if (chatBox.text != "")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SendMessageToChat(GetFormattedTimestamp() + username + " : " + chatBox.text, Message.MessageType.playerMessage);
                chatBox.text = "";
            }
        }
        else
        {
            if (!chatBox.isFocused && Input.GetKeyDown(KeyCode.Return))
            {
                chatBox.ActivateInputField();
            }
        }

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
            $"Lck : <color=white>{playerData.PlayerLck}</color>{((playerData.Lck.ToString() == "0") ? "" : $" <color=green>+ {playerData.Lck}</color>")}{Environment.NewLine}{Environment.NewLine}" +
            $"Exp : <color=yellow>{playerData.Exp} / {playerData.ExpToLevelUp}</color>";

        // STATS DISPLAYED ON UI
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

    public void InfoMessage()
    {
        SendMessageToChat("Inventory Full! Cannot add more items.", Message.MessageType.info);
    }

    public void SendMessageToChat(string text, Message.MessageType messageType)
    {
        // Limit amount of messages in chat box
        if (messageList.Count >= maxMessages)
        {
            Destroy(messageList[0].chatTextPrefab.gameObject);
            messageList.Remove(messageList[0]);
        }

        Message newMessage = new Message();
        newMessage.text = text;
        GameObject newText = Instantiate(chatTextPrefab, chatPanel.transform);
        newMessage.chatTextPrefab = newText.GetComponent<TMP_Text>();
        newMessage.chatTextPrefab.text = newMessage.text;
        newMessage.chatTextPrefab.color = MessageTypeColor(messageType);
        messageList.Add(newMessage);
    }

    Color MessageTypeColor(Message.MessageType messageType)
    {
        Color color;

        switch (messageType)
        {
            case Message.MessageType.playerMessage:
                color = playerMessage;
                break;

            default:
                color = info;
                break;
        }
        return color;
    }

    private string GetFormattedTimestamp()
    {
        DateTime now = DateTime.Now;
        string formattedTime = $"[{now.Hour:D2}:{now.Minute:D2}] ";
        return formattedTime;
    }
}

[System.Serializable]
public class Message
{
    public string text;
    public TMP_Text chatTextPrefab;
    public MessageType messageType;

    public enum MessageType
    {
        info,
        playerMessage
    }
}