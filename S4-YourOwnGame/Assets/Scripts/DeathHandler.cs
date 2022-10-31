using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject[] OnDeathFunctions;
    [SerializeField] public bool TriggerPlayerDeath = true;
    [SerializeField] public bool TriggerCloneDisintegration = true;

    public void SetDeathHandlerFields(GameObject[] OnDeathFunctions) => this.OnDeathFunctions = OnDeathFunctions;

    private void OnTriggerEnter(Collider other) => PerformDeathHandlerFunctions(other.gameObject);

    private void OnCollisionEnter(Collision collision) => PerformDeathHandlerFunctions(collision.gameObject);

    private void PerformPrematureDisintegration()
    {
        if (CloneRecordingCreator.instance != null)
        {
            CloneRecordingCreator.instance.EndRecording(false);
            //CloneManager.instance.SetPlayerControlStatus();
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
        PlayerRespawn Respawner = PlayerRespawn.instance;
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
