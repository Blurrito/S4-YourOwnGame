using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;

public class CloneRecordingCreator : MonoBehaviour
{
    List<TransformRecord> records = new List<TransformRecord>();
    [SerializeField] Transform clone;
    [SerializeField] int maxRecordingTime = 30;

    public static CloneRecordingCreator instance;

    void OnEnable()
    {
        instance = this;
        records = new List<TransformRecord>();
        Invoke(nameof(StopRecording), maxRecordingTime);
    }

    void FixedUpdate()
    {
        records.Add(new TransformRecord(clone));
    }

    void StopRecording()
    {
        CloneManager.instance.PlayRecording(records);
        Destroy(gameObject);
    }

    public void AddAnimationRecord(AnimationRecord animationRecord)
    {
        if (records.Count == 0) return;

        records[^1].animationRecords.Add(animationRecord);
    }
}
