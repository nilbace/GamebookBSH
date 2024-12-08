using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using DefaultNamespace;
using DefaultNamespace.Network;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public enum GameState
    {
        Idle,
        Start,
        Choice,
        End
    }
    
    public ItemSO[] itemData = new ItemSO[6];
    
    public Inventory inventory = new Inventory();
    public GameState gameState;
    [Header("Controllers and Managers")]
    [SerializeField] private PageManager pageManager;
    [SerializeField] private NetworkSender networkSender;
    
    [Header("Events")]
    [SerializeField] private TargetEventChannelSO targetEventChannel;
    [SerializeField] private PageEventChannelSO pageEventChannel;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        gameState = GameState.Idle;
        targetEventChannel.onTargetEnterEvent.AddListener(OnTargetEnter);
        targetEventChannel.onTargetExitEvent.AddListener(OnTargetExit);
        
        pageEventChannel.onPageEnterEvent.AddListener((idx) =>
        {
            if (idx == 0)
            {
                ResetGame();
                StartGame();
            }
        });
    }
    [FormerlySerializedAs("initNetworkJsonData")]
    [Header("Network")]
    [SerializeField] NetworkJsonData resetNetworkJsonData;
    [SerializeField] NetworkJsonData startNetworkJsonData;

    private void Reset()
    {
        resetNetworkJsonData = new NetworkJsonData();
        resetNetworkJsonData.messageType = NetworkJsonData.MessageType.Reset;
        resetNetworkJsonData.time = 0;
        resetNetworkJsonData.inventory = new Inventory();
        
        startNetworkJsonData = new NetworkJsonData();
        startNetworkJsonData.messageType = NetworkJsonData.MessageType.Start;
        startNetworkJsonData.time = 0;
        startNetworkJsonData.inventory = new Inventory();
    }

    public void ResetGame()
    {
        gameState = GameState.Idle;
        inventory.Clear();
        pageManager.Initialize();
        resetNetworkJsonData.message = $"Reset Game at {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
        networkSender.SendMessage(resetNetworkJsonData);
    }
    
    public void StartGame()
    {
        gameState = GameState.Start;
        currentTime = 0f;
        startNetworkJsonData.time = currentTime;
        startNetworkJsonData.message = $"Start Game at {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
        networkSender.SendMessage(startNetworkJsonData);
    }

    [SerializeField]
    private float currentTime = 0;
    private void Update()
    {
        if (gameState == GameState.Start)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= 301)
            {
                ShowChoicePage();
            }
        }
    }

    [ContextMenu("마지막")]
    public void ShowChoicePage()
    {
        pageManager.ExitCurrent();
        pageManager.ForceEnterPage(2);
        gameState = GameState.Choice;
        NetworkJsonData timeCheckNetworkJsonData = new NetworkJsonData();
        timeCheckNetworkJsonData.messageType = NetworkJsonData.MessageType.Message;
        timeCheckNetworkJsonData.time = currentTime;
        timeCheckNetworkJsonData.message = $"Choice Page at {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
        networkSender.SendMessage(timeCheckNetworkJsonData);
    }

    public void ShowEnding(string endingName)
    {
        gameState = GameState.End;
        NetworkJsonData endNetworkJsonData = new NetworkJsonData();
        endNetworkJsonData.messageType = NetworkJsonData.MessageType.End;
        endNetworkJsonData.endName = endingName;
        endNetworkJsonData.message = $"End Game at {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
        networkSender.SendMessage(endNetworkJsonData);
    }

    private int enteredTarget = 0;
    private void OnTargetEnter(int idx)
    {
        if(gameState == GameState.Idle && idx != 0)
        {
            return;
        }
        if(enteredTarget == idx)
        {
            return;
        }
        float waitTime = (idx == 0) ? 5f : 3f;
        enteredTarget = idx;
        StartCoroutine(WaitForTarget(idx, waitTime));
    }
    
    /// <summary>
    /// 해당 시간이 지나도 그대로이면 페이지 이동
    /// </summary>
    /// <param name="idx"></param>
    /// <param name="waitTime"></param>
    /// <returns></returns>
    private IEnumerator WaitForTarget(int idx, float waitTime)
    {
        
        while (enteredTarget == idx && waitTime > 0)
        {
            waitTime -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        if(enteredTarget == idx)
        {
            pageEventChannel.onPageEnterEvent.Invoke(idx);
        }
    }
    
    private void OnTargetExit(int idx)
    {
        enteredTarget = -1;
        pageEventChannel.onPageExitEvent.Invoke(idx);
    }
}
