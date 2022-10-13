using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject[] OnDeathFunctions;
    [SerializeField] bool TriggerPlayerDeath = true;
    [SerializeField] bool TriggerCloneDisintegration = true;

    public void SetDeathHandlerFields(GameObject[] OnDeathFunctions) => this.OnDeathFunctions = OnDeathFunctions;

    private void OnTriggerEnter(Collider other) => PerformDeathHandlerFunctions(other.gameObject);

    private void OnCollisionEnter(Collision collision) => PerformDeathHandlerFunctions(collision.gameObject);

    private void PerformPrematureDisintegration()
    {
        if (CloneRecordingCreator.instance != null)
        {
            CloneRecordingCreator.instance.CancelRecording();
            CloneManager.instance.ReturnControlToPlayer();
        }
    }

    private void PerformDeathHandlerFunctions(GameObject collision)
    {
        if (collision.gameObject.CompareTag("Player") && TriggerPlayerDeath)
        {
            RespawnPlayer();
            ExecuteOnDeathFunctions();
        }
        else if (collision.gameObject.CompareTag("PlayerClone") && TriggerCloneDisintegration)
            PerformPrematureDisintegration();
    }

    private void RespawnPlayer()
    {
        PlayerRespawn Respawner = gameObject.GetComponent<PlayerRespawn>();
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
