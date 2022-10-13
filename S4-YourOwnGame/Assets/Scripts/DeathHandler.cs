using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject[] OnDeathFunctions;

    public void SetDeathHandlerFields(GameObject[] OnDeathFunctions) => this.OnDeathFunctions = OnDeathFunctions;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            RespawnPlayer(collision);
        else if (collision.gameObject.CompareTag("PlayerClone"))
            PerformPrematureDisintegration(collision);
        ExecuteOnDeathFunctions();
    }

    private void PerformPrematureDisintegration(Collision collision)
    {
        if (CloneRecordingCreator.instance != null)
        {
            CloneRecordingCreator.instance.CancelRecording();
            CloneManager.instance.ReturnControlToPlayer();
        }
    }

    private void RespawnPlayer(Collision collision)
    {
        PlayerRespawn Respawner = collision.gameObject.GetComponent<PlayerRespawn>();
        if (Respawner != null)
            Respawner.RespawnObject();
    }

    private void ExecuteOnDeathFunctions()
    {
        if (OnDeathFunctions != null)
            for (int i = 0; i < OnDeathFunctions.Length; i++)
                if (OnDeathFunctions[i] != null)
                    OnDeathFunctions[i].SendMessage("OnPlayerDeath");
    }
}
