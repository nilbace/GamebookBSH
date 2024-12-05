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
        ipInputField.text = networkSender._IpAddr;
    }

    public void SetIP()
    {
        networkSender._IpAddr = ipInputField.text;
        networkSender.RegisterClient();
    }
    
}
