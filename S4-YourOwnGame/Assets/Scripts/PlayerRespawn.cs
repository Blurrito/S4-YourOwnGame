using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public static PlayerRespawn instance;

    private GameObject CurrentRespawnPoint;

    [SerializeField] AudioClip respawnSound;

    private void Start()
    {
        instance = this;
    }

    public void SetRespawnPoint(GameObject NewRespawnPoint)
    {
        if (NewRespawnPoint != null)
            CurrentRespawnPoint = NewRespawnPoint;
    }

    public void RespawnObject()
    {
        AudioSource.PlayClipAtPoint(respawnSound, transform.position);
        if (CurrentRespawnPoint != null)
            transform.position = CurrentRespawnPoint.transform.position;
    }
}
