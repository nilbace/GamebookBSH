using System.Net.Sockets;
using UnityEngine;

namespace DefaultNamespace.Network
{
    public class NetworkSender : MonoBehaviour
    {
        private UdpClient _udpClient;
        
        public string _IpAddr = "127.0.0.1";
        public int _Port = 22222;
        #if UNITY_EDITOR
        public NetworkJsonData testJsonData;
        #endif
        
        public void Start()
        {
            _udpClient = new UdpClient(_IpAddr, _Port);
            _udpClient.Client.Blocking = false; // 막지 않음
        }
        
        public void RegisterClient()
        {
            _udpClient.Close();
            _udpClient = new UdpClient(_IpAddr, _Port);
            _udpClient.Client.Blocking = false; // 막지 않음
            
        }
        
        public void OnDestroy()
        {
            _udpClient.Close();
        }
        #if UNITY_EDITOR
        [ContextMenu("Send Test Message")]
        public void SendTestMessage()
        {
            NetworkJsonData jsonData = new NetworkJsonData();
            jsonData.messageType = NetworkJsonData.MessageType.Message;
            jsonData.messageName = "Test Message";
            jsonData.message = "This is Test Message";
            SendMessage(jsonData);
        }
        #endif
        
        public void SendMessage(NetworkJsonData jsonData)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonString);
            
            _udpClient.Send(data, data.Length);
            print($"Send Message: {jsonData.messageType}({jsonData.time}) to {_IpAddr}:{_Port}");
        }
    }
}