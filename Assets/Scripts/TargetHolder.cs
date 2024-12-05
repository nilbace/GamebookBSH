using System;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using Object = UnityEngine.Object;

namespace DefaultNamespace
{
    public class TargetHolder : MonoBehaviour
    {
        [Header("Channel")]
        public TargetEventChannelSO channel;
        
        [Header("Targets")]
        [ContextMenuItem("Register", "RegisterTargets")]
        public List<GameObject> targets;
        public List<ImageTargetBehaviour> imageTargets;
        public List<DefaultObserverEventHandler> observers;

        private void Awake()
        {
            // RegisterTargets();
            
        }

        private void Start()
        {
            RegisterChannelToTargets();
        }

        public void RegisterTargets()
        {
            GameObject holder = GameObject.FindWithTag("TargetHolder");
            int childCount = holder.transform.childCount;
            targets = new List<GameObject>();
            imageTargets = new List<ImageTargetBehaviour>();
            observers = new List<DefaultObserverEventHandler>();
            for (int i = 0; i < childCount; i++)
            {
                GameObject child = holder.transform.GetChild(i).gameObject;
                targets.Add(child);
                imageTargets.Add(child.GetComponent<ImageTargetBehaviour>());
                observers.Add(child.GetComponent<DefaultObserverEventHandler>());
            }
        }
        
        public void RegisterChannelToTargets()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                DefaultObserverEventHandler observer = observers[i];
                int index = i;
                observer.OnTargetFound.AddListener(() =>
                {
                    channel.onTargetEnterEvent.Invoke(index);
                });
                observer.OnTargetLost.AddListener(() =>
                {
                    channel.onTargetExitEvent.Invoke(index);
                });
            }
        }
    }
}