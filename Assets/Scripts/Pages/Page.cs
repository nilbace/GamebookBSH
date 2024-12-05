using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace  Pages
{
    public class Page : MonoBehaviour
    {
        [FormerlySerializedAs("pageBody")] [SerializeField] private BasePageBody basePageBody;

        private void Awake()
        {
            // pageBody = GetComponent<PageBody>();
        }

        public void Enter()
        {
            basePageBody.OnPageEnter();
        }
        
        public void Exit()
        {
            basePageBody.OnPageExit();
        }
    }
}