using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PageEventChannel", menuName = "PageEventChannel")]
    public class PageEventChannelSO:ScriptableObject
    {
        public UnityEvent<int> onPageEnterEvent;
        public UnityEvent<int> onPageExitEvent;
    }
}