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
        [Header("Objects")]
        [SerializeField] private VideoPlayer videoPlayer;
        [SerializeField] private RawImage videoImage;
        [SerializeField] private Image image;
        [Header("Bool")]
        public bool isWatched = false;
        public bool hasVideo => videoPlayer.clip;
        public void Awake()
        {
            videoPlayer.loopPointReached += (e) => isWatched = true;
        }
        
        public void OnPageEnter()
        {
            print($"Page {pageId} Enter");
            gameObject.SetActive(true);
            if(!hasVideo)
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