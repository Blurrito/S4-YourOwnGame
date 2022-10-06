using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointUpdater : MonoBehaviour
{
    [SerializeField] PlayerRespawn RespawnObject;
    [SerializeField] GameObject RespawnPoint;

    private bool IsTriggered = false;

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("RespawnUpdate");
        if (!IsTriggered)
            if (other.gameObject.tag.Equals("Player"))
            {
                IsTriggered = true;
                RespawnObject.SetRespawnPoint(RespawnPoint);
            }
    }
}
