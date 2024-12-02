using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace Pages
{
    [RequireComponent(typeof(Page))]
    public class PageBody : MonoBehaviour
    {
        [Header("Data")]
        public int pageId;
        public int[] nextPageIds;
        [Header("Media, Video가 우선됨.")]
        [SerializeField] private Sprite imageSprite;
        [SerializeField] private VideoClip videoClip;
        [Header("Objects")]
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private RawImage videoImage;
        [SerializeField] private Image image;
        [Header("Bool")]
        public bool isWatched = false;
        public bool hasVideo => videoClip;
        public bool hasImage => imageSprite;
        public void Awake()
        {
            videoPlayer.loopPointReached += (e) => isWatched = true;
        }
        
        
        public void OnPageEnter()
        {
            print($"Page {pageId} Enter");
            gameObject.SetActive(true);
            if (hasVideo)
            {
                videoPlayer.clip = videoClip;
            }else if(hasImage)
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
            }
        }

        public void OnPageExit()
        {
            print($"Page {pageId} Exit");
            if(hasVideo)
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