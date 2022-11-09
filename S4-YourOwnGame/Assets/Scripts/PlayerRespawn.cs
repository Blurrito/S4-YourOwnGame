using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn instance;

    private GameObject CurrentRespawnPoint;

    [SerializeField] AudioClip respawnSound;
    [SerializeField] Animator fade;

    bool isRespawning = false;

    private void Start()
    {
        if (instance != null) return;

        instance = this;
        CurrentRespawnPoint = new GameObject("InitialSpawnPoint");
        CurrentRespawnPoint.transform.SetPositionAndRotation(transform.position, transform.rotation);
    }

    public void SetRespawnPoint(GameObject NewRespawnPoint)
    {
        StateManager.instance.SaveAllStates(ObjectStateStamp.checkpoint);

        if (NewRespawnPoint != null)
            CurrentRespawnPoint = NewRespawnPoint;
    }

    public void RespawnObject()
    {
        if (isRespawning) return;

        isRespawning = true;

        StateManager.instance.LoadAllStates(ObjectStateStamp.checkpoint);

        if (fade) fade.SetTrigger("Fade");

        AudioSource.PlayClipAtPoint(respawnSound, transform.position);

        StartCoroutine(SetPos());
    }

    private IEnumerator SetPos()
    {
        if (fade) yield return new WaitForSeconds(0.3f);

        if (CurrentRespawnPoint != null)
            transform.position = CurrentRespawnPoint.transform.position;


        yield return new WaitForSeconds(0.1f);
        isRespawning = false;
    }
}
