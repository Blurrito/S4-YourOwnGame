using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquishManager : MonoBehaviour
{
    [SerializeField] float secondsRequiredForDeath = 0.3f;
    float secondsSinceHitStart = 0f;
    [SerializeField] List<string> squishTags;

    bool wasHitThisUpdate = false;

    private void OnCollisionStay(Collision collision)
    {
        if (!wasHitThisUpdate && squishTags.Contains(collision.gameObject.tag))
        {
            wasHitThisUpdate = true;
        }
    }

    private void LateUpdate()
    {
        if (wasHitThisUpdate)
        {
            secondsSinceHitStart += Time.deltaTime;

            if (secondsSinceHitStart >= secondsRequiredForDeath)
            {
                Debug.Log("Player was squished");
                PlayerRespawn Respawner = PlayerRespawn.instance;
                if (Respawner != null) Respawner.RespawnObject();
            }

            wasHitThisUpdate = false;
        }
        else
        {
            secondsSinceHitStart = 0;
        }
    }
}
