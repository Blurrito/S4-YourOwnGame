using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCloneRecordingMode : MonoBehaviour
{
    [SerializeField] CloneManager Manager;

    public void OnFadeToBlackFinish()
    {
        if (Manager != null)
            Manager.SwitchToClone();
    }

    public void OnAnimationFinish() => CloneRecordingCreator.instance.StartCountdown();
}