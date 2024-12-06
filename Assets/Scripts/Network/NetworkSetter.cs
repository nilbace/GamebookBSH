using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Network;
using TMPro;
using UnityEngine;

public class NetworkSetter : MonoBehaviour
{
 
    [SerializeField] private NetworkSender networkSender;
    [SerializeField] private TMP_InputField ipInputField;

    private void Start()
    {
        networkSender._IpAddr = PlayerPrefs.GetString("ServerIp", "127.0.0.1");
        ipInputField.text = networkSender._IpAddr;
    }

    public void SetIP()
    {
        networkSender._IpAddr = ipInputField.text;
        networkSender.RegisterClient();
        PlayerPrefs.SetString("ServerIp", networkSender._IpAddr);
    }
    
}
