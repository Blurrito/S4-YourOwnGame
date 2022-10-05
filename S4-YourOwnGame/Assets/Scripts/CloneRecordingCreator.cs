using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using System;

public class CloneRecordingCreator : MonoBehaviour
{
    List<TransformRecord> records = new List<TransformRecord>();
    [SerializeField] Transform clone;
    [SerializeField] int RecordingTimeLeft = 30;

    public static CloneRecordingCreator instance;

    void OnEnable()
    {
        StateManager.instance.SaveAllStates(ObjectStateStamp.recording);
        instance = this;
        records = new List<TransformRecord>();
        Invoke(nameof(DecreaseTimer), 1);
        HudManager.instance.SetTimer(RecordingTimeLeft);
        HudManager.instance.ActivateTimer(true);
    }

    void FixedUpdate()
    {
        records.Add(new TransformRecord(clone));
    }

    private void DecreaseTimer()
    {
        if (RecordingTimeLeft <= 0)
        {
            StopAndPlayRecording();
        }
        else
        {
            RecordingTimeLeft--;
            HudManager.instance.SetTimer(RecordingTimeLeft);
            Invoke(nameof(DecreaseTimer), 1);
        }
    }

    void StopAndPlayRecording()
    {
        CloneManager.instance.SaveRecording(records);

        HudManager.instance.ActivateTimer(false);
        CloneManager.instance.PlayRecording(records);
        Destroy(gameObject);
    }

    public void AddAnimationRecord(AnimationRecord animationRecord)
    {
        if (records.Count == 0) return;

        records[^1].animationRecords.Add(animationRecord);
    }

    public void CancelRecording()
    {
        HudManager.instance.ActivateTimer(false);
        StateManager.instance.LoadAllStates(ObjectStateStamp.recording);
        Destroy(gameObject);
    }
}
