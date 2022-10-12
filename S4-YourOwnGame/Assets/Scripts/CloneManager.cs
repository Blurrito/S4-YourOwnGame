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
    [SerializeField] AudioListener audioListener;

    public static CloneManager instance;

    private List<TransformRecord> previousCloneRecording;

    [SerializeField] bool canUseClones = true;

    private void Start()
    {
        instance = this;
    }

    void OnStartRecord()
    {
        if (!canUseClones) return;

        if (FindObjectOfType<CloneRecordingPlayer>()) return;

        playerInput.enabled = false;
        virtualCamera.gameObject.SetActive(false);
        audioListener.enabled = false;
        GameObject newClone = Instantiate(cloneEnvisionPrefab, transform.position, transform.rotation);
    }

    public void PlayRecording(List<TransformRecord> records)
    {
        if (!canUseClones) return;

        StateManager.instance.LoadAllStates(ObjectStateStamp.recording);

        transform.SetPositionAndRotation(records[0].position, records[0].rotation);
        ReturnControlToPlayer();
        

        GameObject replayingClone = Instantiate(clonePhysicalPrefab, protagonist.position, protagonist.rotation);
        replayingClone.GetComponent<CloneRecordingPlayer>().records = records;
    }

    private void FixInput()
    {
        playerInput.enabled = true;
    }

    public void ReturnControlToPlayer()
    {
        audioListener.enabled = true;
        virtualCamera.gameObject.SetActive(true);
        playerInput.enabled = false;
        Invoke(nameof(FixInput), 0.01f);
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
                CloneRecordingCreator.instance.CancelRecording();
            }

            var clonePlayer = FindObjectOfType<CloneRecordingPlayer>();
            if (clonePlayer != null)
            {
                Destroy(clonePlayer.gameObject);
            }

            PlayRecording(previousCloneRecording.ToList());
        }
        else
        {
            Debug.Log("No previous recording exists.");
        }
    }
}
