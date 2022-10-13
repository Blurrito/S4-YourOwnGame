using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointUpdater : MonoBehaviour
{
    [SerializeField] PlayerRespawn RespawnObject;
    [SerializeField] GameObject RespawnPoint;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Player"))
        {
            RespawnObject.SetRespawnPoint(RespawnPoint);
            Destroy(gameObject);
        }
    }
}
