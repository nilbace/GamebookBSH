using System.Net.Sockets;
using UnityEngine;

namespace DefaultNamespace.Network
{
    public class NetworkSender : MonoBehaviour
    {
        private UdpClient _udpClient;
        
        public string _IpAddr = "127.0.0.1";
        public int _Port = 22222;
        
        public NetworkJsonData testJsonData;
        
        public void Start() {
            _udpClient = new UdpClient(_IpAddr, _Port);
            _udpClient.Client.Blocking = false; // 듣지 않음
        }
        [ContextMenu("Send Test Message")]
        public void SendTestMessage()
        {
            NetworkJsonData json_data = new NetworkJsonData();
            json_data.doReset = false;
            json_data.time = 0;
            json_data.message = "Hello World";
            SendMessage(json_data);
        }
        
        public void SendMessage(NetworkJsonData jsonData)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonData);
            byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonString);
            
            _udpClient.Send(data, data.Length);
            print($"Send Message: {data.Length} to {_IpAddr}:{_Port}");
        }
    }
}