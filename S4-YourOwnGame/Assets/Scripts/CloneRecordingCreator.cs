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
    [SerializeField] PlayerInput CloneInput;
    [SerializeField] Animator EnterCloneRecordingAnimation;
    [SerializeField] Animator ExitCloneRecordingAnimation;

    private bool RecordingStarted = false;
    private bool RecordingSaved = false;

    public void StartCountdown()
    {
        Invoke(nameof(DecreaseTimer), 1);
        HudManager.instance.SetTimer(RecordingTimeLeft);
        HudManager.instance.ActivateTimer(true);
        RecordingStarted = true;
        SetCloneActionMapStatus(true);
    }

    void OnEnable()
    {
        StateManager.instance.SaveAllStates(ObjectStateStamp.recording);
        instance = this;
        records = new List<TransformRecord>();

        if (EnterCloneRecordingAnimation != null)
            EnterCloneRecordingAnimation.SetTrigger("Active");
    }

    void FixedUpdate()
    {
        if (RecordingStarted)
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
            EndRecording(true);
        }
        else
        {
            RecordingTimeLeft--;
            HudManager.instance.SetTimer(RecordingTimeLeft);
            Invoke(nameof(DecreaseTimer), 1);
        }
    }

    public void EndRecording(bool SaveRecording)
    {
        RecordingSaved = SaveRecording;
        if (ExitCloneRecordingAnimation != null)
            ExitCloneRecordingAnimation.SetTrigger("Active");
    }

    public void StopRecording()
    {
        SetCloneActionMapStatus(false);
        RecordingStarted = false;
        if (RecordingSaved)
            CloneManager.instance.SaveRecording(records.ToList());
        HudManager.instance.ActivateTimer(false);
    }

    public void DestroyClone()
    {
        transform.parent.position = new Vector3(0, -1000, 0);
        Destroy(transform.parent.gameObject);
    }

    public void LoadRecording()
    {
        StateManager.instance.LoadAllStates(ObjectStateStamp.recording);
        if (RecordingSaved)
            CloneManager.instance.PrepareRecordingPlayer(records);
    }

    public void AddAnimationRecord(AnimationRecord animationRecord)
    {
        if (records.Count == 0) return;

        records[^1].animationRecords.Add(animationRecord);
    }

    private void SetCloneActionMapStatus(bool IsEnabled)
    {
        if (CloneInput != null)
            CloneInput.enabled = IsEnabled;
    }
}
