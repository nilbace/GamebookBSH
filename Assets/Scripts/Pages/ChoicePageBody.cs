using System;
using System.Collections.Generic;
using Items;
using Test;
using UnityEngine;

namespace Pages
{
    public class ChoicePageBody : BasePageBody
    {
        [SerializeField]
        private PageDataSO pageDataSo;

        [SerializeField] private GameObject[] blockImages;
        
        
        [ContextMenu("Initialize")]
        public override void Initialize()
        {
            base.Initialize();
            pageId = pageDataSo.pageId;
            nextPageIds = pageDataSo.nextPages;
            
        }

        public override void OnPageEnter()
        {
            base.OnPageEnter();
            Inventory inv = GameManager.Instance.inventory;
            List<int> nextPages = new List<int>();
            for (int i = 0; i < blockImages.Length; i++)
            {
                if(inv.HasItem((Inventory.Item) (1 << i)))
                {
                    
                    nextPages.Add(pageDataSo.nextPages[i]);
                    blockImages[i].SetActive(false);
                }
                else
                {
                    blockImages[i].SetActive(true);
                }
                // blockImages[i].SetActive(!inv.HasItem((Inventory.Item) (1 << i)));
            }
            nextPageIds = nextPages.ToArray();
        }
        
        public override void OnPageExit()
        {
            print($"Page {pageId} Exit");
            if (waitForReadyCoroutine != null)
            {
                StopCoroutine(waitForReadyCoroutine);
            }


            if (!isWatched)
            {
                videoPlayer.Stop();
            }
            RenderTexture renderTexture = videoPlayer.targetTexture;
            renderTexture.Release();
            gameObject.SetActive(false);
        }
    }
}