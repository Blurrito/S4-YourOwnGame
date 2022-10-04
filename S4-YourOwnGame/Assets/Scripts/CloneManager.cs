using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloneManager : MonoBehaviour
{
    [SerializeField] Transform protagonist;
    [SerializeField] GameObject clonePhysicalPrefab;
    [SerializeField] GameObject cloneEnvisionPrefab;

    public static CloneManager instance;

    private void Start()
    {
        instance = this;
    }

    void OnStartRecord()
    {
        if (FindObjectOfType<CloneRecordingPlayer>()) return;

        protagonist.GetComponent<PlayerInput>().enabled = false;
        protagonist.gameObject.SetActive(false);
        Instantiate(cloneEnvisionPrefab, protagonist.position, protagonist.rotation);
    }

    public void PlayRecording(List<TransformRecord> records)
    {
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
}
