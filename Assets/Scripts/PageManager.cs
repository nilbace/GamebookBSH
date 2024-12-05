using System;
using System.Collections.Generic;
using System.Linq;
using Pages;
using Test;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace DefaultNamespace
{
    public class PageManager : MonoBehaviour
    {
        public PageEventChannelSO pageEventChannel;
        public GameObject pageContainer;
        [FormerlySerializedAs("currentPage")] public int currentPageIdx = 0;
        [Header("Page List")]
        public List<Page> pageList;


        
        public Page CurrentPage => pageList[currentPageIdx];
        public PageBody CurrentPageBody => pageList[currentPageIdx].GetComponent<PageBody>();
        private void Awake()
        {
            for (int i = 0; i < pageList.Count; i++)
            {
                pageList[i].gameObject.SetActive(false);
            }
        }

        private void Start()
        {
            currentPageIdx = 0;
 
        }

        public void Initialize()
        {
            currentPageIdx = 0;
            for (int i = 0; i < pageList.Count; i++)
            {
                pageList[i].GetComponent<PageBody>().Initialize();
            }
        }

        private void OnEnable()
        {
            pageEventChannel.onPageEnterEvent.AddListener(EnterPage);
            pageEventChannel.onPageExitEvent.AddListener(ExitPage);
        }
        
        public void EnterPage(int idx)
        {
            if ( currentPageIdx == idx|| CurrentPageBody.nextPageIds.Contains(idx))
            {
                pageList[idx].Enter();
                currentPageIdx = idx;
            }
        }
        public void ForceEnterPage(int index)
        {
            if (index < 0 || index >= pageList.Count)
            {
                return;
            }
            
            pageList[index].Enter();
            currentPageIdx = index;
        }

        public void ExitPage(int idx) => ForceExitPage(idx);
        public void ForceExitPage(int index)
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
            
            pageList[currentPageIdx].Exit();
            pageList[index].Enter();
            currentPageIdx = index;
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