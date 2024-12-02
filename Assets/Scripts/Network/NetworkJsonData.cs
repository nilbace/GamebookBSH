using System;
using UnityEngine;

namespace DefaultNamespace.Network
{
    [Serializable]
    public class NetworkJsonData
    {
        public bool doReset;
        public float time;
        public string messageName;
        [TextArea(5,20)]
        public string message;

        public string etc;
        
        public string TimeString => TimeSpan.FromSeconds(time).ToString();
        
        public string ToString()
        {
            return $"doReset: {doReset}, time: {TimeString}({time}), message: {message}";
        }
    }
}