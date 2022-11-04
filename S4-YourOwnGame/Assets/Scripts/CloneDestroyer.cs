using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneDestroyer : MonoBehaviour
{
    public void DestroyClone()
    {
        if (CloneRecordingCreator.instance != null)
            CloneRecordingCreator.instance.EndRecording(false);
        CloneManager.instance.SaveRecording(null);
    }
}
