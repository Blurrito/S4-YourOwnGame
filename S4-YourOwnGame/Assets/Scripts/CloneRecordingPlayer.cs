using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneRecordingPlayer : MonoBehaviour
{
    public List<TransformRecord> records = new List<TransformRecord>();
    [SerializeField] Animator animator;

    [SerializeField] AudioClip[] footstepSounds;
    [SerializeField] AudioClip onLandSound;

    [SerializeField, Range(0, 1), ] float footstepAudioVolume;


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

        records.RemoveAt(0);

        if (records.Count == 0)
        {
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
