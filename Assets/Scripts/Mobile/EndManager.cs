using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Mobile;
using UnityEngine;
using UnityEngine.Video;

public class EndManager : MonoBehaviour
{

    
    public EndDataContainerSO endDataContainer;
    public VideoPlayer videoPlayer;
    public GameObject endingImage;
    
    public void HideEnding()
    {
        videoPlayer.Stop();
        endingImage.SetActive(false);
    }
    
    public void PlayEnding(string endingName)
    {
        var endingData = endDataContainer.endingDataList.Find(data => data.endingName == endingName);
        if (endingData == null)
        {
            Debug.LogError($"Ending Data not found: {endingName}");
            return;
        }
        videoPlayer.clip = endingData.videoClip;
        videoPlayer.Play();
        endingImage.SetActive(true);
    }
}
