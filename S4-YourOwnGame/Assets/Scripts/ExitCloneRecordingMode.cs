using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCloneRecordingMode : MonoBehaviour
{
    [SerializeField] CloneManager Manager;

    public void OnAnimationStart() => CloneRecordingCreator.instance.StopRecording();

    public void OnFadeToBlackFinish() => HudManager.instance.PlayExitCloneRecordingAnimation();

    public void OnFadeInStart()
    {
        if (Manager != null)
            Manager.SwitchToPlayer();
        CloneRecordingCreator.instance.DestroyClone();
        CloneRecordingCreator.instance.LoadRecording();
    }

    public void OnAnimationFinish()
    {
        if (Manager != null)
            Manager.OnEndRecord();
        CloneRecordingPlayer.instance.Play();
    }
}
