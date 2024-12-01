using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DefaultCamera : MonoBehaviour
    {
        
        public string[] displays;
        
        private void Start()
        {
            List<DisplayInfo> displayInfos = new List<DisplayInfo>();
            Screen.GetDisplayLayout(displayInfos);
            displays = new string[Display.displays.Length];
            for (int i = 0; i < Display.displays.Length; i++)
            {
                displays[i] = displayInfos[i].name;
            }
            Debug.Log("displays connected: " + Display.displays.Length);
            // Display.displays[0] is the primary, default display and is always ON, so start at index 1.
            // Check if additional displays are available and activate each.
            for (int i = 1; i < Display.displays.Length; i++)
            {
                Display.displays[i].Activate();
            }
        }
        
        public void SetDisplay(int displayIndex)
        {
                Display.displays[displayIndex].SetRenderingResolution(5000,3508);
                Camera cam = GetComponent<Camera>();
                // cam.SetTargetBuffers(Display.displays[displayIndex].colorBuffer, Display.displays[displayIndex].depthBuffer);
                cam.targetDisplay = displayIndex;
                cam.enabled = true;
        }
    }
}