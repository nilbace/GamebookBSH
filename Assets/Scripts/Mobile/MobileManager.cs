using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using DefaultNamespace.Network;
using Items;
using TMPro;
using UnityEngine;

public class MobileManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private ChatManager _chatManager;
    [SerializeField] private EndManager _endManager;
    [Header("Network")]
    [SerializeField] private NetworkReceiver _networkReceiver;
    [Header("UI")]
    [SerializeField] private bool showTime;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _ipText;
    [SerializeField] private TextMeshProUGUI _debugText;
    
    private Queue<Action> _actionQueue = new Queue<Action>();
    
    [SerializeField]
    private Inventory _inventory;
    private bool _isGameStarted;
    private TimeSpan _timeSpan;
    private int _minute = 0;
    private void OnEnable()
    {
        _networkReceiver.onMessageReceived += OnMessageReceived;
        IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
        string ips = "";
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ips += ip.ToString() + "\n";
            }
        }
        _ipText.text = ips;
    }
    
    private void OnDisable()
    {
        _networkReceiver.onMessageReceived -= OnMessageReceived;
    }
    
    private void OnMessageReceived(NetworkJsonData json_data)
    {
        _actionQueue.Enqueue(()=>_debugText.text = json_data.ToString());
        
        switch (json_data.messageType)
        {
            case NetworkJsonData.MessageType.Reset:
                _actionQueue.Enqueue(ResetGame);
                break;
            case NetworkJsonData.MessageType.Start:
                _actionQueue.Enqueue(StartGame);
                break;
            case NetworkJsonData.MessageType.End:
                string end = json_data.endName;
                _inventory = json_data.inventory;
                _actionQueue.Enqueue(()=>EndGame(end));
                break;
        }
        if (_isGameStarted)
        {
            _timeSpan = TimeSpan.FromSeconds(json_data.time);
            _inventory = json_data.inventory;
            Debug.Log($"[Mobile] Receive Message: {json_data.messageName}, Time: {_timeSpan}");
        }
    }
    
    public void StartGame()
    {
        _inventory = new Inventory();
        _isGameStarted = true;
        Debug.Log("[Mobile] Start Game");
    }
    public void ResetGame()
    {
        _timeSpan = TimeSpan.Zero;
        _inventory.Clear();
        _isGameStarted = false;
        
        _chatManager.Initialize();
        _endManager.HideEnding();
        
        Debug.Log("[Mobile] Reset Game");
    }
    
    public void EndGame(string endingName)
    {
        _chatManager.HideAll();
        _endManager.PlayEnding(endingName);
        Debug.Log("[Mobile] End Game");
    }

    private void Update()
    {
        while (_actionQueue.Count > 0)
        {
            _actionQueue.Dequeue().Invoke();
        }
        if (!_isGameStarted) return;
        _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(Time.deltaTime));
        if(_minute < _timeSpan.Minutes)
        {
            print($"Hour: {_timeSpan.Hours}");
            _minute = _timeSpan.Minutes;
            _chatManager.OnTimePassed(_minute); 
        }
    }

    private void LateUpdate()
    {
        if(showTime)
            _timeText.text = "Current : "+ _minute+" "+ _timeSpan.ToString();
    }

    private void OnValidate()
    {
        _timeText.gameObject.SetActive(showTime);
    }
}
