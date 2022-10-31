using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using Cinemachine;

public class CloneManager : MonoBehaviour
{
    [SerializeField] Transform protagonist;
    [SerializeField] GameObject clonePhysicalPrefab;
    [SerializeField] GameObject cloneEnvisionPrefab;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    [SerializeField] GameObject PlayerCameraRoot;
    [SerializeField] AudioListener audioListener;
    [SerializeField] Animator EnterCloneRecordingAnimation;
    [SerializeField] Animator ExitCloneRecordingAnimation;

    public static CloneManager instance;

    private List<TransformRecord> previousCloneRecording;

    [SerializeField] public bool canUseClones = true;

    private void Start()
    {
        instance = this;
    }

    void OnStartRecord()
    {
        if (!canUseClones) return;

        if (FindObjectOfType<CloneRecordingPlayer>()) return;

        if (EnterCloneRecordingAnimation != null)
        {
            SetPlayerActionMapStatus(false);
            Animator ZoomInAnimator = virtualCamera.GetComponent<Animator>();
            if (ZoomInAnimator != null)
                ZoomInAnimator.SetTrigger("Active");
            EnterCloneRecordingAnimation.SetTrigger("Active");
        }
    }

    public void OnEndRecord() => SetPlayerActionMapStatus(true);

    public void SwitchToClone()
    {
        SetPlayerControlStatus(false);
        GameObject newClone = Instantiate(cloneEnvisionPrefab, transform.position, transform.rotation);
    }

    public void SwitchToPlayer()
    {
        SetPlayerControlStatus(true);
    }

    public void PrepareRecordingPlayer(List<TransformRecord> records)
    {
        if (!canUseClones) return;

        StateManager.instance.LoadAllStates(ObjectStateStamp.recording);

        transform.SetPositionAndRotation(records[0].position, records[0].rotation);
        SetPlayerControlStatus(true);
        

        GameObject replayingClone = Instantiate(clonePhysicalPrefab, protagonist.position, protagonist.rotation);
        replayingClone.GetComponent<CloneRecordingPlayer>().records = records;
    }

    private void EnablePlayerInput() => playerInput.enabled = true;

    public void SetPlayerControlStatus(bool IsEnabled)
    {
        PlayerCameraRoot.SetActive(IsEnabled);
        audioListener.enabled = IsEnabled;
        virtualCamera.gameObject.SetActive(IsEnabled);
        if (IsEnabled)
            Invoke(nameof(EnablePlayerInput), 0.01f);
    }

    public void SetPlayerActionMapStatus(bool IsEnabled)
    {
        if (playerInput != null)
            playerInput.enabled = IsEnabled;
    }

    public void SaveRecording(List<TransformRecord> recording)
    {
        if (!canUseClones) return;

        previousCloneRecording = recording;
    }

    public void OnRetryWithLastRecording()
    {
        if (!canUseClones) return;

        if (previousCloneRecording != null && previousCloneRecording.Count > 0)
        {
            if (CloneRecordingCreator.instance != null)
            {
                CloneRecordingCreator.instance.EndRecording(false);
            }

            var clonePlayer = FindObjectOfType<CloneRecordingPlayer>();
            if (CloneRecordingPlayer.instance != null)
            {
                Destroy(clonePlayer.gameObject);
            }

            PrepareRecordingPlayer(previousCloneRecording.ToList());
            CloneRecordingPlayer Player = FindObjectOfType<CloneRecordingPlayer>();
            Player.Play();
        }
        else
        {
            Debug.Log("No previous recording exists.");
        }
    }
}
