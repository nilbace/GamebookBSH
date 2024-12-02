using System;
using Items;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace.Network
{
    [Serializable]
    public class NetworkJsonData
    {
        public enum MessageType
        {
            Start,
            Reset,
            Message,
            Etc
        }
        public MessageType messageType;
        public float time;
        public string messageName;
        [TextArea(5,20)]
        public string message;
        public string etc;
        public Inventory inventory;
        
        public string TimeString => TimeSpan.FromSeconds(time).ToString();
        
        public string ToString()
        {
            return $"[NetworkJsonData] Type: {messageType}, Time: {TimeString}, Message: {messageName}, Etc: {etc}";
        }
    }
}