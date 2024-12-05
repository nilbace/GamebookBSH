using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;

public class PageController : MonoBehaviour
{
    public bool isForceMove = false;

    public PageManager PageManager
    {
        get
        {
           if(!pageManager)
               pageManager = GetComponent<PageManager>();
           return pageManager;
        }
    }
    
    private PageManager pageManager;
    
    [HideInInspector] public int CurrentPageIdx => PageManager.currentPageIdx;
    [Header("Change Page")]
    public int pageToChange = 0;

    private void Awake()
    {
        pageManager = GetComponent<PageManager>();
    }
    
    public void ExitPage()
    {
        PageManager.ForceExitPage(CurrentPageIdx);
    }

    public void EnterPage(int i)
    {
        PageManager.ForceEnterPage(i);
    }

    public void NextPage()
    {
        PageManager.ChangePage(CurrentPageIdx + 1);
    }
    
    public void PreviousPage()
    {
        PageManager.ChangePage(CurrentPageIdx - 1);
    }
    public void ChangePage(int index)
    {
        PageManager.ChangePage(index);
    }
}
