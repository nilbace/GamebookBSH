using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Edit
{

    [CustomEditor(typeof(PageController))]
    public class PageControllerEditor: Editor
    {
        public override void OnInspectorGUI()
        {
            PageController pageController = (PageController) target;
            pageController.isForceMove = EditorGUILayout.Toggle("Force Move", pageController.isForceMove);
            if (pageController.isForceMove)
            {
                pageController.pageToChange = EditorGUILayout.IntField("Page To Change", pageController.pageToChange);
            }
            else
            {
                var candidates = pageController.PageManager.CurrentPageBody.nextPageIds;
                if(candidates[0] == -1)
                {
                    EditorGUILayout.LabelField("No Page To Change");
                }
                else
                {
                    pageController.pageToChange = EditorGUILayout.IntPopup(pageController.pageToChange
                        , candidates.Select(e => "Page " + e).ToArray()
                        , candidates.ToArray());
                }
            }
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("Change Page"))
            {
                pageController.ChangePage(pageController.pageToChange);
            }
            if(GUILayout.Button("Enter Page"))
            {
                pageController.EnterPage(pageController.pageToChange);
            }
            if(GUILayout.Button("Exit Page"))
            {
                pageController.ExitPage();
            }
            EditorGUILayout.EndHorizontal();
                        
            EditorGUILayout.BeginHorizontal();
            
            if (GUILayout.Button("Previous Page"))
            {
                pageController.PreviousPage();
            }
            if (GUILayout.Button("Next Page"))
            {
                pageController.NextPage();
            }
            EditorGUILayout.EndHorizontal();
                        
        }
    }
}