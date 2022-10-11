using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloneRecordingPlayer : MonoBehaviour
{
    public List<TransformRecord> records = new List<TransformRecord>();
    [SerializeField] Animator animator;

    [SerializeField] AudioClip[] footstepSounds;
    [SerializeField] AudioClip onLandSound;

    [SerializeField, Range(0, 1), ] float footstepAudioVolume;

    List<GameObject> touchingObjects = new();

    private void OnCollisionEnter(Collision collision)
    {
        touchingObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingObjects.Remove(collision.gameObject);
    }


    void FixedUpdate()
    {
        if (records.Count == 0) return;

        transform.position = records[0].position;
        transform.rotation = records[0].rotation;

        foreach(AnimationRecord ar in records[0].animationRecords)
        {
            if (ar.isBool)
            {
                animator.SetBool(ar.name, ar.state);
            }
            else
            {
                animator.SetFloat(ar.name, ar.numState);
            }
        }

        //checks whether the list of objects we are currently touching is equal to the list of objects we touching during the creation of the recording
        if (touchingObjects.All(records[0].colliders.gameObjects.Contains) && records[0].colliders.gameObjects.All(touchingObjects.Contains))
        {
            Debug.Log("this bitch died prematurely ahahahahahah");
            //transform.position = new Vector3(0, -1000, 0);
            //Destroy(gameObject);
            //return;
        }

        records.RemoveAt(0);

        if (records.Count == 0)
        {
            transform.position = new Vector3(0, -1000, 0);
            Destroy(gameObject);
        }
    }

    private void OnFootstep(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            if (footstepSounds.Length > 0)
            {
                var index = Random.Range(0, footstepSounds.Length);
                AudioSource.PlayClipAtPoint(footstepSounds[index], transform.position, footstepAudioVolume);
            }
        }
    }

    private void OnLand(AnimationEvent animationEvent)
    {
        if (animationEvent.animatorClipInfo.weight > 0.5f)
        {
            AudioSource.PlayClipAtPoint(onLandSound, transform.position, footstepAudioVolume);
        }
    }
}
