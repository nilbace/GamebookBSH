using System;
using System.Collections.Generic;
using Pages;
using Test;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class PageManager : MonoBehaviour
    {
        public PageEventChannelSO pageEventChannel;
        public GameObject pageContainer;
        public int currentPage = 0;
        [Header("Page List")]
        public List<Page> pageList;


        
        public Page CurrentPage => pageList[currentPage];
        public PageBody CurrentPageBody => pageList[currentPage].GetComponent<PageBody>();
        private void Awake()
        {
            for (int i = 0; i < pageList.Count; i++)
            {
                pageList[i].gameObject.SetActive(false);
            }
            pageList[0].gameObject.SetActive(true);
        }

        private void Start()
        {
            currentPage = 0;
        }

        private void OnEnable()
        {
            pageEventChannel.onPageEnterEvent.AddListener(EnterPage);
            pageEventChannel.onPageExitEvent.AddListener(ExitPage);
        }
        
        public void EnterPage(int index)
        {
            if (index < 0 || index >= pageList.Count)
            {
                return;
            }
            
            pageList[index].Enter();
            currentPage = index;
        }
        
        public void ExitPage(int index)
        {
            if (index < 0 || index >= pageList.Count)
            {
                return;
            }
            
            pageList[index].Exit();
        }

        public void ChangePage(int index)
        {
            if (index < 0 || index >= pageList.Count)
            {
                return;
            }
            
            pageList[currentPage].Exit();
            pageList[index].Enter();
            currentPage = index;
        }
        
        #if UNITY_EDITOR
        [Header("Dev")]
        public GameObject pagePrefab;
        
        public List<TestPageDataSO> pageDataList;
        
        [ContextMenu("Create Pages")]
        public void CreatePages()
        {
            for (int i = pageList.Count; i < pageDataList.Count; i++)
            {
                GameObject page = (GameObject)PrefabUtility.InstantiatePrefab(pagePrefab, pageContainer.transform);
                
                pageList.Add(page.GetComponent<Page>());
                page.name = "Page " + i;
                
            }
        }
        
        [ContextMenu("Initialize Pages")]
        public void InitializePages()
        {
            for (int i = 0; i < pageDataList.Count; i++)
            {
                TestPageBody pageBody = pageList[i].GetComponent<TestPageBody>();
                pageBody.testPageDataSo = pageDataList[i];
                pageBody.Initialize();
                pageBody.gameObject.SetActive(false);
                PrefabUtility.RecordPrefabInstancePropertyModifications(pageBody);
            }
        }
        
        [ContextMenu("Clear Pages")]
        public void ClearPages()
        {
            for (int i = 0; i < pageList.Count; i++)
            {
                DestroyImmediate(pageList[i].gameObject);
            }
            pageList.Clear();
        }
        #endif
    }
}