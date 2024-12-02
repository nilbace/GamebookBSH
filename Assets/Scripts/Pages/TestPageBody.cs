using Test;
using TMPro;
using UnityEngine;

namespace Pages
{
    public class TestPageBody : PageBody
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

        
        public TestPageDataSO testPageDataSo;
        
        private void Start()
        {
            Initialize();
        }
        
        public void UpdateTexts()
        {
            if(hasVideo)
            {
                DebugTextHolder.gameObject.SetActive(false);
                return;
            }
            else
            {
                DebugTextHolder.gameObject.SetActive(true);
            }
            pageIdText.text = "id : " + testPageDataSo.pageId;
            nextPagesText.text = string.Join(",", nextPageIds);
            optionText.text = testPageDataSo.option;
            giftText.text = testPageDataSo.gift;
            endingText.text = testPageDataSo.ending;
            remarksText.text = testPageDataSo.remarks;
            textText.text = testPageDataSo.text;
            // imageOrVideoText.text = testPageDataSo.imageOrVideo;
            // soundText.text = testPageDataSo.sound;
        }

        public void Initialize()
        {
            pageId = testPageDataSo.pageId;
            nextPageIds = testPageDataSo.nextPages;
            UpdateTexts();
        }
        
    }
}