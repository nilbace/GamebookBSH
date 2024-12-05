using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public GameObject[] chatMessages;

    private int currentChatIndex;
    public int CurrentChatIndex
    {
        get => currentChatIndex;
        set
        {
            currentChatIndex = value;
            OnChatIndexChange(value);
        }
    }
    
    private void Start()
    {
        CurrentChatIndex = 0;
    }

    [ContextMenu("Initialize")]
    public void Initialize()
    {
        CurrentChatIndex = 0;
        chatMessages[0].SetActive(true);
        for(int i = 1; i < chatMessages.Length; i++)
        {
            chatMessages[i].SetActive(false);
        }
    }
    
    public void HideAll()
    {
        for (int i = 0; i < chatMessages.Length; i++)
        {
            chatMessages[i].SetActive(false);
        }
    }

    public void OnTimePassed(int hour)
    {
        if (CurrentChatIndex <= hour && hour < chatMessages.Length)
        {
            CurrentChatIndex = hour;
        }
    }
    private void OnChatIndexChange(int idx)
    {
        for (int i = 0; i <= idx; i++)
        {
            chatMessages[i].SetActive(true);
        }
    }
}
