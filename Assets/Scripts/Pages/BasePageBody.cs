using System;
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
        public bool HasVideo => videoClip;
        public bool HasImage => imageSprite;
        public bool IsEnding => !string.IsNullOrEmpty(endingName);
        public void Awake()
        {
            videoPlayer.loopPointReached += (e) => isWatched = true;
        }

        public void Initialize()
        {
            gameObject.SetActive(false);
            isWatched = false;
        }
        
        
        public void OnPageEnter()
        {
            print($"Page {pageId} Enter");
            gameObject.SetActive(true);
            videoImage.gameObject.SetActive(false);
            // 영상 있으면 우선적으로 재생
            if (HasVideo)
            {
                videoPlayer.clip = videoClip;
            }else if(HasImage)
            {
                image.sprite = imageSprite;
                image.gameObject.SetActive(true);
                videoImage.gameObject.SetActive(false);
            }else
            {
                videoImage.gameObject.SetActive(false);
            }
            
            if(isWatched)
            {
                videoPlayer.time = videoPlayer.length;
                videoPlayer.Play();
                videoImage.gameObject.SetActive(true);
            }

            if (IsEnding)
            {
                GameManager.Instance.SendMessage(endingName);
            }
        }

        public void OnPageExit()
        {
            print($"Page {pageId} Exit");
            if(HasVideo)
            {
                videoImage.gameObject.SetActive(true);
            }
            if(!isWatched)
            {
                videoPlayer.Stop();
            }
            gameObject.SetActive(false);
        }
    }
}