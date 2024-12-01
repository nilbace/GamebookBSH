using UnityEditor;
using UnityEngine;

namespace Test
{
    [CreateAssetMenu(fileName = "TestPageData", menuName = "TestPageData", order = 0)]
    public class TestPageDataSO : ScriptableObject
    {
        public int pageId;
        public string option;
        public bool canGoAnyPage;
        public bool isWrongEnds;
        public int[] nextPages;
        public string gift;
        public string ending;
        public string remarks;
        [TextArea(3, 50)]
        public string text;
        public string imageOrVideo;
        public string sound;
        
        


        public override string ToString()
        {
            return $"pageId: {pageId}, option: {option}, nextPages: {string.Join(",", nextPages)}, gift: {gift}, ending: {ending}, remarks: {remarks}, text: {text}, imageOrVideo: {imageOrVideo}, sound: {sound}";
        }
    }
}