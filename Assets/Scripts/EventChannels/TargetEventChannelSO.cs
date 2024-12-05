using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "TargetEventChannel", menuName = "TargetEventChannel")]
    public class TargetEventChannelSO:ScriptableObject
    {
        public UnityEvent<int> onTargetEnterEvent;
        public UnityEvent<int> onTargetExitEvent;
    }
}