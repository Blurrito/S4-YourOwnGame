using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using UnityEngine;

public class DisintegrationManager : MonoBehaviour
{
    [SerializeField] float GroundedOffset = -0.14f;
    [SerializeField] float GroundedRadius = 0.28f;
    [SerializeField] LayerMask GroundLayers;
    [SerializeField] AudioClip disintegrationSound;
    [SerializeField] CloneRecordingPlayer recordingPlayer;

    private static bool hasShownMessageObject = false;
    private static bool hasShownMessageFloor = false;

    int ticksWithoutFloor = 0;
    [SerializeField] int maxTicksWithoutFloor = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasShownMessageObject)
        {
            hasShownMessageObject = true;
            DialogueManager.instance.AddHintPopupAndDestroy("If an object blocks the clone's path, it will disintegrate.", 5);
        }

        AudioSource.PlayClipAtPoint(disintegrationSound, transform.position);
        Debug.Log("Die to collision with: " + collision.gameObject.name);
        recordingPlayer.KillClone();
    }

    public bool CheckGroundedEqual(bool recorded)
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset,transform.position.z);
        bool actual = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

        if (recorded != actual)
        {
            if (ticksWithoutFloor >= maxTicksWithoutFloor)
            {
                if (!hasShownMessageFloor)
                {
                    hasShownMessageFloor = true;
                    DialogueManager.instance.AddHintPopupAndDestroy("If the clone is not touching the ground when it should, it will disintegrate.", 5);
                }
                Debug.Log("Die to no floor");
                AudioSource.PlayClipAtPoint(disintegrationSound, transform.position);
                recordingPlayer.KillClone();
                return false;
            }
            else
            {
                ticksWithoutFloor++;
                return true;
            }
        }
        else
        {
            ticksWithoutFloor = 0;
            return true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
        Gizmos.DrawWireSphere(spherePosition, GroundedRadius);
    }
}