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
            StopRecording();
        }
        else
        {
            RecordingTimeLeft--;
            HudManager.instance.SetTimer(RecordingTimeLeft);
            Invoke(nameof(DecreaseTimer), 1);
        }
    }

    void StopRecording()
    {
        HudManager.instance.ActivateTimer(false);
        CloneManager.instance.PlayRecording(records);
        Destroy(gameObject);
    }

    public void AddAnimationRecord(AnimationRecord animationRecord)
    {
        if (records.Count == 0) return;

        records[^1].animationRecords.Add(animationRecord);
    }
}
