using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] GameObject[] OnDeathFunctions;

    public void SetDeathHandlerFields(GameObject[] OnDeathFunctions) => this.OnDeathFunctions = OnDeathFunctions;

    private void OnCollisionEnter(Collision collision)
    {
        RespawnPlayer(collision);
        ExecuteOnDeathFunctions();
    }

    private void RespawnPlayer(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerRespawn Respawner = collision.gameObject.GetComponent<PlayerRespawn>();
            if (Respawner != null)
                Respawner.RespawnObject();
        }
    }

    private void ExecuteOnDeathFunctions()
    {
        if (OnDeathFunctions != null)
            for (int i = 0; i < OnDeathFunctions.Length; i++)
                if (OnDeathFunctions[i] != null)
                    OnDeathFunctions[i].SendMessage("OnPlayerDeath");
    }
}
