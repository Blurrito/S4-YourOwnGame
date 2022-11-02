using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieOnSquish : MonoBehaviour
{
    private static List<DieOnSquish> squishes = new();
    public bool isHitThisUpdate = false;
    private static List<string> notAllowedTags = new() { "Player", "Clone", "PlayerClone" };

    private void Start()
    {
        squishes.Add(this);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!isHitThisUpdate && !notAllowedTags.Contains(collision.gameObject.tag))
        {
            isHitThisUpdate = true;

            if (squishes.TrueForAll(x => x.isHitThisUpdate))
            {
                PlayerRespawn Respawner = PlayerRespawn.instance;
                if (Respawner != null)
                    Respawner.RespawnObject();
            }
        }
    }

    private void LateUpdate()
    {
        isHitThisUpdate = false;
    }
}
