using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public class CloneManager : MonoBehaviour
{
    [SerializeField] Transform protagonist;
    [SerializeField] GameObject clonePhysicalPrefab;
    [SerializeField] GameObject cloneEnvisionPrefab;

    public static CloneManager instance;

    private List<TransformRecord> previousCloneRecording;

    private void Start()
    {
        instance = this;
    }

    void OnStartRecord()
    {
        if (FindObjectOfType<CloneRecordingPlayer>()) return;

        protagonist.GetComponent<PlayerInput>().enabled = false;
        protagonist.gameObject.SetActive(false);
        GameObject newClone = Instantiate(cloneEnvisionPrefab, protagonist.position, protagonist.rotation);
        newClone.transform.Find("PlayerCameraRoot").rotation = transform.Find("PlayerCameraRoot").rotation;
    }

    public void PlayRecording(List<TransformRecord> records)
    {
        StateManager.instance.LoadAllStates(ObjectStateStamp.recording);

        transform.SetPositionAndRotation(records[0].position, records[0].rotation);
        ReturnControlToPlayer();
        

        GameObject replayingClone = Instantiate(clonePhysicalPrefab, protagonist.position, protagonist.rotation);
        replayingClone.GetComponent<CloneRecordingPlayer>().records = records;
    }

    private void FixInput()
    {
        protagonist.GetComponent<PlayerInput>().enabled = true;
    }

    public void ReturnControlToPlayer()
    {
        protagonist.gameObject.SetActive(true);
        protagonist.GetComponent<PlayerInput>().enabled = false;
        Invoke(nameof(FixInput), 0.01f);
    }

    public void SaveRecording(List<TransformRecord> recording)
    {
        previousCloneRecording = recording;
    }

    public void OnRetryWithLastRecording()
    {
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
