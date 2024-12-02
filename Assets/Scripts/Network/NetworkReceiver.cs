using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DefaultNamespace.Network;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkReceiver : MonoBehaviour
{

    public delegate void MessageReceiveEvent(NetworkJsonData json_data);
    
    public event MessageReceiveEvent onMessageReceived; 

    public string _IpAddr = "127.0.0.1";
    public int _Port = 22222;
    
    private Thread listenThread;

    public void Start() {
        // Start TcpServer background thread
        listenThread = new Thread(Listen);
        listenThread.IsBackground = true;
        listenThread.Start();
    }

    void Listen() {
        Debug.Log($"Start Listening on {_IpAddr}:{_Port}");
        UdpClient listener = new UdpClient(_Port);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _Port);
        try {
            while (true){
                byte[] bytes = listener.Receive(ref endPoint);
                print($"Receive Message: {bytes.Length} from {endPoint.Address}:{endPoint.Port}");
                NetworkJsonData json_data = GetJsonFromByte(bytes);
                
                MessageHandler(json_data);
            }
        } catch (SocketException e) {
            Debug.Log("SocketException " + e.ToString());
        } finally {
            listener.Close();
        }
    }

    public NetworkJsonData GetJsonFromByte(byte[] data) {
        string JsonString = Encoding.UTF8.GetString(data);
        var JsonData = Newtonsoft.Json.JsonConvert.DeserializeObject<NetworkJsonData>(JsonString);
        return JsonData;
    }

    private void MessageHandler(NetworkJsonData json_data)
    {
        print("Handle Message: " + json_data.ToString());
        onMessageReceived?.Invoke(json_data);
    }
    
}
