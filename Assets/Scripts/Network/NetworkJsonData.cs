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
            End,
            Etc
        }
        public MessageType messageType;
        public float time;
        public string messageName;
        [TextArea(5,20)]
        public string message;
        [FormerlySerializedAs("etc")] public string endName;
        public Inventory inventory;
        
        public string TimeString => TimeSpan.FromSeconds(time).ToString();
        
        public string ToString()
        {
            return $"[NetworkJsonData] Type: {messageType},\n" +
                   $" Time: {TimeString},\n" +
                   $" Message: {messageName},\n" +
                   $" Etc: {endName}";
        }
    }
}