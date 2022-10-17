using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;
using System;
using System.Linq;

public class CloneRecordingCreator : MonoBehaviour
{
    List<TransformRecord> records = new List<TransformRecord>();
    [SerializeField] int RecordingTimeLeft = 30;

    public static CloneRecordingCreator instance;

    List<GameObject> touchingObjects = new();

    [SerializeField] ThirdPersonController ThirdPersonController;

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
        records.Add(new TransformRecord(transform, ThirdPersonController.Grounded));
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchingObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingObjects.Remove(collision.gameObject);
    }

    private void DecreaseTimer()
    {
        if (RecordingTimeLeft == 0)
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

    public void StopAndPlayRecording()
    {
        CloneManager.instance.SaveRecording(records.ToList());
        HudManager.instance.ActivateTimer(false);
        StateManager.instance.LoadAllStates(ObjectStateStamp.recording);
        CloneManager.instance.PlayRecording(records);
        Destroy(transform.parent.gameObject);
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
        transform.parent.position = new Vector3(0, -1000, 0);
        Destroy(transform.parent.gameObject);
    }
}
