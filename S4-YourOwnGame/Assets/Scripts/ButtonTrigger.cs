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

    public bool IsPressed = false;
    public List<string> Actors = new List<string>();

    public void OnCollisionEnter(Collision collision)
    {
        if (!IsPressed && !Actors.Contains(collision.gameObject.name))
        {
            if (HasObjectTagRestriction && TagNames != null)
            {
                if (TagNames.Contains(collision.gameObject.tag))
                {
                    Actors.Add(collision.gameObject.name);
                    BeginButtonPress();
                }
                        
            }
            else
            {
                Actors.Add(collision.gameObject.name);
                BeginButtonPress();
            }
        }
    }

    public void OnCollisionExit(Collision collision)
    {
        if (IsPressed && Actors.Contains(collision.gameObject.name))
        {
            Actors.Remove(collision.gameObject.name);
            if (Actors.Count == 0 && DisableOnExit)
                EndButtonPress();
        }
    }

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
