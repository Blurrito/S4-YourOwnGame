using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private GameObject CurrentRespawnPoint;

    public void SetRespawnPoint(GameObject NewRespawnPoint)
    {
        if (NewRespawnPoint != null)
            CurrentRespawnPoint = NewRespawnPoint;
    }

    public void RespawnObject()
    {
        if (CurrentRespawnPoint != null)
            transform.position = CurrentRespawnPoint.transform.position;
    }
}
