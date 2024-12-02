using System;
using DefaultNamespace.Network;
using Items;
using TMPro;
using UnityEngine;

public class MobileManager : MonoBehaviour
{
    [SerializeField] private NetworkReceiver _networkReceiver;
    
    [SerializeField] private TextMeshProUGUI _timeText;

    private Inventory _inventory;
    private bool _isGameStarted;
    private TimeSpan _timeSpan;
    private void OnEnable()
    {
        _networkReceiver.onMessageReceived += OnMessageReceived;
    }
    
    private void OnDisable()
    {
        _networkReceiver.onMessageReceived -= OnMessageReceived;
    }
    
    private void OnMessageReceived(NetworkJsonData json_data)
    {
        if (json_data.messageType == NetworkJsonData.MessageType.Reset)
        {
            ResetGame();
            _isGameStarted = false;
        }
        else if(json_data.messageType == NetworkJsonData.MessageType.Start)
        {
            StartGame();
        }else if (_isGameStarted)
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

    private void Update()
    {
        _timeSpan = _timeSpan.Add(TimeSpan.FromSeconds(Time.deltaTime));
    }

    private void LateUpdate()
    {
        _timeText.text = _timeSpan.ToString();
    }

    public void ResetGame()
    {
        _timeSpan = TimeSpan.Zero;
        Debug.Log("[Mobile] Reset Game");
    }
}
