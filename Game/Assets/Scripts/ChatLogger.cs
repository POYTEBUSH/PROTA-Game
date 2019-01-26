using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatLogger : MonoBehaviour {

	public static void SendChatMessage(string message, Color messageColor)
    {
        var ChatWindow = GameObject.FindGameObjectWithTag("ChatContainer");

        if (ChatWindow.transform.childCount >= 8)
            Destroy(ChatWindow.transform.GetChild(0).gameObject);

        var newMessage = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/ChatMessage"), ChatWindow.transform);
        newMessage.GetComponent<Text>().text = message;
        newMessage.GetComponent<Text>().color = messageColor;
    }
}
