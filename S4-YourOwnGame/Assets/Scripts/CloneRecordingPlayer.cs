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

    [SerializeField] DisintegrationManager disintegrationManager;

    static public CloneRecordingPlayer instance;

    private bool StartPlayback = false;

    List<GameObject> touchingObjects = new();

    private void Start()
    {
        instance = this;
    }

    public void Play() => StartPlayback = true;

    void FixedUpdate()
    {
        if (records.Count == 0 || !StartPlayback) return;

        disintegrationManager.CheckGroundedEqual(records[0].IsGrounded);

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
            StartPlayback = false;
            KillClone();
        }
    }

    public void KillClone()
    {
        transform.position = new Vector3(0, -1000, 0);
        Destroy(gameObject);
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

    private void OnCollisionEnter(Collision collision)
    {
        touchingObjects.Add(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        touchingObjects.Remove(collision.gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        touchingObjects.Add(collision.gameObject);
    }

    private void OnTriggerExit(Collider collision)
    {
        touchingObjects.Remove(collision.gameObject);
    }

    private void OnDestroy()
    {
        foreach (GameObject ob in touchingObjects)
        {
            ButtonTrigger bt = ob.GetComponent<ButtonTrigger>();
            if (bt != null)
            {
                bt.HandleExit(this.gameObject);
            }

            ToggleTrigger tt = ob.GetComponent<ToggleTrigger>();
            if (tt != null)
            {
                tt.HandleExit(this.gameObject);
            }
        }
    }
}
