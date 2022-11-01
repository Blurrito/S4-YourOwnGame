using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using System.Linq;

public class ButtonTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] EffectObjects;
    [SerializeField] bool DisableOnExit = true;
    [SerializeField] bool HasActiveTimer = false;
    [SerializeField] float ActiveTimerSeconds = 0f;
    [SerializeField] bool HasObjectTagRestriction = false;
    [SerializeField] string[] TagNames;
    [SerializeField] AudioClip clickSound;

    public bool IsPressed = false;
    public List<string> Actors = new List<string>();

    public void HandleEnter(GameObject collider)
    {
        if (!IsPressed && !Actors.Contains(collider.name))
        {
            AudioSource.PlayClipAtPoint(clickSound, transform.position);

            if (HasObjectTagRestriction && TagNames != null)
            {
                if (TagNames.Contains(collider.tag))
                {
                    Actors.Add(collider.name);
                    BeginButtonPress();
                }

            }
            else
            {
                Actors.Add(collider.name);
                BeginButtonPress();
            }
        }
    }

    public void OnCollisionEnter(Collision collision) => HandleEnter(collision.gameObject);
    public void OnTriggerEnter(Collider other) => HandleEnter(other.gameObject);


    public void HandleExit(GameObject collider)
    {
        if (IsPressed && Actors.Contains(collider.name))
        {
            Actors.Remove(collider.name);
            if (Actors.Count == 0 && DisableOnExit)
                EndButtonPress();
        }
    }

    public void OnCollisionExit(Collision collision) => HandleExit(collision.gameObject);
    public void OnTriggerExit(Collider other) => HandleExit(other.gameObject);

    public void TriggerEffect()
    {
        if (EffectObjects != null)
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                    Effect.StartEffect();
            }
    }

    public void RevertEffect()
    {
        if (EffectObjects != null)
            foreach (GameObject EffectObject in EffectObjects)
            {
                TriggerEffect[] Effects = EffectObject.GetComponents<TriggerEffect>();
                foreach (TriggerEffect Effect in Effects)
                    Effect.EndEffect();
            }
    }

    private void BeginButtonPress()
    {
        IsPressed = true;
        CancelInvoke(nameof(RevertEffect));
        TriggerEffect();
    }

    private void EndButtonPress()
    {
        IsPressed = false;
        if (HasActiveTimer)
            Invoke(nameof(RevertEffect), ActiveTimerSeconds);
        else
            RevertEffect();
    }
}
