using Test;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pages
{
    /// <summary>
    /// 범용 및 테스트용 페이지 바디
    /// </summary>
    public class UniversalPageBody : BasePageBody
    {
        [Header("Object References")] 
        [SerializeField] private GameObject DebugTextHolder;
        [SerializeField] private TextMeshProUGUI pageIdText;
        [SerializeField] private TextMeshProUGUI nextPagesText;
        [SerializeField] private TextMeshProUGUI optionText;
        [SerializeField] private TextMeshProUGUI giftText;
        [SerializeField] private TextMeshProUGUI endingText;
        [SerializeField] private TextMeshProUGUI remarksText;
        [SerializeField] private TextMeshProUGUI textText;

        
        [FormerlySerializedAs("testPageDataSo")] public PageDataSO pageDataSo;
        
        private void Start()
        {
            Initialize();
        }
        #if UNITY_EDITOR
        public void UpdateTexts()
        {
     
            if(HasVideo)
            {
                DebugTextHolder.gameObject.SetActive(false);
                return;
            }
            else
            {
                DebugTextHolder.gameObject.SetActive(true);
            }
            pageIdText.text = "id : " + pageDataSo.pageId;
            nextPagesText.text = string.Join(",", nextPageIds);
            optionText.text = pageDataSo.option;
            giftText.text = pageDataSo.gift;
            endingText.text = pageDataSo.ending;
            remarksText.text = pageDataSo.remarks;
            textText.text = pageDataSo.text;
            // imageOrVideoText.text = testPageDataSo.imageOrVideo;
            // soundText.text = testPageDataSo.sound;
            
        }
        #endif
        /// <summary>
        /// 초기화
        /// </summary>
        /// <remarks>
        /// Override 하면 안됨
        /// </remarks>
        public new void Initialize()
        {
            pageId = pageDataSo.pageId;
            nextPageIds = pageDataSo.nextPages;
            endingName = pageDataSo.ending;

#if UNITY_EDITOR
            UpdateTexts();
#endif

        }
        
    }
}