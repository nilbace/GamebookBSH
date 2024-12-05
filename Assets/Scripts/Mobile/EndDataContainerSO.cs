using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace DefaultNamespace.Mobile
{
    [CreateAssetMenu(fileName = "EndDataContainer", menuName = "EndDataContainer")]
    public class EndDataContainerSO: ScriptableObject
    {
        public List<EndingData> endingDataList;
        
        [Serializable]
        public class EndingData
        {
            public string endingName;
            public VideoClip videoClip;
        }
    }
}