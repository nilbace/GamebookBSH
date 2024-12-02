using DefaultNamespace.Network;
using UnityEditor;
using UnityEngine;

namespace Edit
{
    
    [CustomEditor(typeof(NetworkSender))]
    public class MessageSenderEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (Application.isPlaying == false)
            {
                GUILayout.Button("Only Available in Play Mode");
                return;
            }
            var messageSender = (NetworkSender) target;
            
            NetworkJsonData json_data = messageSender.testJsonData;
            
            if (GUILayout.Button("Send Message"))
            {
                messageSender.SendMessage(json_data);
            }
        }
    }
}