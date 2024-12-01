using System;
using UnityEngine;

namespace  Pages
{
    public class Page : MonoBehaviour
    {
        [SerializeField] private PageBody pageBody;

        private void Awake()
        {
            // pageBody = GetComponent<PageBody>();
        }

        public void Enter()
        {
            pageBody.OnPageEnter();
        }
        
        public void Exit()
        {
            pageBody.OnPageExit();
        }
    }
}