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
    [SerializeField] Transform clone;
    [SerializeField] int RecordingTimeLeft = 30;

    public static CloneRecordingCreator instance;

    List<GameObject> touchingObjects = new();

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
<<<<<<< Updated upstream
        records.Add(new TransformRecord(clone));
=======
        records.Add(new TransformRecord(transform, new CollidersRecord(touchingObjects)));
    }

    private void OnCollisionEnter(Collision collision)
    {
        touchingObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingObjects.Remove(collision.gameObject);
>>>>>>> Stashed changes
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

    void StopAndPlayRecording()
    {
        CloneManager.instance.SaveRecording(records.ToList());

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
