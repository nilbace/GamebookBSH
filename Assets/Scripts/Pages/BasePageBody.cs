using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Pages
{
    [RequireComponent(typeof(Page))]
    public class BasePageBody : MonoBehaviour
    {
        [Header("Data")]
        public int pageId;
        public int[] nextPageIds;
        public string item;
        public bool giveItem;
        [FormerlySerializedAs("ending")] public string endingName = String.Empty;
        [Header("Media, Video가 우선됨.")]
        [SerializeField] private Sprite imageSprite;
        [SerializeField] private VideoClip videoClip;
        [Header("Objects")]
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private RawImage videoImage;
        [SerializeField] private Image image;
        [Header("Bool")]
        public bool isWatched = false;
        public bool isVideoPrepared = false;
        public bool HasVideo => videoClip;
        public bool HasImage => imageSprite;
        public bool IsEnding => !string.IsNullOrEmpty(endingName);
        public void Awake()
        {
            videoPlayer.loopPointReached += (e) => isWatched = true;
            videoPlayer.prepareCompleted += e => isVideoPrepared = true;
        }

        public virtual void Initialize()
        {
            gameObject.SetActive(false);
            isWatched = false;
            videoPlayer.Prepare();
            videoPlayer.clip = videoClip;
        }
        
        
        public virtual void OnPageEnter()
        {
            print($"Page {pageId} Enter");
            gameObject.SetActive(true);
            videoImage.gameObject.SetActive(false);
            
            
            if (HasVideo)
            {
                if(!videoPlayer.isPrepared)
                {
                    videoPlayer.Prepare();
                }
                if(isWatched)
                {
                    videoPlayer.time = videoPlayer.length;
                }
                videoPlayer.Play();
                videoImage.gameObject.SetActive(true);
            }
            if(HasImage)
            {
                image.sprite = imageSprite;
                if(isVideoPrepared)
                {
                    image.gameObject.SetActive(true);
                }else
                {
                    waitForReadyCoroutine = StartCoroutine(WaitForReady());
                }
            }

            if (giveItem)
            {
                GameManager.Instance.inventory.AddItem(item);
            }
            
            if (IsEnding)
            {
                GameManager.Instance.ShowEnding(endingName);
            }
        }
        
        private Coroutine waitForReadyCoroutine = null;
        
        private IEnumerator WaitForReady()
        {
            yield return new WaitUntil(() => isVideoPrepared);
            image.gameObject.SetActive(true);
        }

        public virtual void OnPageExit()
        {
            print($"Page {pageId} Exit");
            if(waitForReadyCoroutine != null)
            {
                StopCoroutine(waitForReadyCoroutine);
            }
            
            
            if(!isWatched)
            {
                videoPlayer.Stop();
            }
            RenderTexture renderTexture = videoPlayer.targetTexture;
            renderTexture.Release();
            gameObject.SetActive(false);
        }
    }
}